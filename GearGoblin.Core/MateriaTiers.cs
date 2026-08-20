// GearGoblin.Core/MateriaTiers.cs
//
// Shared materia tier -> stat value table used by plugin and web when
// projecting a materia that is not already present in the player's gear.
// Existing melds are read from Lumina and remain the source of truth.

using System.Collections.Generic;

namespace GearGoblin.Core;

/// <summary>
/// Combat substat materia values by displayed tier (I-XII).
/// Values cover Crit, Direct Hit, Determination, Skill/Spell Speed,
/// Tenacity, and Piety materia. Current endgame tier is XII.
/// </summary>
public static class MateriaTiers
{
    // In-game combat-substat materia progression as of Dawntrail / patch 7.x:
    // I +1, II +2, III +3, IV +4, V +6, VI +16,
    // VII +8, VIII +24, IX +12, X +36, XI +18, XII +54.
    //
    // Odd-numbered modern tiers are the lower-value overmeld-friendly tier;
    // even-numbered tiers are the higher-value restricted tier. Do not
    // "smooth" this progression into a monotonic curve.
    private static readonly Dictionary<int, int> SubstatValues = new()
    {
        [1]  = 1,
        [2]  = 2,
        [3]  = 3,
        [4]  = 4,
        [5]  = 6,
        [6]  = 16,
        [7]  = 8,
        [8]  = 24,
        [9]  = 12,
        [10] = 36,
        [11] = 18,
        [12] = 54,
    };

    /// <summary>Highest combat materia tier in the current Dawntrail data set.</summary>
    public const int CurrentCapTier = 12;

    /// <summary>
    /// Return the combat-substat value for a displayed materia tier.
    /// Invalid tiers fall back to the current cap tier so legacy callers
    /// remain non-throwing.
    /// </summary>
    public static int SubstatValue(int tier) =>
        SubstatValues.TryGetValue(tier, out var v)
            ? v
            : SubstatValues[CurrentCapTier];

    public static string RomanNumeral(int tier) => tier switch
    {
        1  => "I",   2  => "II",  3  => "III", 4  => "IV",
        5  => "V",   6  => "VI",  7  => "VII", 8  => "VIII",
        9  => "IX",  10 => "X",   11 => "XI",  12 => "XII",
        _  => tier.ToString(),
    };

    /// <summary>Build the in-game materia item name for a combat substat.</summary>
    public static string NameOf(string statName, int tier)
    {
        var roman = RomanNumeral(tier);
        var prefix = MateriaPrefix(statName);
        return $"{prefix} Materia {roman}";
    }

    public static string MateriaPrefix(string statName) =>
        statName?.Trim() switch
        {
            "Critical Hit"     => "Savage Aim",
            "Direct Hit Rate"  => "Heavens' Eye",
            "Direct Hit"       => "Heavens' Eye",
            "Determination"    => "Savage Might",
            "Skill Speed"      => "Quickarm",
            "Spell Speed"      => "Quicktongue",
            "Tenacity"         => "Battledance",
            "Piety"            => "Piety",
            _                   => statName ?? "Generic",
        };

    public static IEnumerable<int> AllTiers()
    {
        for (var t = 1; t <= CurrentCapTier; t++)
            yield return t;
    }
}
