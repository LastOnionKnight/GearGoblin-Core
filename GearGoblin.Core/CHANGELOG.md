# Changelog — GearGoblin.Core

All notable changes to GearGoblin.Core are documented here. Format 
based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), 
versioning matches the web app and plugin (lockstep from v0.6.3 onward).

## [0.6.4] — 2026-05-14  "Quickarm Correction"

**Headline:** Fixes the v0.6.3 known-stub where Skill Speed materia
recommendations surfaced as "Piety Materia XII" instead of the
correct in-game item name "Quickarm Materia XII". Cosmetic — affects
only the display string, not the priority math or stat values — but
visibly wrong for any physical-DPS or tank job whose priority list
hit the Skill Speed slot. Tracked since the overnight v0.6.3 ship.

Lockstep version bump alongside plugin v0.6.4. Web v0.6.4 already
shipped (vendored Core source approach to work around Cloudflare
Pages' inability to resolve sibling-repo ProjectReferences); web's
vendored copy of MateriaTiers.cs still carries the v0.6.3 stub and
will sync to v0.6.4's content on web's next release.

### Fixed

- **`MateriaTiers.MateriaPrefix("Skill Speed")`** — returns
  `"Quickarm"` (correct) rather than `"Piety"` (placeholder). Piety
  materia keeps its own correct prefix (`"Piety"`); the two were
  transposed in v0.6.3's overnight ship and have been swapped to
  their proper assignments. Verified against current FFXIV item-name
  data for patch 7.5.

### Changed

- **`release.ps1`** — adds persistent error log via `Start-Transcript`
  / `Stop-Transcript`. Output appends to `release-error.log` alongside
  the script, capturing both stdout and stderr through the full run
  including any failure path. Matches the pattern landing in
  TonberryTactics/release.ps1 and GearGoblin/release.ps1 (the
  plugin-side script ships alongside plugin v0.6.4).
- **`GearGoblin.Core.csproj`** — version `0.6.3 → 0.6.4`, Description
  refreshed for "Quickarm Correction".

### Pairing

- **GearGoblin plugin v0.6.4** — ships alongside this release.
  Plugin's existing ProjectReference to Core picks up the
  corrected prefix automatically on next build; `/goblin → /tt`
  regression at line 730 of `StatusPanelInjector.cs` (v0.4.7.1 brand
  convergence miss) also lands in plugin v0.6.4.
- **TonberryTactics web v0.6.4** — already shipped with the v0.6.3
  vendored copy of `MateriaTiers.cs`. The Skill Speed prefix bug
  remains visible on the live web (cosmetic only) until web v0.6.5
  syncs the vendored copies to match this Core release.

### Known stubs still in flight

- **Per-job stat-cap awareness** — no breakpoint or stat-cap math in
  Core yet. The optimizer recommends Tier XII into any empty slot
  without checking whether it would overcap. Plugin's MeldOptimizer
  handles this via in-game BaseParam read; Core / web stack will
  need its own implementation. Targeted at v0.8.x Balance-mode work.
- **Priority table currency** — patch 7.x Dawntrail tier values
  baked in. Major-patch updates will need refresh sweeps against
  the latest thebalanceffxiv.com guides.

---

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
