# Changelog

## [1.6.1] - 2026-08-14

### Fixed
- Corrected the combat-substat materia projection table used for hypothetical/new meld recommendations. The previous `Materia/Materias.cs` table incorrectly projected Materia XII as +96; current combat Materia XII is +54. The shared table now uses the actual I-XII progression: +1, +2, +3, +4, +6, +16, +8, +24, +12, +36, +18, +54.
- Removed the duplicate tier-value table from `Materia/Materias.cs`. `MateriaCatalog` now delegates hypothetical meld values to `MateriaTiers`, leaving one shared source of truth for plugin and web projections. Existing meld values remain Lumina-derived at inventory read time.

### Changed
- Version remains 1.6.1 for trinity lockstep with the GearGoblin plugin and Tonberry Tactics web companion following the 2026-08-11 FFXIV patch update.

## [1.6.0] - 2026-08-07

### Changed
- Version bump for trinity lockstep.

## [1.5.7c] - 2026-07-06

### Changed
- Version bump for trinity lockstep (web PSA video fix). No functional core changes.

## [1.5.7b] - 2026-07-05

### Changed
- Version bump for trinity lockstep (plugin readability + FontAwesome chrome glyphs). No functional core changes.

## [1.5.7] - 2026-07-05

### Added
- Added `IconId` field to `MeldablePiece` to support native FFXIV UI texture retrieval without asset bundles.

## [1.5.6] - 2026-07-05

### Changed
- Version bump for trinity lockstep (plugin Phase 4 UI overhaul).

## [1.2.0] - 2026-06-01

### Fixed
- **Web adapter regression (resolves v1.1.4 build failure):** Restored the `WeightMode` argument that was improperly dropped from `MeldOptimizerAdapter.Optimize(...)` in v1.1.4. `WeightMode` is a fully supported Core feature (added v1.1.0) toggling between PureMath and BalancePreset scoring. The v1.1.4 web tag was never deployed to production due to this regression; v1.2.0 brought the web back into compilable state.

### Changed
- Scrubbed lingering documentation references to the retired `PureMathOptimizer` in Core's `JobPriorities.cs`. Optimizer code itself was retired in v0.6.3.
- Plugin AssemblyVersion, FileVersion, and Version synchronized to 1.2.0 (previously AssemblyVersion and FileVersion were stale at 1.1.2 due to `release.ps1` only updating `<Version>`; logged as Architecture Debt).

### Lockstep notes
- Standalone GearGoblin.Core skipped v1.1.3 in its tag stream; the v1.1.3 cap-math fix was applied via the submodule-vendored Core consumed by plugin and web. v1.2.0 brought standalone Core back into trinity lockstep.
- Web `CHANGELOG.md` previously skipped v1.1.3 (SuccessRate adapter parity fix) and v1.1.4 (build-failure tag); see git history for those releases.

## [1.1.2] - 2026-05-31

### Changed
- Lockstep bump for trinity parity with GearGoblin plugin v1.1.2, which fixed the empirical cap-math mapping.

## [1.1.1] - 2026-05-31

### Changed
- No internal changes to Core. Version bump to maintain trinity coherence with GearGoblin and TonberryTactics v1.1.1.
