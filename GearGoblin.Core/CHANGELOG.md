## [1.1.1] - 2026-05-31
- Released for consumer-repo submodule consumption; no Core code changes from v1.1.0.
## [v1.1.1] - Submodule release; consumer repos now consume via ProjectReference from CI

## [1.1.0] - 2026-05-30
### Changed
- Refactored GearGoblin Core math. Lifted Materia logic from plugin and web app into GearGoblin.Core to guarantee parity.
- Fixed substat cap bug where capped stats were being incorrectly recommended.
# Changelog — GearGoblin.Core

All notable changes to GearGoblin.Core are documented here. Format 
based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), 
versioning matches the web app and plugin (lockstep from v0.6.3 onward).

## [0.6.7.6] — 2026-05-27  "Lockstep"

**Headline:** Version lockstep bump to match Plugin v0.6.7.6 (mojibake hotfix). No source changes to Core.

---

## [0.6.5.4] — 2026-05-18  "H6 Lockstep"

**Pairing:** GearGoblin v0.6.5.4 (plugin) + TonberryTactics v0.6.5.4 (web).

**Scope:** Version-only lockstep bump — no source changes to Core.

**Why this version exists:** BUG-001 (Materia Advisor header ghost text)
remains unfixed after three prior attempts. v0.6.5.4 is the plugin's
fourth swing at it — testing hypothesis H6 (text overflow from the
cloned label cell into the number cell's render zone). The plugin also
unifies its `/ttinfo` and header-pill version strings under a single
resolver. Core travels along to keep the trinity in lockstep.

**Versioning note:** The letter-suffix experiment (v0.6.5.3a) is closed
on the plugin side; all three repos go pure-numeric from v0.6.5.4
onward. Letter-suffix versions caused friction with release.ps1 (tag
from csproj Version) and the github-actions repo.json bot (semver
parsing).

**Mobile site:** Slides from its originally-scoped v0.6.5.4 slot to
v0.6.6. The v0.6.5.x patch stream has been consumed by BUG-001
stabilization; mobile is a feature, not a fix, and waits.

---

## [0.6.5.3] — 2026-05-16  "Collision Fix"

**Headline:** Pure lockstep version bump. No source changes to the
library.

**Pairing with:** Plugin v0.6.5.3 ships the real fix for the
character-panel ghost-text bug that v0.6.5.2 misdiagnosed. Adding an
`expandCollisionNode` parameter to `StatusPanelInjector.AddStatRow`
matches the upstream CharacterPanelRefined pattern we adapted from and
removes collision-node growth on rows that inject into the Gear /
Average Item Level component. Same release also lands four `/goblin`
→ `/tt` brand-convergence fixes the v0.5.x sweep missed in
StatusPanelInjector. Web (TonberryTactics v0.6.5.3) is also a
version-only bump.

### Why Core bumps when it has no changes

Lockstep is the project's release invariant — Plugin, Core, and Web
must always carry the same version tag so consumers can reason about
compatibility from a single number. Letting Core lag at 0.6.5.2 while
Plugin moves to 0.6.5.3 breaks that invariant and complicates the
matrix going forward. Version-only bumps are cheap.

---

## [0.6.5.2] — 2026-05-14  "Release Hardening"

**Headline:** Pure lockstep version bump. No source changes to the
library.

The release.ps1 script gains a `git fetch + pull --rebase --autostash`
preamble that keeps the workflow symmetric with Plugin and Web. Core
itself has no automated upstream pushes that would force this step
(Plugin has the repo.json auto-bot; Web has Cloudflare Pages builds
that don't push back), but the symmetry matters: every release script
in the trio should behave identically so muscle memory carries across
all three workspaces.

### Changed

- **`release.ps1`** — new "sync with remote" step between branch
  detection and the build gate. `git fetch origin <branch>` then
  `git pull --rebase --autostash origin <branch>`. Same shape as
  Plugin and Web.
- **`GearGoblin.Core.csproj`** — version `0.6.5.1 → 0.6.5.2`,
  Description updated for "Release Hardening".

### Unchanged (carried forward from v0.6.5)

- `JobPriorities.cs`, `MateriaTiers.cs`, `StatNames.cs` — Core
  library content unchanged.

### Pairing

- **GearGoblin plugin v0.6.5.2** — same `release.ps1` sync step; the
  critical destination for the rebase pattern because of the
  repo.json bot.
- **TonberryTactics web v0.6.5.2** — same sync step **plus** the
  build gate that web's `release.ps1` was missing; plus the
  EVERCOLD wordmark wrapped in an external link to the official
  FFXIV Evercold expansion page.

---

## [0.6.5.1] — 2026-05-14  "Lockstep"

**Headline:** Pure lockstep version bump. No source changes.

Plugin v0.6.5.1 ("Quiet Info") ships the `/ttinfo` hard-crash hotfix
and the About-tab "What's New" trim. Web v0.6.5.1 ("Audit reads
right") ships the off-by-one Tier display fix. Neither needs Core
changes — the existing `JobPriorities`, `MateriaTiers`, and `StatNames`
tables continue to work unchanged. Core bumps anyway to maintain the
strict-lockstep invariant so `/ttinfo` shows `Plugin: 0.6.5.1 /
Core: 0.6.5.1 / Web: 0.6.5.1` matching.

### Changed

- **`GearGoblin.Core.csproj`** — version `0.6.5 → 0.6.5.1`,
  Description updated to reflect the lockstep-only nature of this
  release and the pairing notes for plugin and web.

### Unchanged (carried forward from v0.6.5)

- `JobPriorities.cs` — per-job stat priority tables for all 21
  combat jobs (plus BLU).
- `MateriaTiers.cs` — Tier I–XII materia stat values; `CurrentCapTier
  = 12` (note: this is 1-indexed, while plugin's wire-format `Grade`
  field is 0-indexed. The off-by-one fix in web v0.6.5.1 lives at the
  consumer, not here; this comment exists for future maintainers).
- `StatNames.cs` — BaseParam name canonicalization.

### Pairing

- **GearGoblin plugin v0.6.5.1** — "Quiet Info". `/ttinfo` crash fix
  + About-tab What's New trim.
- **TonberryTactics web v0.6.5.1** — "Audit reads right". Off-by-one
  Tier XII display fix.

---

## [0.6.5] — 2026-05-14  "Lockstep"

**Headline:** Pure lockstep version bump. No source changes.

The plugin and web halves both ship v0.6.5 with meaningful work
this release — plugin gets the critical HQ-offset
`InventoryReader` fix ("Crafted Visible"), web wires the
previously-stub Meld Audit panel rows and adds a Sell / replace
verdict row ("Audit lit up"). The shared `JobPriorities`,
`MateriaTiers`, and `StatNames` tables don't need any changes to
support either piece of work.

The strict-lockstep policy (every release bumps all three even
when one side is a no-op) means Core ships v0.6.5 anyway, so when
someone runs `/ttinfo` and sees `Plugin: 0.6.5 / Core: 0.6.5 /
Web: 0.6.5` the version trio matches and there's no "did the
library land?" question to ask.

### Changed

- **`GearGoblin.Core.csproj`** — version `0.6.4 → 0.6.5`,
  Description updated to reflect the lockstep-only nature of this
  release.

### Unchanged (carried forward from v0.6.4)

- `JobPriorities.cs` — per-job stat priority tables for all 21
  combat jobs (plus BLU).
- `MateriaTiers.cs` — Tier I–XII materia stat values; Skill Speed
  prefix is "Quickarm" (corrected from v0.6.3 "Piety" stub).
- `StatNames.cs` — BaseParam name canonicalization.

### Pairing

- **GearGoblin plugin v0.6.5** — "Crafted Visible".
- **TonberryTactics web v0.6.5** — "Audit lit up".

---

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



