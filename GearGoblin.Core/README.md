# GearGoblin.Core

**Current version: 1.6.1**

`GearGoblin.Core` is the shared business-logic library used by both halves of the Tonberry Tactics system:

- `LastOnionKnight/GearGoblin` — Dalamud plugin
- `LastOnionKnight/TonberryTactics` — Blazor WebAssembly companion

Its purpose is simple: the plugin and web app must produce the same answer from the same gear data. Formula logic, job profiles, materia data, cap math, and optimizer behavior therefore live here instead of being independently reimplemented in each front end.

## Current role

Core is no longer just a future contract layer. In the current 1.6.1 codebase it contains the shared optimizer and formula system used by the plugin and web companion.

Important areas include:

- `JobPriorities.cs` — shared job-priority/reference data
- `Materia/JobProfile.cs` — job IDs, roles, relevant stats, and Balance-style weights
- `Materia/Formulas.cs` — battle-stat derivation helpers
- `Materia/LevelTable.cs` — level modifiers
- `Materia/Materias.cs` / `MateriaTiers.cs` — materia catalog and tier values
- `Materia/StatCaps.cs` — cap helpers
- `Materia/StatSnapshot.cs` — normalized character stat input
- `Materia/MeldSlots.cs` — optimizer slot/piece models
- `Materia/MeldOptimizer.cs` — shared recommendation + audit engine
- `ExportSchema.cs` — shared export/round-trip data contracts used by the current system
- `StatNames.cs` — canonical stat-name handling
- `Materia/CraftGatherReference.cs` — current DoH/DoL display reference values

## Supported jobs

The shared job profile table currently covers the 21 standard combat jobs:

- Tanks: PLD, WAR, DRK, GNB
- Melee: MNK, DRG, NIN, SAM, RPR, VPR
- Physical ranged: BRD, MCH, DNC
- Magical ranged: BLM, SMN, RDM, PCT
- Healers: WHM, SCH, AST, SGE

DoH/DoL ClassJob IDs are also represented so the plugin/web can identify and display crafting and gathering stats correctly. Their meld optimization is intentionally display-only for now; battle formulas are not applied to crafting/gathering gear.

## Optimizer behavior

`MeldOptimizer` currently provides:

- empty-slot recommendations
- per-piece substat-cap enforcement
- current-meld auditing
- wrong-stat / zero-value detection
- overcap detection
- outdated/replacement recommendations
- projected gain scoring
- Pure Math and Balance-weight modes
- guarded behavior for DoH/DoL jobs

The plugin and web companion both consume this shared implementation.

## Versioning

GearGoblin, GearGoblin.Core, and TonberryTactics follow **trinity lockstep** versioning.

Current release:

```text
GearGoblin plugin     1.6.1
GearGoblin.Core       1.6.1
TonberryTactics web   1.6.1
```

If one component intentionally diverges, that should be documented explicitly. Otherwise mismatched product versions indicate a release problem.

## Repository / submodule layout

Core lives in its own repository and is consumed by both front ends as a git submodule at the same relative path:

```text
external/GearGoblin.Core/
```

The consuming projects reference:

```xml
<ProjectReference Include="external\GearGoblin.Core\GearGoblin.Core.csproj" />
```

For a fresh clone of either consumer:

```powershell
git submodule update --init --recursive
```

The old sibling-folder development layout is retired and should not be used as the documented setup.

## Target framework

Core targets:

```text
netstandard2.0
```

This keeps the shared library consumable by both the .NET 10 Dalamud plugin and the .NET 10 Blazor WebAssembly app.

## Build

From this repository root:

```powershell
dotnet restore .\GearGoblin.Core\GearGoblin.Core.csproj
dotnet build .\GearGoblin.Core\GearGoblin.Core.csproj -c Release
```

To validate the complete product, also build both consumers with their Core submodules initialized.

## What Core intentionally does not own

Core is UI-agnostic and platform-agnostic. It does not contain:

- Dalamud services or ImGui code
- live FFXIV inventory access
- live PlayerState reads
- Etro/XIVGear HTTP fetching
- Blazor components or browser state
- game textures, fonts, or Tonberry Tactics UI chrome

Those responsibilities remain with the appropriate consumer.

## Current known debt

- DoH/DoL optimization is display-only pending dedicated crafting/gathering formulas and scoring rules.
- The next Raider consumables feature will require a shared scoring model while live game-data enumeration remains plugin-side.
- Future plan schema revisions should continue to centralize contracts here where practical so plugin/web serializers cannot drift.

## Release

`release.ps1` is retained for lockstep release work. The normal expectation is that version bumps across Core, plugin, and web are coordinated.

## License

MIT.

Tonberry Tactics / GearGoblin project by LastOnionKnight.
