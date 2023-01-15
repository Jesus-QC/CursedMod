using System.Collections.Generic;
using CursedMod.Features.Enums;
using CursedMod.Features.Wrappers.Player;
using Hazards;
using UnityEngine;

namespace CursedMod.Features.Wrappers.Facility.Hazards;

public class CursedEnvironmentalHazard
{
    public EnvironmentalHazard EnvironmentalHazard { get; }

    public CursedEnvironmentalHazard(EnvironmentalHazard hazard)
    {
        EnvironmentalHazard = hazard;

        HazardType = EnvironmentalHazard switch
        {
            SinkholeEnvironmentalHazard => EnvironmentalHazardType.Sinkhole,
            TantrumEnvironmentalHazard => EnvironmentalHazardType.Tantrum,
            TemporaryHazard => EnvironmentalHazardType.Temporary,
            _ => EnvironmentalHazardType.Other
        };
    }

    public List<ReferenceHub> AffectedPlayers => EnvironmentalHazard.AffectedPlayers;

    public IEnumerable<CursedPlayer> GetAffectedPlayers()
    {
        foreach (ReferenceHub hub in AffectedPlayers)
        {
            if(!CursedPlayer.TryGet(hub, out CursedPlayer ply))
                continue;
                
            yield return ply;
        }
    }

    public EnvironmentalHazardType HazardType { get; }

    public float MaxDistance
    {
        get => EnvironmentalHazard.MaxDistance;
        set => EnvironmentalHazard.MaxDistance = value;
    }

    public float MaxHeightDistance
    {
        get => EnvironmentalHazard.MaxHeightDistance;
        set => EnvironmentalHazard.MaxHeightDistance = value;
    }

    public Vector3 SourceOffset
    {
        get => EnvironmentalHazard.SourceOffset;
        set => EnvironmentalHazard.SourceOffset = value;
    }
    
    public Vector3 SourcePosition
    {
        get => EnvironmentalHazard.SourcePosition;
        set => EnvironmentalHazard.SourcePosition = value;
    }

    public bool IsActive => EnvironmentalHazard.IsActive;

    public bool IsInArea(Vector3 sourcePosition, Vector3 targetPosition) => EnvironmentalHazard.IsInArea(sourcePosition, targetPosition);
    
    public bool IsInArea(Vector3 targetPosition) => EnvironmentalHazard.IsInArea(SourcePosition, targetPosition);
}