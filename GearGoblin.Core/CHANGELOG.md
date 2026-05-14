# Changelog — GearGoblin.Core

All notable changes to GearGoblin.Core are documented here. Format 
based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), 
versioning matches the web app and plugin (lockstep from v0.6.3 onward).

## [0.6.3] — 2026-05-14  "Lockstep"

**Headline:** Initial public release. Extracts shared business logic 
from the web's `PureMathOptimizer` (where it had been hardcoded to 
GNB priority since v0.5.1) and the plugin's `MeldOptimizer` (where 
it had been job-aware via `JobProfile` but unable to share with the 
web). Both halves now project-reference Core for their priority 
table, materia tiers, and stat-name canonicalization.

**Why now:** v0.6.2's user-reported bug — an AST seeing GNB-priority 
recommendations because the web's optimizer only knew Gunbreaker — 
forced the issue. Rather than re-implement per-job priorities 
separately in the web (which would have re-diverged the two halves), 
this release stands up Core and routes both halves through it. 
v0.5.5's lockstep commitment now extends across three projects.

### Added (everything; initial release)

- **`JobPriorities`** — per-job materia stat-priority tables, keyed 
  by three-letter abbreviation. All 21 combat jobs plus BLU. Priorities 
  derived from thebalanceffxiv.com public guides for patch 7.x.
  - `For(jobAbbr)` returns `IReadOnlyList<string>` of priority stats 
    in order. Defensive: unknown jobs fall back to the GNB tank 
    baseline (same behaviour as v0.6.0/0.6.1/0.6.2 web).
  - `IsTableHit(jobAbbr)` tells callers whether they got a real 
    table entry or the fallback — UI surfaces should display a 
    "FALLBACK" badge when false.
  - `SourceLabel(jobAbbr)` returns a human-readable label for the 
    badge: `"AST priority"` or `"GNB fallback (no table for XYZ)"`.

- **`MateriaTiers`** — Tier I → XII stat-value lookup.
  - `SubstatValue(tier)` returns the per-tier stat value for substat 
    materia (Crit/DH/Det/SkS/SpS/Ten/Pie). Tier XII = 54.
  - `CurrentCapTier` constant exposes the current patch's top tier 
    (XII in patch 7.x).
  - `RomanNumeral(tier)` for the in-game-style "Materia XII" suffix.
  - `NameOf(statName, tier)` builds the in-game materia item name 
    (`"Savage Aim Materia XII"` for Critical Hit tier 12).
  - Known stub: `MateriaPrefix` lookup has the Skill Speed prefix 
    wrong ("Piety" instead of "Quickarm"). Cosmetic — only affects 
    the display string in recommendations, not the math. Tracked 
    for v0.6.4 hotfix.

- **`StatNames`** — BaseParam name canonicalization.
  - `Canonical(statName)` maps any input form (full BaseParam name, 
    abbreviation, common variants, whitespace/case noise) to a 
    three-letter key. Extracts the v0.6.1 web hotfix helper 
    (`CanonicalStatKey`) and makes it available to both sides.
  - `DisplayName(canonicalKey)` reverses to the in-game name for 
    rendering.
  - `IsKnown(key)`, `IsSpeed(key)` for filtering and role-aware 
    grid logic.
  - Constants: `Crit`, `DirectHit`, `Det`, `SkillSpeed`, 
    `SpellSpeed`, `Tenacity`, `Piety` — string constants for 
    callers that want to compare against canonical forms without 
    magic strings.

### Project setup

- `GearGoblin.Core.csproj` targets `netstandard2.0` — compatible 
  with both the web's .NET 8+ target (via Blazor WASM) and the 
  plugin's net10.0-windows target (via Dalamud SDK 15.0.0).
- Generates XML documentation file for IDE tooltips in consumers.
- Suppresses CS1591/CS1573 (missing XML on public members) — 
  most members are self-documenting via the class-level docs.

### Lockstep alignment

This release establishes Core as part of the version-lockstep 
contract with web and plugin:

- **GearGoblin.Core v0.6.3** — this release
- **TonberryTactics web v0.6.3** — adds Core ProjectReference, 
  refactors `PureMathOptimizer` to consume `Core.JobPriorities` 
  and `Core.StatNames`. Materia Advisor now shows per-job 
  recommendations for all 21 jobs.
- **GearGoblin plugin v0.6.3** — adds Core ProjectReference. 
  Plugin's existing job-aware `MeldOptimizer` continues to 
  function unchanged; consuming Core's priority tables is an 
  incremental migration tracked for v0.7.x.

All three ship same date, same version, same release night.

### Known limitations / deferred work

- **Priority tables are per-role baselines.** Most tanks share 
  one table; most healers share another; etc. The current model 
  doesn't capture job-specific nuance (BLM-specific SpS 
  breakpoints, MNK gear strategy, DNC partner-buff considerations). 
  Real fight-tuning lives in the BiS guides; users should treat 
  Pure-Math recommendations as a starting point, not gospel.
- **Wire format types not yet in Core.** `ExportPayloadV1` etc. 
  stay in consumer projects to avoid breaking all the `using` 
  statements in one release. Migration tracked for v0.7.x.
- **MateriaTiers prefix table stub** (Skill Speed → "Piety" is 
  wrong; should be "Quickarm"). Cosmetic, fixed in v0.6.4.
- **Settings-tab priority overrides** — users can't yet edit 
  priorities in-app. Planned for v0.7.x.
