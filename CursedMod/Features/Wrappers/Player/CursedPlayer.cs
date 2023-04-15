// -----------------------------------------------------------------------
// <copyright file="CursedPlayer.cs" company="CursedMod">
// Copyright (c) CursedMod. All rights reserved.
// Licensed under the GPLv3 license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CommandSystem;
using CursedMod.Features.Enums;
using CursedMod.Features.Extensions;
using CursedMod.Features.Logger;
using CursedMod.Features.Wrappers.Facility;
using CursedMod.Features.Wrappers.Inventory.Items;
using CursedMod.Features.Wrappers.Inventory.Pickups;
using CursedMod.Features.Wrappers.Player.Roles;
using CursedMod.Features.Wrappers.Player.Roles.SCPs;
using CursedMod.Features.Wrappers.Player.VoiceChat;
using CursedMod.Features.Wrappers.Server;
using CustomPlayerEffects;
using Footprinting;
using Hints;
using Interactables;
using InventorySystem;
using InventorySystem.Disarming;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using InventorySystem.Searching;
using Mirror;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerStatsSystem;
using PluginAPI.Core;
using RemoteAdmin;
using Security;
using Subtitles;
using UnityEngine;
using Utils.Networking;
using VoiceChat;

namespace CursedMod.Features.Wrappers.Player;

public class CursedPlayer
{
    public static readonly Dictionary<ReferenceHub, CursedPlayer> Dictionary = new ();

    internal CursedPlayer(ReferenceHub hub)
    {
        ReferenceHub = hub;
        GameObject = ReferenceHub.gameObject;
        Transform = ReferenceHub.transform;
        CurrentRole = CursedRole.Get(RoleBase);
        
        if (hub == ReferenceHub.HostHub || NetworkConnection.address == "npc")
            return;
        
        SetUp();
        
        CursedLogger.InternalDebug("Adding Player");
        Dictionary.Add(hub, this);
    }
    
    public static IEnumerable<CursedPlayer> Collection => Dictionary.Values;
    
    public static List<CursedPlayer> List => Collection.ToList();
   
    public static int Count => Dictionary.Count;
    
    public ReferenceHub ReferenceHub { get; }
    
    public GameObject GameObject { get; private set; }
    
    public Transform Transform { get; internal set; }

    public PlayerSharedStorage SharedStorage { get; } = new ();

    public AuthenticationType AuthenticationType { get; private set; }
    
    public string RawUserId { get; private set; }

    public CursedRole CurrentRole { get; internal set; }
    
    public Transform PlayerCameraReference => ReferenceHub.PlayerCameraReference;

    public NetworkIdentity NetworkIdentity => ReferenceHub.networkIdentity;

    public NetworkConnectionToClient NetworkConnection => NetworkIdentity.connectionToClient;

    public CharacterClassManager CharacterClassManager => ReferenceHub.characterClassManager;

    public PlayerRoleManager RoleManager => ReferenceHub.roleManager;

    public PlayerStats PlayerStats => ReferenceHub.playerStats;

    public InventorySystem.Inventory Inventory => ReferenceHub.inventory;

    public Dictionary<ushort, ItemBase> Items => Inventory.UserInventory.Items;

    public SearchCoordinator SearchCoordinator => ReferenceHub.searchCoordinator;

    public ServerRoles ServerRoles => ReferenceHub.serverRoles;

    public QueryProcessor QueryProcessor => ReferenceHub.queryProcessor;

    public NicknameSync NicknameSync => ReferenceHub.nicknameSync;

    public PlayerInteract PlayerInteract => ReferenceHub.playerInteract;

    public InteractionCoordinator InteractionCoordinator => ReferenceHub.interCoordinator;

    public PlayerEffectsController PlayerEffectsController => ReferenceHub.playerEffectsController;

    public HintDisplay HintDisplay => ReferenceHub.hints;

    public AspectRatioSync AspectRatioSync => ReferenceHub.aspectRatioSync;

    public PlayerRateLimitHandler PlayerRateLimitHandler => ReferenceHub.playerRateLimitHandler;

    public GameConsoleTransmission GameConsoleTransmission => ReferenceHub.gameConsoleTransmission;

    public FriendlyFireHandler FriendlyFireHandler => ReferenceHub.FriendlyFireHandler;

    public Footprint Footprint => new (ReferenceHub);

    public PlayerCommandSender Sender => QueryProcessor._sender;
    
    public HealthStat HealthStat => PlayerStats.GetModule<HealthStat>();
    
    public AhpStat AhpStat => PlayerStats.GetModule<AhpStat>();
    
    public StaminaStat StaminaStat => PlayerStats.GetModule<StaminaStat>();
    
    public AdminFlagsStat AdminFlagsStat => PlayerStats.GetModule<AdminFlagsStat>();
    
    public HumeShieldStat HumeShieldStat => PlayerStats.GetModule<HumeShieldStat>();

    public bool IsDummy => ReferenceHub == ReferenceHub.HostHub || NetworkConnection.address == "npc";
    
    public string SaltedUserId => CharacterClassManager.SaltedUserId;
    
    public bool IsVerified => ServerRoles.IsVerified;
    
    public string GroupName => ServerRoles.GetUncoloredRoleString();

    public byte KickPower => ServerRoles.KickPower;
    
    // public bool IsDummy => this is CursedDummy;
    public bool IsDead => Role is RoleTypeId.Spectator or RoleTypeId.Overwatch or RoleTypeId.None;

    public bool IsAlive => !IsDead;

    public bool IsFoundationForce => RoleManager.CurrentRole.Team is Team.FoundationForces;

    public bool IsChaos => RoleManager.CurrentRole.Team is Team.ChaosInsurgency;

    public bool IsScp => RoleManager.CurrentRole.Team is Team.SCPs;

    public bool IsTutorial => Role is RoleTypeId.Tutorial;

    public bool IsHuman => !IsScp || !IsDead;
    
    public bool IsCuffed => Inventory.IsDisarmed();

    public float TimeHoldingCurrentItem => Inventory.LastItemSwitch;

    public CursedItem CurrentItem
    {
        get => CursedItem.Get(Inventory._curInstance);
        set => SetHoldingItem(value);
    }

    public ItemType CurrentItemType => Inventory.GetSelectedItemType();

    public ItemIdentifier PreviousHoldingItem => Inventory._prevCurItem;
    
    public bool CanInteract => PlayerInteract.CanInteract;

    public Color RoleColor => CurrentRole.RoleColor;

    public bool BadgeHidden
    {
        get => !string.IsNullOrEmpty(ServerRoles.HiddenBadge);
        set
        {
            if (value)
                ShowTag();
            else
                HideTag();
        }
    }
    
    public bool IsBypassEnabled
    {
        get => ServerRoles.BypassMode;
        set => ServerRoles.BypassMode = value;
    }

    public Vector3 Position
    {
        get => Transform.position;
        set => ReferenceHub.TryOverridePosition(value, Vector3.zero);
    }

    public Vector3 Rotation
    {
        get => Transform.eulerAngles;
        set => ReferenceHub.TryOverridePosition(Position, value);
    }

    public float HorizontalRotation
    {
        get
        {
            if (CurrentRole is not CursedFpcRole fpcRole)
                return 0;

            return fpcRole.FpcModule.MouseLook.CurrentHorizontal;
        }
        
        set
        {
            if (CurrentRole is not CursedFpcRole fpcRole)
                return;

            fpcRole.FpcModule.MouseLook.CurrentHorizontal = value;
        }
    }
    
    public float VerticalRotation
    {
        get
        {
            if (CurrentRole is not CursedFpcRole fpcRole)
                return 0;

            return fpcRole.FpcModule.MouseLook.CurrentVertical;
        }
        
        set
        {
            if (CurrentRole is not CursedFpcRole fpcRole)
                return;

            fpcRole.FpcModule.MouseLook.CurrentVertical = value;
        }
    }
    
    public Vector3 Scale
    {
        get => Transform.localScale;
        set
        {
            Transform.localScale = value;
            SendSpawnMessageToAll(NetworkIdentity);
        }
    }
    
    public int Id
    {
        get => ReferenceHub.PlayerId;
        set => ReferenceHub.Network_playerId = new RecyclablePlayerId(value);
    }
    
    public string UserId
    {
        get => CharacterClassManager.UserId;
        set => CharacterClassManager.UserId = value;
    }
    
    public string SyncedUserId
    {
        get => CharacterClassManager.SyncedUserId;
        set => CharacterClassManager.NetworkSyncedUserId = value;
    }

    public uint NetId => NetworkIdentity.netId;
    
    public ClientInstanceMode InstanceMode
    {
        get => CharacterClassManager.InstanceMode;
        set => CharacterClassManager.InstanceMode = value;
    }
    
    public string AuthToken
    {
        get => CharacterClassManager.AuthToken;
        set => CharacterClassManager.AuthToken = value;
    }

    public string AuthTokenSerial
    {
        get => CharacterClassManager.AuthTokenSerial;
        set => CharacterClassManager.AuthTokenSerial = value;
    }
    
    public string Asn
    {
        get => CharacterClassManager.AuthTokenSerial;
        set => CharacterClassManager.AuthTokenSerial = value;
    }

    public string PublicPlayerInfoToken
    {
        get => ServerRoles.PublicPlayerInfoToken;
        set => ServerRoles.NetworkPublicPlayerInfoToken = value;
    }
    
    public RateLimit InteractRateLimit
    {
        get => CharacterClassManager._interactRateLimit;
        set => CharacterClassManager._interactRateLimit = value;
    }
    
    public RateLimit CommandRateLimit
    {
        get => CharacterClassManager._commandRateLimit;
        set => CharacterClassManager._commandRateLimit = value;
    }

    public CentralAuthInterface CentralAuthInterface
    {
        get => CharacterClassManager._centralAuthInt;
        set => CharacterClassManager._centralAuthInt = value;
    }

    public RoleTypeId Role
    {
        get => RoleBase.RoleTypeId;
        set => SetRole(value);
    }
    
    public PlayerRoleBase RoleBase
    {
        get => RoleManager.CurrentRole;
        set => RoleManager.CurrentRole = value;
    }

    public CursedScp049Role CursedScp049Role => CurrentRole as CursedScp049Role;
    
    public CursedScp079Role CursedScp079Role => CurrentRole as CursedScp079Role;
    
    public CursedScp096Role CursedScp096Role => CurrentRole as CursedScp096Role;
    
    public CursedScp106Role CursedScp106Role => CurrentRole as CursedScp106Role;
    
    public CursedScp173Role CursedScp173Role => CurrentRole as CursedScp173Role;
    
    public CursedScp939Role CursedScp939Role => CurrentRole as CursedScp939Role;
    
    public CursedZombieRole CursedZombieRole => CurrentRole as CursedZombieRole;
    
    public CursedHumanRole CursedHumanRole => CurrentRole as CursedHumanRole; 

    public bool IsInOverWatch
    {
        get => ServerRoles.IsInOverwatch;
        set => ServerRoles.IsInOverwatch = value;
    }
    
    public float SearchRayDistance
    {
        get => SearchCoordinator.RayDistance;
        set => SearchCoordinator.RayDistance = value;
    }
    
    public UserGroup Group
    {
        get => ServerRoles.Group;
        set => ServerRoles.SetGroup(value, false);
    }

    public string RankColor
    {
        get => ServerRoles._myColor;
        set => ServerRoles.SetColor(value);
    }
    
    public string RankName
    {
        get => ServerRoles._myText;
        set => ServerRoles.SetText(value);
    }

    public bool DoNotTrack
    {
        get => ServerRoles.DoNotTrack;
        set => ServerRoles.DoNotTrack = value;
    }
    
    public bool HasGodMode
    {
        get => CharacterClassManager.GodMode;
        set => CharacterClassManager.GodMode = value;
    }

    public bool HasNoClip
    {
        get => AdminFlagsStat.HasFlag(AdminFlags.Noclip);
        set => AdminFlagsStat.SetFlag(AdminFlags.Noclip, value);
    }

    public bool IsNorthWoodStaff => ServerRoles.Staff;

    public bool IsGlobalModerator => ServerRoles.RaEverywhere;

    public bool IsDisarmed
    {
        get => Inventory.IsDisarmed();
        set
        {
            if (value)
                Disarm();
            else 
                Release();
        }
    }

    public CursedPlayer Disarmer
    {
        get => IsDisarmed ? Get(DisarmedPlayers.Entries.Find(x => x.DisarmedPlayer == NetId).Disarmer) : null;
        set => Disarm(value);
    }
    
    public Dictionary<ItemType, ushort> Ammo
    {
        get => Inventory.UserInventory.ReserveAmmo;
        set => Inventory.UserInventory.ReserveAmmo = value;
    }

    public uint FriendlyFireKills
    {
        get => FriendlyFireHandler.Round.Kills;
        set => FriendlyFireHandler.Round.Kills = value;
    }

    public float FriendlyFireDamage
    {
        get => FriendlyFireHandler.Round.Damage;
        set => FriendlyFireHandler.Round.Damage = value;
    }
    
    public PlayerInfoArea PlayerInfoArea
    {
        get => NicknameSync._playerInfoToShow;
        set => NicknameSync.Network_playerInfoToShow = value;
    }

    public string CustomInfo
    {
        get => NicknameSync._customPlayerInfoString;
        set => NicknameSync.Network_customPlayerInfoString = value;
    }

    public string DisplayNickname
    {
        get => ReferenceHub.nicknameSync.DisplayName;
        set => ReferenceHub.nicknameSync.Network_displayName = value;
    }

    public string RealNickname
    {
        get => ReferenceHub.nicknameSync._myNickSync;
        set => ReferenceHub.nicknameSync._myNickSync = value;
    }

    public string Address => NetworkConnection.address;

    public bool HasReservedSlot => ReservedSlot.HasReservedSlot(UserId, out _);

    public string GlobalBadge => ServerRoles.GlobalBadge;

    public ulong Permissions
    {
        get => ServerRoles.Permissions;
        set => ServerRoles.Permissions = value;
    }

    // if user is basically in the whitelist file
    public bool IsOnWhitelist => WhiteList.IsOnWhitelist(UserId);

    // difference: this also returns true if the whitelist is disabled
    public bool IsWhitelisted => WhiteList.IsWhitelisted(UserId);

    public CursedVoiceChat VoiceChat
    {
        get
        {
            if (CurrentRole is CursedFpcRole fpcRole)
                return new CursedVoiceChat(fpcRole.VoiceModule);

            if (CurrentRole is CursedScp079Role scp079Role)
                return new CursedVoiceChat(scp079Role.VoiceModule);

            if (CurrentRole is CursedNoneRole noneRole)
                return new CursedVoiceChat(noneRole.VoiceModule);

            return null;
        }
    }

    public bool IsHost => ReferenceHub == ReferenceHub.HostHub;
    
    public static void SendSpawnMessageToAll(NetworkIdentity identity)
    {
        try
        {
            foreach (CursedPlayer target in Collection)
            {
                NetworkServer.SendSpawnMessage(identity, target.NetworkConnection);
            }
        }
        catch
        {
            // ignore
        }
    }

    public static bool TryGet(ReferenceHub hub, out CursedPlayer player)
    {
        if (hub is not null && Dictionary.TryGetValue(hub, out CursedPlayer value))
        {
            player = value;
            return true;
        }

        player = null;
        return false;
    }

    public static bool TryGet(GameObject go, out CursedPlayer player)
    {
        foreach (CursedPlayer ply in Collection)
        {
            if (ply.GameObject != go)
                continue;

            player = ply;
            return true;
        }

        player = null;
        return false;
    }
    
    public static bool TryGet(MonoBehaviour component, out CursedPlayer player) => TryGet(component.gameObject, out player);

    public static bool TryGet(NetworkIdentity identity, out CursedPlayer player) => TryGet(identity.gameObject, out player);

    public static bool TryGet(int id, out CursedPlayer player)
    {
        foreach (ReferenceHub hub in ReferenceHub.AllHubs)
        {
            if (hub.PlayerId == id)
                return TryGet(hub, out player);
        }

        player = null;
        return false;
    }
    
    public static bool TryGet(string info, out CursedPlayer player)
    {
        foreach (CursedPlayer ply in Collection)
        {
            if (ply.Id.ToString() != info && ply.UserId != info && ply.RawUserId != info && ply.DisplayNickname != info && ply.Address != info && ply.Sender.LogName != info)
                continue;

            player = ply;
            return true;
        }

        player = null;
        return false;
    }

    public static bool TryGet(ICommandSender sender, out CursedPlayer player)
    {
        foreach (CursedPlayer ply in Collection)
        {
            if (ply.Sender != sender)
                continue;

            player = ply;
            return true;
        }

        player = null;
        return false;
    }
    
    public static bool TryGet(uint netId, out CursedPlayer player)
    {
        if (ReferenceHub.TryGetHubNetID(netId, out ReferenceHub hub))
            return TryGet(hub, out player);
        
        player = null;
        return false;
    }

    public static CursedPlayer Get(ReferenceHub hub) => TryGet(hub, out CursedPlayer player) ? player : CursedServer.LocalPlayer;
   
    public static CursedPlayer Get(GameObject go) => TryGet(go, out CursedPlayer player) ? player : CursedServer.LocalPlayer;
   
    public static CursedPlayer Get(MonoBehaviour component) => TryGet(component, out CursedPlayer player) ? player : CursedServer.LocalPlayer;
  
    public static CursedPlayer Get(NetworkIdentity identity) => TryGet(identity, out CursedPlayer player) ? player : CursedServer.LocalPlayer;
   
    public static CursedPlayer Get(int id) => TryGet(id, out CursedPlayer player) ? player : CursedServer.LocalPlayer;
   
    public static CursedPlayer Get(string info) => TryGet(info, out CursedPlayer player) ? player : CursedServer.LocalPlayer;
   
    public static CursedPlayer Get(ICommandSender sender) => TryGet(sender, out CursedPlayer player) ? player : CursedServer.LocalPlayer;
   
    public static CursedPlayer Get(uint netId) => TryGet(netId, out CursedPlayer player) ? player : CursedServer.LocalPlayer;

    public void Kill(string text) => Kill(new CustomReasonDamageHandler(text, float.MaxValue));

    public void Kill(DamageHandlerBase damageHandlerBase) => PlayerStats.KillPlayer(damageHandlerBase);

    public void Damage(float amount, string reason = "") => PlayerStats.DealDamage(new CustomReasonDamageHandler(reason, amount));
    
    public void Damage(DamageHandlerBase damageHandlerBase) => PlayerStats.DealDamage(damageHandlerBase);
    
    public IEnumerable<CursedItem> GetItems()
    {
        return Items.Values.Select(CursedItem.Get);
    }

    public IEnumerable<ItemType> GetItemTypes()
    {
        return Items.Values.Select(x => x.ItemTypeId);
    }

    public void GrantRoleLoadout(RoleTypeId role, bool resetInventory) => InventoryItemProvider.ServerGrantLoadout(ReferenceHub, role, resetInventory);

    public void SetStableGroup(string name)
    {
        ServerRoles.SetGroup(ServerStatic.GetPermissionsHandler().GetGroup(name), true);
        ServerStatic.GetPermissionsHandler()._members.SetOrAddElement(UserId, name);
    }
    
    public void Release()
    {
        Inventory.SetDisarmedStatus(null);
        new DisarmedPlayersListMessage(DisarmedPlayers.Entries).SendToAuthenticated();
    }
    
    public void Disarm(CursedPlayer cuffer = null)
    {
        if (cuffer is null)
        {
            Inventory.SetDisarmedStatus(null);
            DisarmedPlayers.Entries.Add(new DisarmedPlayers.DisarmedEntry(NetId, 0U));
            new DisarmedPlayersListMessage(DisarmedPlayers.Entries).SendToAuthenticated();
            return;
        }
        
        Inventory.SetDisarmedStatus(cuffer.Inventory);
        DisarmedPlayers.Entries.Add(new DisarmedPlayers.DisarmedEntry(NetId, cuffer.NetId));
        new DisarmedPlayersListMessage(DisarmedPlayers.Entries).SendToAuthenticated();
    }

    public void SetHoldingItem(CursedItem item) => Inventory.ServerSelectItem(item.Serial);

    public bool TryGetEffect(string effectName, out StatusEffectBase effect) => PlayerEffectsController.TryGetEffect(effectName, out effect);
    
    public bool TryGetEffect<T>(out T effect) 
        where T : StatusEffectBase => PlayerEffectsController.TryGetEffect(out effect);

    public StatusEffectBase ChangeState(string effectName, byte intensity, float duration = 0f, bool addDuration = false) => PlayerEffectsController.ChangeState(effectName, intensity, duration, addDuration);

    public T ChangeState<T>(byte intensity, float duration = 0f, bool addDuration = false) 
        where T : StatusEffectBase => PlayerEffectsController.ChangeState<T>(intensity, duration, addDuration);

    public T EnableEffect<T>(float duration = 0f, bool addDuration = false) 
        where T : StatusEffectBase =>
        PlayerEffectsController.EnableEffect<T>(duration, addDuration);
    
    public T DisableEffect<T>() 
        where T : StatusEffectBase => PlayerEffectsController.DisableEffect<T>();

    public void DisableAllEffects() => PlayerEffectsController.DisableAllEffects();

    public void SendPulseEffect<T>() 
        where T : IPulseEffect => PlayerEffectsController.ServerSendPulse<T>();
    
    public void OpenRemoteAdmin() => ServerRoles.TargetOpenRemoteAdmin(true);

    public void CloseRemoteAdmin() => ServerRoles.TargetCloseRemoteAdmin();
    
    public void ToggleOverWatch() => IsInOverWatch = !IsInOverWatch;
    
    public void SyncServerCommandBinds() => CharacterClassManager.SyncServerCmdBinding();
    
    public void SendCommandBind(KeyCode code, string command) => CharacterClassManager.TargetChangeCmdBinding(NetworkConnection, code, command);

    public void SendConsoleMessage(string text, string color) => GameConsoleTransmission.SendToClient(NetworkConnection, text, color);

    public void Disconnect(string message) => CharacterClassManager.DisconnectClient(NetworkConnection, message);

    public void ShowTag(bool global = false) => CharacterClassManager.UserCode_CmdRequestShowTag(global);
    
    public void HideTag() => CharacterClassManager.UserCode_CmdRequestHideTag();

    public void ShowHint(string content, int time = 5) => ShowHint(new TextHint(content, new HintParameter[] { new StringHintParameter(string.Empty) }, null, 2));
    
    public void ShowHint(Hint hint) => HintDisplay.Show(hint);
    
    public void ClearBroadcasts() => CursedFacility.Broadcast.TargetClearElements(NetworkConnection);
    
    public void ShowBroadcast(string message, ushort duration = 5, Broadcast.BroadcastFlags flags = Broadcast.BroadcastFlags.Normal) => CursedFacility.Broadcast.TargetAddElement(NetworkConnection, message, duration, flags);

    public void ShowSubtitle(SubtitlePart[] subtitleParts) => NetworkConnection.Send(new SubtitleMessage(subtitleParts));
    
    public void SetRole(RoleTypeId role, RoleChangeReason reason = RoleChangeReason.RemoteAdmin, RoleSpawnFlags flags = RoleSpawnFlags.All) => RoleManager.ServerSetRole(role, reason, flags);

    public T AddComponent<T>() 
        where T : MonoBehaviour => GameObject.AddComponent<T>();
   
    public T GetComponent<T>()
        where T : MonoBehaviour => GameObject.GetComponent<T>();

    public bool Kick(string reason) => BanPlayer.KickUser(ReferenceHub, reason);

    public bool Ban(string reason, long duration) => BanPlayer.BanUser(ReferenceHub, reason, duration);

    public void SetScene(string sceneName) => NetworkConnection.Send(new SceneMessage { sceneName = sceneName });

    public void SendEscapeInformation(Escape.EscapeMessage message) => NetworkConnection.Send(message);

    public ItemBase AddItemBase(ItemType itemType) => Inventory.ServerAddItem(itemType);
    
    public CursedItem AddItem(ItemType itemType) => CursedItem.Get(AddItemBase(itemType));

    public void AddItem(CursedItem item)
    {
        Inventory.UserInventory.Items[item.Serial] = item.Base;
        Inventory.SendItemsNextFrame = true;
    }
    
    public void SetItems(IEnumerable<CursedItem> items)
    {
        ClearItems();
        
        foreach (CursedItem item in items)
        {
            AddItem(item);
        }
    }

    public void DropItem(CursedItem item) => Inventory.ServerDropItem(item.Serial);

    public void DropItem(ItemBase itemBase) => Inventory.ServerDropItem(itemBase.ItemSerial);

    public void DropItem(CursedPickup pickup) => Inventory.ServerDropItem(pickup.Serial);

    public void DropItem(ItemPickupBase pickupBase) => Inventory.ServerDropItem(pickupBase.Info.Serial);

    public void RemoveItem(CursedItem item) => Inventory.ServerRemoveItem(item.Serial, item.Base.PickupDropModel);

    public void RemoveItem(ItemBase itemBase) => Inventory.ServerRemoveItem(itemBase.ItemSerial, itemBase.PickupDropModel);

    public void RemoveItem(ItemPickupBase pickupBase) => Inventory.ServerRemoveItem(pickupBase.Info.Serial, pickupBase);

    public void RemoveItem(CursedPickup pickup) => Inventory.ServerRemoveItem(pickup.Serial, pickup.Base);

    public void RemoveItemWithoutDestroying(CursedItem cursedItem) => RemoveItemWithoutDestroying(cursedItem.Base);
    
    public void RemoveItemWithoutDestroying(ItemBase itemBase)
    {
        if (itemBase.ItemSerial == Inventory.CurItem.SerialNumber)
        {
            Inventory.NetworkCurItem = ItemIdentifier.None;
        }
        
        if (itemBase is IAcquisitionConfirmationTrigger acquisitionConfirmationTrigger)
        {
            acquisitionConfirmationTrigger.AcquisitionAlreadyReceived = false;
        }
        
        Inventory.UserInventory.Items.Remove(itemBase.ItemSerial);
        Inventory.SendItemsNextFrame = true;
    }

    public ushort GetAmmo(ItemType ammoType) => Inventory.GetCurAmmo(ammoType);
    
    public void SetAmmo(ItemType itemType, ushort amount) => Inventory.ServerSetAmmo(itemType, amount);

    public void AddAmmo(ItemType itemType, ushort amount) => Inventory.ServerAddAmmo(itemType, amount);

    public void DropAmmo(ItemType itemType, ushort amount, bool checkMinimals = false) => Inventory.ServerDropAmmo(itemType, amount, checkMinimals);

    public void DropEverything() => Inventory.ServerDropEverything();

    public void Mute(bool isTemporary = true)
    {
        if (isTemporary)
        {
            VoiceChatMutes.SetFlags(ReferenceHub, VcMuteFlags.GlobalRegular);
            return;
        }
        
        VoiceChatMutes.IssueLocalMute(UserId);
    }

    public void IntercomMute(bool isTemporary)
    {
        if (isTemporary)
        {
            VoiceChatMutes.SetFlags(ReferenceHub, VcMuteFlags.GlobalRegular);
            return;
        }
        
        VoiceChatMutes.IssueLocalMute(UserId, true);
    }

    public void Unmute(bool removeMute = true)
    {
        if (removeMute)
        {
            VoiceChatMutes.RevokeLocalMute(UserId);
            return;
        }
        
        VoiceChatMutes.SetFlags(ReferenceHub, VcMuteFlags.None);
    }

    public void IntercomUnmute(bool removeMute = true)
    {
        if (removeMute)
        {
            VoiceChatMutes.RevokeLocalMute(UserId, true);
            return;
        }
        
        VoiceChatMutes.SetFlags(ReferenceHub, VcMuteFlags.None);
    }
    
    public void ClearInventory(bool onlyItems = false)
    {
        ClearItems();

        if (onlyItems)
            return;
        
        ClearAmmo();
    }

    public void ClearItems()
    {
        foreach (ItemBase item in Items.Values.ToArray())
        {
            RemoveItem(item);
        }
    }
    
    public IEnumerable<CursedItem> ClearItemsWithoutDestroying()
    {
        IEnumerable<CursedItem> items = GetItems().ToArray();

        foreach (CursedItem item in items)
        {
            if (item.Base is IAcquisitionConfirmationTrigger acquisitionConfirmationTrigger)
            {
                acquisitionConfirmationTrigger.AcquisitionAlreadyReceived = false;
            }
        }
        
        Inventory.NetworkCurItem = ItemIdentifier.None;
        Inventory.UserInventory.Items.Clear(); 
        Inventory.SendItemsNextFrame = true;

        return items;
    }

    public void ClearAmmo()
    {
        foreach (ItemType item in Ammo.Keys.ToArray())
        {
            SetAmmo(item, 0);
        }
    }

    public void AddItems(IEnumerable<ItemType> items)
    {
        foreach (ItemType item in items)
        {
            AddItemBase(item);
        }
    }
    
    public void SetItems(IEnumerable<ItemType> items)
    {
        ClearItems();
        AddItems(items);
    }

    public void AddAmmo(Dictionary<ItemType, ushort> ammo)
    {
        foreach (KeyValuePair<ItemType, ushort> item in ammo)
        {
            AddAmmo(item.Key, item.Value);
        }
    }
    
    public void SetAmmo(Dictionary<ItemType, ushort> ammo)
    {
        ClearAmmo();
        AddAmmo(ammo);
    }

    public void SetInventory(IEnumerable<ItemType> items, Dictionary<ItemType, ushort> ammo)
    {
        SetItems(items);
        SetAmmo(ammo);
    }
    
    public void SendHitMarker(float size = 2.55f) => Hitmarker.SendHitmarker(ReferenceHub, size);

    public void SendWarheadPanelLeverSound() => PlayerInteract.RpcLeverSound();
    
    public bool HasPermission(PlayerPermissions permission) => PermissionsHandler.IsPermitted(Permissions, permission);
    
    public bool HasPermissions(PlayerPermissions permissions) => PermissionsHandler.IsPermitted(Permissions, permissions);

    public bool TryRaycast(float maxDistance, int layerMask, out RaycastHit hit) => Physics.Raycast(new Ray(PlayerCameraReference.position, PlayerCameraReference.forward), out hit, maxDistance, layerMask);
  
    public bool TryRaycast(int layerMask, out RaycastHit hit) => Physics.Raycast(new Ray(PlayerCameraReference.position, PlayerCameraReference.forward), out hit, layerMask);
   
    public bool TryRaycast(float maxDistance, out RaycastHit hit) => Physics.Raycast(new Ray(PlayerCameraReference.position, PlayerCameraReference.forward), out hit, maxDistance);
   
    public bool TryRaycast(out RaycastHit hit) => Physics.Raycast(new Ray(PlayerCameraReference.position, PlayerCameraReference.forward), out hit);
    
    public int RaycastNonAlloc(float maxDistance, int layerMask, RaycastHit[] hits) => Physics.RaycastNonAlloc(new Ray(PlayerCameraReference.position, PlayerCameraReference.forward), hits, maxDistance, layerMask);
  
    public int RaycastNonAlloc(int layerMask, RaycastHit[] hits) => Physics.RaycastNonAlloc(new Ray(PlayerCameraReference.position, PlayerCameraReference.forward), hits, layerMask);
   
    public int RaycastNonAlloc(float maxDistance, RaycastHit[] hits) => Physics.RaycastNonAlloc(new Ray(PlayerCameraReference.position, PlayerCameraReference.forward), hits, maxDistance);
    
    public int RaycastNonAlloc(RaycastHit[] hits) => Physics.RaycastNonAlloc(new Ray(PlayerCameraReference.position, PlayerCameraReference.forward), hits);

    public Stopwatch GetTimeHoldingItem() => Inventory._lastEquipSw;

    private void SetUp()
    {
        int index = UserId.LastIndexOf('@');

        if (index == -1)
        {
            RawUserId = UserId;
            AuthenticationType = AuthenticationType.Other;
            return;
        }
        
        RawUserId = UserId.Substring(0, index);

        AuthenticationType = UserId.Substring(index + 1) switch
        {
            "steam" => AuthenticationType.Steam,
            "discord" => AuthenticationType.Discord,
            "northwood" => AuthenticationType.NorthWood,
            _ => AuthenticationType.Other,
        };
    }
}