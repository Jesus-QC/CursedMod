using System.Collections.Generic;
using System.Linq;
using CursedMod.Features.Enums;
using CursedMod.Features.Wrappers.Facility;
using CursedMod.Features.Wrappers.Player.Dummies;
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
using RemoteAdmin;
using Security;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Player;

public class CursedPlayer
{
    public static readonly Dictionary<ReferenceHub, CursedPlayer> Dictionary = new ();
    public static IEnumerable<CursedPlayer> Collection => Dictionary.Values;
    public static List<CursedPlayer> List => Collection.ToList();
    public static int Count => Dictionary.Count;
    
    public ReferenceHub ReferenceHub { get; }
    public GameObject GameObject { get; private set; }
    public Transform Transform { get; internal set; }

    public AuthenticationType AuthenticationType { get; private set; }
    
    public string RawUserId { get; private set; }
    
    public Transform PlayerCameraReference => ReferenceHub.PlayerCameraReference;

    public NetworkIdentity NetworkIdentity => ReferenceHub.networkIdentity;

    public NetworkConnectionToClient NetworkConnection => NetworkIdentity.connectionToClient;

    public CharacterClassManager CharacterClassManager => ReferenceHub.characterClassManager;

    public PlayerRoleManager RoleManager => ReferenceHub.roleManager;

    public PlayerStats PlayerStats => ReferenceHub.playerStats;

    public Inventory Inventory => ReferenceHub.inventory;

    public Dictionary<ushort, ItemBase> Items => Inventory.UserInventory.Items;

    public Dictionary<ItemType, ushort> ReserveAmmo => Inventory.UserInventory.ReserveAmmo;

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
    
    public string SaltedUserId => CharacterClassManager.SaltedUserId;
    
    public bool IsVerified => ServerRoles.IsVerified;
    
    public string GroupName => ServerRoles.GetUncoloredRoleString();

    public byte KickPower => ServerRoles.KickPower;
    
    public bool IsDummy => this is CursedDummy;
    
    public bool IsDead => Role is RoleTypeId.Spectator or RoleTypeId.Overwatch or RoleTypeId.None;

    public bool IsAlive => !IsDead;

    public bool IsFoundationForce => RoleManager.CurrentRole.Team is Team.FoundationForces;

    public bool IsChaos => RoleManager.CurrentRole.Team is Team.ChaosInsurgency;

    public bool IsScp => RoleManager.CurrentRole.Team is Team.SCPs;

    public bool IsTutorial => Role is RoleTypeId.Tutorial;

    public bool IsHuman => !IsScp || !IsDead;
    
    public float TimeHoldingCurrentItem => Inventory.LastItemSwitch;

    public bool TryGetEffect(string effectName, out StatusEffectBase effect) => PlayerEffectsController.TryGetEffect(effectName, out effect);
    
    public bool TryGetEffect<T>(out T effect) where T : StatusEffectBase => PlayerEffectsController.TryGetEffect(out effect);

    public StatusEffectBase ChangeState(string effectName, byte intensity, float duration = 0f, bool addDuration = false) => PlayerEffectsController.ChangeState(effectName, intensity, duration, addDuration);

    public T ChangeState<T>(byte intensity, float duration = 0f, bool addDuration = false) where T : StatusEffectBase => PlayerEffectsController.ChangeState<T>(intensity, duration, addDuration);

    public T EnableEffect<T>(float duration = 0f, bool addDuration = false) where T : StatusEffectBase =>
        PlayerEffectsController.EnableEffect<T>(duration, addDuration);
    
    public T DisableEffect<T>() where T : StatusEffectBase => PlayerEffectsController.DisableEffect<T>();

    public void DisableAllEffects() => PlayerEffectsController.DisableAllEffects();
    
    public void OpenRemoteAdmin() => ServerRoles.TargetOpenRemoteAdmin(true);

    public void CloseRemoteAdmin() => ServerRoles.TargetCloseRemoteAdmin();
    
    public void ToggleOverWatch() => IsInOverWatch = !IsInOverWatch;
    
    public void SyncServerCommandBinds() => CharacterClassManager.SyncServerCmdBinding();
    
    public void SendCommandBind(KeyCode code, string command) => CharacterClassManager.TargetChangeCmdBinding(NetworkConnection, code, command);

    public void SendConsoleMessage(string text, string color) => GameConsoleTransmission.SendToClient(NetworkConnection, text, color);

    public void Disconnect(string message) => CharacterClassManager.DisconnectClient(NetworkConnection, message);

    public void ShowTag(bool global) => CharacterClassManager.UserCode_CmdRequestShowTag(global);
    
    public void HideTag() => CharacterClassManager.UserCode_CmdRequestHideTag();

    public void ShowHint(string content, int time = 5) => ShowHint(new TextHint(content, new HintParameter[] { new StringHintParameter(string.Empty) }, null, 2));
    
    public void ShowHint(Hint hint) => HintDisplay.Show(hint);
    
    public void ClearBroadcasts() => CursedFacility.Broadcast.TargetClearElements(NetworkConnection);
    
    public void ShowBroadcast(string message, ushort duration = 5, Broadcast.BroadcastFlags flags = Broadcast.BroadcastFlags.Normal) => CursedFacility.Broadcast.TargetAddElement(NetworkConnection, message, duration, flags);
    
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
        get => CurrentRole.RoleTypeId;
        set => SetRole(value);
    }
    
    public void SetRole(RoleTypeId role, RoleChangeReason reason = RoleChangeReason.RemoteAdmin, RoleSpawnFlags flags = RoleSpawnFlags.All) => RoleManager.ServerSetRole(role, reason, flags);
    
    public PlayerRoleBase CurrentRole
    {
        get => RoleManager.CurrentRole;
        set => RoleManager.CurrentRole = value;
    }

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

    public void SetStableGroup(string name)
    {
        ServerRoles.SetGroup(ServerStatic.GetPermissionsHandler().GetGroup(name), true);

        if (ServerStatic.GetPermissionsHandler()._members.ContainsKey(UserId))
        {
            ServerStatic.GetPermissionsHandler()._members[UserId] = name;
            return;
        }

        ServerStatic.GetPermissionsHandler()._members.Add(UserId, name);
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
    
    public bool GodMode
    {
        get => CharacterClassManager.GodMode;
        set => CharacterClassManager.GodMode = value;
    }
    
    public bool Cuffed => Inventory.IsDisarmed();

    public T AddComponent<T>() where T : MonoBehaviour => GameObject.AddComponent<T>();
    public T GetComponent<T>() where T : MonoBehaviour => GameObject.GetComponent<T>();

    public bool Kick(string reason) => BanPlayer.KickUser(ReferenceHub, reason);

    public bool Ban(string reason, long duration) => BanPlayer.BanUser(ReferenceHub, reason, duration);

    public void SetScene(string sceneName) => NetworkConnection.Send(new SceneMessage { sceneName = sceneName });

    public void SendEscapeInformation(Escape.EscapeMessage message) => NetworkConnection.Send(message);

    public ItemBase AddItem(ItemType itemType) => Inventory.ServerAddItem(itemType);

    public void DropItem(ItemBase pickupBase) => Inventory.ServerDropItem(pickupBase.ItemSerial);

    public void DropItem(ItemPickupBase pickupBase) => Inventory.ServerDropItem(pickupBase.Info.Serial);

    public void RemoveItem(ItemBase itemBase) => Inventory.ServerRemoveItem(itemBase.ItemSerial, itemBase.PickupDropModel);

    public void RemoveItem(ItemPickupBase pickupBase) => Inventory.ServerRemoveItem(pickupBase.Info.Serial, pickupBase);

    public void SetAmmo(ItemType itemType, ushort amount) => Inventory.ServerSetAmmo(itemType, amount);

    public void AddAmmo(ItemType itemType, ushort amount) => Inventory.ServerAddAmmo(itemType, amount);

    public void DropAmmo(ItemType itemType, ushort amount, bool checkMinimals = false) => Inventory.ServerDropAmmo(itemType, amount, checkMinimals);

    public void DropEverything() => Inventory.ServerDropEverything();

    public void ClearInventory(bool onlyItem = false)
    {
        foreach (ItemBase item in Inventory.UserInventory.Items.Values)
        {
            RemoveItem(item);
        }
        if (!onlyItem)
        {
            foreach (ItemType item in Inventory.UserInventory.ReserveAmmo.Keys)
            {
                SetAmmo(item, 0);
            }
        }
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

    public void SendHitMarker(float size = 2.55f) => Hitmarker.SendHitmarker(ReferenceHub, size);

    public PlayerInfoArea PlayerInfoArea
    {
        get => NicknameSync._playerInfoToShow;
        set => NicknameSync.Network_playerInfoToShow = value;
    }

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
    
    internal CursedPlayer(ReferenceHub hub, bool dummy = false)
    {
        ReferenceHub = hub;
        SetUp(!dummy);
        
        if (dummy)
            return;
        
        Dictionary.Add(hub, this);
    }
    
    private void SetUp(bool auth)
    {
        GameObject = ReferenceHub.gameObject;
        Transform = ReferenceHub.transform;
        
        if(!auth)
            return;
        
        SetUpAuth();
    }
    
    private void SetUpAuth()
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

    public static bool TryGet(ReferenceHub hub, out CursedPlayer player)
    {
        if (hub is not null && Dictionary.ContainsKey(hub))
        {
            player = Dictionary[hub];
            return false;
        }

        player = null;
        return false;
    }

    public static CursedPlayer Get(ReferenceHub hub) => TryGet(hub, out CursedPlayer player) ? player : CursedServer.LocalPlayer;

}