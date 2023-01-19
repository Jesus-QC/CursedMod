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
        HazardType = EnvironmentalHazardType.Other;
    }

    public static CursedEnvironmentalHazard Get(EnvironmentalHazard environmentalHazard)
    {
        return environmentalHazard switch
        {
            SinkholeEnvironmentalHazard sinkholeEnvironmentalHazard => new CursedSinkholeHazard(sinkholeEnvironmentalHazard),
            TantrumEnvironmentalHazard tantrumEnvironmentalHazard => new CursedTantrumHazard(tantrumEnvironmentalHazard),
            TemporaryHazard temporaryHazard => new CursedTemporaryHazard(temporaryHazard),
            _ => new CursedEnvironmentalHazard(environmentalHazard)
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

    public EnvironmentalHazardType HazardType { get; internal set; }

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