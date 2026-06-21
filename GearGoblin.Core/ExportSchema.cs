using System.Collections.Generic;

namespace GearGoblin.Core;

// =========================================================================
// Wire-format DTOs for Gearset Export. 
// Schema is versioned via the GG-EXPORT:v1: prefix; new schema versions
// get new record types (ExportPayloadV2, etc.) rather than mutating these.
// =========================================================================

public sealed record ExportPayloadV1(
    int                  V,
    string               Plugin,
    string               Version,
    string               ExportedAt,
    ExportCharacterV1    Character,
    List<ExportPieceV1>  Equipped
);

public sealed record ExportCharacterV1(
    uint   Job,
    string JobAbbreviation,
    int    Level,
    int    AverageItemLevel
);

public sealed record ExportPieceV1(
    string                  Slot,
    uint                    ItemId,
    string                  Name,
    uint                    ItemLevel,
    bool                    IsHighQuality,
    byte                    MateriaSlotCount,
    bool                    IsOvermeldAllowed,
    List<ExportMateriaV1>   Materia,
    uint                    SubstatCap = 0,
    Dictionary<string, int>? BaseSubstats = null
);

public sealed record ExportMateriaV1(
    int    SlotIndex,
    ushort MateriaId,
    byte   Grade,
    string StatName,
    int    StatValue
);

// =========================================================================
// V2 Schema - Introduces TotalStats from the character sheet.
// =========================================================================

public sealed record ExportPayloadV2(
    int                  V,
    string               Plugin,
    string               Version,
    string               ExportedAt,
    ExportCharacterV2    Character,
    List<ExportPieceV1>  Equipped
);

public sealed record ExportCharacterV2(
    uint   Job,
    string JobAbbreviation,
    int    Level,
    int    AverageItemLevel,
    List<TotalStat> TotalStats
);

public sealed record TotalStat(
    string DisplayName,
    int Value,
    int? Cap
);

public static class Caps
{
    // Pure logic only. No Lumina, no game sheets.
    public static bool HasNoCap(string displayName) =>
        displayName == "Determination" || displayName == "Vitality" || displayName == "Strength" || displayName == "Dexterity" || displayName == "Intelligence" || displayName == "Mind";

    public static bool IsOver(TotalStat s) =>
        s.Cap is int cap && s.Value > cap;

    public static double PercentOfCap(TotalStat s) =>
        s.Cap is int cap && cap > 0 ? (double)s.Value / cap : 0.0;
}
