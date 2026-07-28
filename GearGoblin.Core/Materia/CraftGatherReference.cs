// Materia/CraftGatherReference.cs
// Approximate current-tier fully-melded reference values per DoH/DoL stat —
// the soft maximum the Character-tab fill bars fill against. Same spirit as
// the battle gauges' rough tier ceilings (CharacterTab.DrawGauges constants).
//
// These are NOT caps. Sourced from patch 7.5x community crafting/gathering
// BiS lists (i750 crafted set, pentamelded) and labeled "approx" in the UI;
// expect drift each gear tier. Operator-tunable in one place here.

namespace GearGoblin.Core.Materia;

public static class CraftGatherReference
{
    /// <summary>
    /// Approximate fully-melded endgame value for a DoH/DoL stat, or 0 for
    /// any other stat. UI treats 0 as "no reference — show value only".
    /// </summary>
    public static int SoftMax(Substat s) => s switch
    {
        Substat.Craftsmanship => 5600,
        Substat.Control       => 5400,
        Substat.CP            => 700,
        Substat.Gathering     => 5100,
        Substat.Perception    => 5000,
        Substat.GP            => 1000,
        _                     => 0,
    };
}
