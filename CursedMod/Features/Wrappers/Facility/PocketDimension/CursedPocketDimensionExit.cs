namespace CursedMod.Features.Wrappers.Facility.PocketDimension;

public class CursedPocketDimensionExit
{
    public PocketDimensionTeleport Base { get; }

    public CursedPocketDimensionExit(PocketDimensionTeleport dimensionTeleport)
    {
        Base = dimensionTeleport;
    }

    public PocketDimensionTeleport.PDTeleportType ExitType
    {
        get => Base._type;
        set => Base._type = value;
    }
}