namespace GearGoblin.Core.Materia
{
    public readonly record struct StatSnapshot(
        int Crit,
        int Det,
        int DH,
        int SkS,
        int SpS,
        int Ten,
        int Pie,
        int Level,
        uint JobId
    );
}
