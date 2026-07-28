namespace GearGoblin.Core.Materia
{
    // v1.5.8: the six DoH/DoL stats trail the battle stats as defaulted
    // positional params so every existing 9-argument construction keeps
    // compiling. Battle jobs leave them 0; crafters/gatherers leave the
    // battle substats at their (base) values.
    public readonly record struct StatSnapshot(
        int Crit,
        int Det,
        int DH,
        int SkS,
        int SpS,
        int Ten,
        int Pie,
        int Level,
        uint JobId,
        int Craftsmanship = 0,
        int Control = 0,
        int CP = 0,
        int Gathering = 0,
        int Perception = 0,
        int GP = 0
    );
}
