// Materia/MeldSlots.cs
// Per-piece meld slot model.
//
// v0.3.1 (Bug B): real slot counts now come from the Item sheet via
// EquippedPiece.MateriaSlotCount (guaranteed slots, 0-2) and
// EquippedPiece.IsOvermeldAllowed (can the player add slots 2-4 via overmeld?).
//
// Total slots = MateriaSlotCount + (IsOvermeldAllowed ? extras : 0).
//   - Crafted gear:        2 guaranteed + 3 overmeld = 5
//   - Augmented tomestone: 2 guaranteed + 3 overmeld = 5
//   - Raid drops:          2 guaranteed + 0 overmeld = 2
//   - Job stone, etc.:     0 guaranteed + 0 overmeld = 0
// Overmeld success rates: ~36% / 25% / 20% for slots 2/3/4.

using System.Collections.Generic;
using System.Linq;


namespace GearGoblin.Core.Materia;

public sealed class MeldSlot
{
    public int  SlotIndex   { get; init; }
    public bool IsGuaranteed{ get; init; }
    public MateriaSpec? Current { get; set; }
    public double SuccessRate { get; init; } = 1.0;
    public bool IsEmpty => Current is null;
}

public sealed class MeldablePiece
{
    public EquipSlot Slot      { get; init; }
    public string    Name      { get; init; } = "";
    public uint      ItemId    { get; init; }
    public uint      ItemLevel { get; init; }
    public bool      IsHighQuality { get; init; }
    public List<MeldSlot> Slots { get; init; } = new();
    public Dictionary<Substat, int> CurrentMeldStats { get; init; } = new();
    public Dictionary<Substat, int> BaseSubstats { get; init; } = new();
    public int SubstatCap { get; init; }
    public int EmptySlotCount => Slots.Count(s => s.IsEmpty);
}

public static class MeldSlotsBuilder
{
    /// <summary>Total slots a piece can theoretically support (guaranteed + overmeld).</summary>
    private const int MaxOvermeldSlots = 5;

    private static double SuccessRateForSlot(int slotIndex) => slotIndex switch
    {
        0 => 1.00,
        1 => 1.00,
        2 => 0.36,
        3 => 0.25,
        4 => 0.20,
        _ => 0.10,
    };
}
