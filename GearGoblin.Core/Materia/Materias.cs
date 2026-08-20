// Materia/Materias.cs
// Core materia model and catalog used by the optimizer.
//
// Actual existing meld values come from Lumina at inventory-read time.
// Hypothetical/new meld projections use GearGoblin.Core.MateriaTiers so
// plugin and web share one tier-value table instead of maintaining copies.

namespace GearGoblin.Core.Materia;

/// <summary>
/// Displayed materia tiers. FFXIVClientStructs inventory grade bytes are
/// zero-indexed: grade 0 -> Tier I, grade 11 -> Tier XII.
/// </summary>
public enum MateriaTier
{
    Unknown = 0,
    Tier1  = 1,
    Tier2  = 2,
    Tier3  = 3,
    Tier4  = 4,
    Tier5  = 5,
    Tier6  = 6,
    Tier7  = 7,
    Tier8  = 8,
    Tier9  = 9,
    Tier10 = 10,
    Tier11 = 11,
    Tier12 = 12,
}

public static class MateriaTierExt
{
    public static string Roman(this MateriaTier t) =>
        GearGoblin.Core.MateriaTiers.RomanNumeral((int)t);
}

public readonly record struct MateriaSpec(
    MateriaTier Tier,
    Substat     Stat,
    int         Value
)
{
    public string Display() => $"{Stat.Short()} {Tier.Roman()} (+{Value})";
}

public static class MateriaCatalog
{
    /// <summary>Default combat materia tier recommended at level 100.</summary>
    public const MateriaTier CurrentEndgameTier = MateriaTier.Tier12;

    /// <summary>
    /// Project the value of a combat-substat materia. Existing melds do not use
    /// this method; their values are read directly from Lumina by InventoryReader.
    /// </summary>
    public static int ValueOf(MateriaTier tier, Substat stat)
    {
        if (tier == MateriaTier.Unknown || stat == Substat.None || stat.IsCraftGather())
            return 0;

        return GearGoblin.Core.MateriaTiers.SubstatValue((int)tier);
    }

    public static MateriaSpec Spec(MateriaTier tier, Substat stat) =>
        new(tier, stat, ValueOf(tier, stat));

    /// <summary>
    /// Convert an in-game stat name plus FFXIVClientStructs zero-indexed grade
    /// into an internal materia specification. The supplied statValue is the
    /// authoritative Lumina value for an existing meld.
    /// </summary>
    public static MateriaSpec FromGrade(string statName, byte grade, int statValue)
    {
        var stat = StatNameToSubstat(statName);
        var tier = grade <= 11 ? (MateriaTier)(grade + 1) : MateriaTier.Unknown;
        return new MateriaSpec(tier, stat, statValue);
    }

    /// <summary>Map the canonical in-game BaseParam display name to a substat.</summary>
    public static Substat StatNameToSubstat(string name) => name switch
    {
        "Critical Hit"    => Substat.CriticalHit,
        "Determination"   => Substat.Determination,
        "Direct Hit Rate" => Substat.DirectHit,
        "Direct Hit"      => Substat.DirectHit,
        "Skill Speed"     => Substat.SkillSpeed,
        "Spell Speed"     => Substat.SpellSpeed,
        "Tenacity"        => Substat.Tenacity,
        "Piety"           => Substat.Piety,
        "Craftsmanship"   => Substat.Craftsmanship,
        "Control"         => Substat.Control,
        "CP"              => Substat.CP,
        "Gathering"       => Substat.Gathering,
        "Perception"      => Substat.Perception,
        "GP"              => Substat.GP,
        _                  => Substat.None,
    };
}
