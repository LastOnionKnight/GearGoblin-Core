# GearGoblin.Core

**Current released version: 1.6.2**  
**Current `main`: unreleased 1.7 solver foundation work**

`GearGoblin.Core` is the shared business-logic library used by both Tonberry Tactics front ends:

- `LastOnionKnight/GearGoblin` — Dalamud plugin
- `LastOnionKnight/TonberryTactics` — Blazor WebAssembly companion

Its job is to keep the plugin and web mathematically consistent. Formula logic, job profiles, materia data, cap math, schemas, and optimizer behavior live here instead of being independently reimplemented in each consumer.

## Current role

Core currently owns:

- job/role profiles
- relevant battle stats and Balance-style weights
- level modifiers
- Crit/DH/Det/Speed/Tenacity/Piety formula helpers
- materia tier/value data
- stat caps
- normalized `StatSnapshot`
- meld-slot / piece models
- shared meld optimizer and audit engine
- export schema types
- stat-name canonicalization
- DoH/DoL reference/display support

Important files include:

```text
JobPriorities.cs
ExportSchema.cs
StatNames.cs
Materia/JobProfile.cs
Materia/Formulas.cs
Materia/LevelTable.cs
Materia/Materias.cs
Materia/StatCaps.cs
Materia/StatSnapshot.cs
Materia/MeldSlots.cs
Materia/MeldOptimizer.cs
Materia/CraftGatherReference.cs
```

## Materia value authority

The current codebase has one authoritative combat-materia projection path.

Current endgame combat projection:

```text
Materia XII = +54
```

`MateriaCatalog` derives projected values from the shared `MateriaTiers` table. The old duplicate projection table that could return `+96` for Tier XII has been retired.

## Level 100 constants

Current level-100 values remain:

```text
MAIN = 440
SUB  = 420
DIV  = 2780
```

These constants feed the shared battle-stat formula layer.

## Optimizer status

`MeldOptimizer` is a useful shared materia recommendation/audit engine. It currently supports:

- empty-slot recommendations
- per-piece cap enforcement
- current-meld auditing
- wrong/zero-value stat detection
- overcap detection
- outdated/replacement recommendations
- Pure Math and Balance-weight modes
- guarded DoH/DoL handling

It is **not yet the final Ask Mr. Robot-style gearset solver**.

The next architecture step is to replace generic cross-stat ranking with a normalized expected-output objective that can evaluate complete gearsets under job/GCD/encounter constraints. Raider food/potion solving will be the first visible feature built on that foundation.

## Supported jobs

Standard combat jobs:

- Tanks: PLD, WAR, DRK, GNB
- Healers: WHM, SCH, AST, SGE
- Melee: MNK, DRG, NIN, SAM, RPR, VPR
- Physical ranged: BRD, MCH, DNC
- Magical ranged: BLM, SMN, RDM, PCT

DoH/DoL ClassJob IDs are represented for correct identification/display. Dedicated crafting/gathering optimization is intentionally not implemented yet.

## Versioning

GearGoblin, GearGoblin.Core, and TonberryTactics use **trinity lockstep** for releases.

Current tagged release:

```text
GearGoblin plugin     1.6.2
GearGoblin.Core       1.6.2
TonberryTactics web   1.6.2
```

Current `main` may contain unreleased stabilization fixes ahead of that tag. Release notes/changelogs should distinguish tagged releases from `Unreleased` work rather than rewriting history.

## Consumer layout

Both consumers mount Core at:

```text
external/GearGoblin.Core/
```

and reference:

```xml
<ProjectReference Include="external\GearGoblin.Core\GearGoblin.Core.csproj" />
```

Fresh consumer clone:

```powershell
git submodule update --init --recursive
```

The old sibling-folder development layout is retired.

## Target framework

```text
netstandard2.0
```

This keeps Core portable between the .NET 10 Dalamud plugin and .NET 10 Blazor WebAssembly app.

## Build

```powershell
dotnet restore .\GearGoblin.Core\GearGoblin.Core.csproj
dotnet build .\GearGoblin.Core\GearGoblin.Core.csproj -c Release
```

A full product validation must also rebuild both consumers at the exact intended Core submodule commit.

## Core intentionally does not own

- Dalamud service access
- live inventory/player reads
- Lumina enumeration tied to a running game client
- Etro/XIVGear HTTP fetching
- ImGui or Blazor UI
- Tonberry Tactics textures/fonts/chrome

Those remain consumer responsibilities.

## Current known debt

- normalized expected-DPS/output objective for cross-stat comparison
- GCD-constrained full-set solving
- Raider food/potion scoring contracts
- full candidate-gear / Best-in-Bags solving
- acquisition/currency optimization contracts
- DoH/DoL optimization formulas and objectives
- future round-trip schema consolidation as new plan data is added

## License

MIT.
