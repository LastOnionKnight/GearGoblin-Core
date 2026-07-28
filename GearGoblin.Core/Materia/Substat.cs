// Materia/Substat.cs
// The substats GearGoblin's advisor reasons about, mapped to FFXIV BaseParam IDs.
// BaseParam IDs are stable game data, datamined and verified across patches:
//   https://github.com/xivapi/ffxiv-datamining/blob/master/csv/BaseParam.csv
//
// v1.5.8 adds the six Disciple of the Hand / Land stats (crafting-gathering
// spec, docs/specs/2026-07-07-crafting-gathering-stats-design.md). These are
// main/pool stats, not battle substats — no battle formulas apply; the
// optimizer treats DoH/DoL jobs as display-only this release.

namespace GearGoblin.Core.Materia;

public enum Substat
{
    None = 0,
    CriticalHit = 27,
    Determination = 44,
    DirectHit = 22,
    SkillSpeed = 45,
    SpellSpeed = 46,
    Tenacity = 19,
    Piety = 6,

    // Disciple of the Hand
    CP = 11,
    Craftsmanship = 70,
    Control = 71,

    // Disciple of the Land
    GP = 10,
    Gathering = 72,
    Perception = 73,
}

public static class SubstatExt
{
    /// <summary>Display name for UI.</summary>
    public static string Display(this Substat s) => s switch
    {
        Substat.CriticalHit   => "Critical Hit",
        Substat.Determination => "Determination",
        Substat.DirectHit     => "Direct Hit",
        Substat.SkillSpeed    => "Skill Speed",
        Substat.SpellSpeed    => "Spell Speed",
        Substat.Tenacity      => "Tenacity",
        Substat.Piety         => "Piety",
        Substat.Craftsmanship => "Craftsmanship",
        Substat.Control       => "Control",
        Substat.CP            => "CP",
        Substat.Gathering     => "Gathering",
        Substat.Perception    => "Perception",
        Substat.GP            => "GP",
        _                     => "Unknown",
    };

    /// <summary>Short name for compact tables.</summary>
    public static string Short(this Substat s) => s switch
    {
        Substat.CriticalHit   => "Crit",
        Substat.Determination => "Det",
        Substat.DirectHit     => "DH",
        Substat.SkillSpeed    => "SkS",
        Substat.SpellSpeed    => "SpS",
        Substat.Tenacity      => "Ten",
        Substat.Piety         => "Pie",
        Substat.Craftsmanship => "Cms",
        Substat.Control       => "Ctl",
        Substat.CP            => "CP",
        Substat.Gathering     => "Gat",
        Substat.Perception    => "Per",
        Substat.GP            => "GP",
        _                     => "??",
    };

    /// <summary>True for the six DoH/DoL stats — no battle formulas apply.</summary>
    public static bool IsCraftGather(this Substat s) => s is
        Substat.Craftsmanship or Substat.Control or Substat.CP or
        Substat.Gathering or Substat.Perception or Substat.GP;
}
