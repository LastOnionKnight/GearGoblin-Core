# GearGoblin.Core

Shared business-logic library for [Tonberry Tactics](https://tonberrytactics.pages.dev). 
Both halves of the product — the web app (Blazor WASM) and the in-game 
plugin (Dalamud) — project-reference this library so their stat-priority 
tables, materia-tier values, and BaseParam name canonicalization 
don't drift.

## Why it exists

Up through v0.6.2, the web's `PureMathOptimizer` had its own hardcoded 
GNB-only priority list. The plugin's `MeldOptimizer` had its own 
job-aware logic. When a healer pasted an export, the web recommended 
GNB-tank melds. When the plugin recommended for a Bard, it didn't 
agree with what the web would have produced. **Two halves of the same 
product diverging on the math the user trusts.**

Core fixes that. One library, one source of truth for:

- **`JobPriorities`** — per-job materia stat priority lists, sourced 
  from public BiS guides at [thebalanceffxiv.com](https://thebalanceffxiv.com). 
  Currently covers all 21 combat jobs plus BLU.
- **`MateriaTiers`** — Tier I through XII stat values, materia item 
  name lookup (Tier XII Critical Hit → "Savage Aim Materia XII").
- **`StatNames`** — canonicalization between abbreviated forms 
  (`"CRT"`, `"DH"`) and in-game BaseParam display names 
  (`"Critical Hit"`, `"Direct Hit Rate"`). The web's v0.6.1 hotfix 
  helper, centralized here.

## Versioning

Core follows the same version number as the web app and plugin. 
From v0.6.3 onward, all three projects ship at the same version 
when there's a release on any of them. v0.5.5 established this 
lockstep convention between web and plugin; v0.6.3 extends it to 
include Core.

If you find Core at a different version than web/plugin, something 
shipped wrong — file an issue.

## Repository layout

Core lives in its own git repository alongside web and plugin:

```
D:\GearGoblin-Core-v0.1\GearGoblin.Core\        ← this repo
D:\GearGoblin-v0.1\GearGoblin\                  ← plugin
D:\TonberryTactics-workspace\TonberryTactics\   ← web
```

Both web and plugin reference Core via relative `<ProjectReference>` 
paths in their csproj files:

```xml
<ProjectReference Include="..\..\GearGoblin-Core-v0.1\GearGoblin.Core\GearGoblin.Core.csproj" />
```

The double-`..` walks up out of the consuming project to a 
sibling-of-grandparent location. If you put Core somewhere else, 
update the path accordingly in both `TonberryTactics.csproj` and 
`GearGoblin.csproj`.

## Build

```powershell
dotnet build --configuration Release
```

Targets netstandard2.0 for maximum compatibility — works with 
both the web's .NET 8+ Blazor WASM target and the plugin's 
net10.0-windows Dalamud target.

## Consumers

Once you've built Core, consumers should rebuild too (the 
ProjectReference picks up Core's dll automatically on build):

```powershell
cd ..\..\TonberryTactics-workspace\TonberryTactics
dotnet build --configuration Release

cd ..\..\GearGoblin-v0.1\GearGoblin
dotnet build --configuration Release
```

## What Core does NOT contain

- **Wire format types** (`ExportPayloadV1`, `ExportPieceV1`, etc.) 
  stay in the consuming projects for v0.6.3. Migrating them into 
  Core would require touching every `using TonberryTactics.Models;` 
  and `using GearGoblin.Models;` in both repos — done incrementally 
  in v0.7.x.
- **Optimization driver** — the `PureMathOptimizer.Optimize(payload)` 
  entry point lives in the web. The plugin has its own. Core 
  provides the priorities they both consume; the per-side 
  orchestration stays in the consumer.
- **UI helpers** — Core is purely business logic. No ImGui, no 
  Blazor components, no styling.

## Releasing

```powershell
.\release.ps1
```

Standard pattern (matches web and plugin): bumps version, 
generates commit, tags, pushes. See `release.ps1` for the 
build-gate behaviour.

## License

MIT. Tonberry Tactics is a Refia Rakkiri / Aisling O'Callaghan 
project.

— Last Onion Knight, 2026
