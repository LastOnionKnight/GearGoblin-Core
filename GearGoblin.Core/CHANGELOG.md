# Changelog

## [1.2.0] - 2026-06-01

### Fixed
- **Web adapter regression (resolves v1.1.4 build failure):** Restored the
  WeightMode argument that was improperly dropped from
  MeldOptimizerAdapter.Optimize(...) in v1.1.4. WeightMode is a fully
  supported Core feature (added v1.1.0) toggling between PureMath and
  BalancePreset scoring. The v1.1.4 web tag was never deployed to production
  due to this regression; v1.2.0 brings the web back into compilable state.

### Changed
- Scrubbed lingering documentation references to the retired PureMathOptimizer
  in Core's JobPriorities.cs. Optimizer code itself was retired in v0.6.3.
- Plugin AssemblyVersion, FileVersion, and Version synchronized to 1.2.0
  (previously AssemblyVersion and FileVersion were stale at 1.1.2 due to
  elease.ps1 only updating <Version>; logged as Architecture Debt).

### Lockstep notes
- Standalone GearGoblin.Core repo skipped v1.1.3 in its tag stream - the
  v1.1.3 cap-math fix was applied via the submodule-vendored Core consumed
  by plugin and web. v1.2.0 brings standalone Core back into trinity lockstep.
- Web CHANGELOG.md previously skipped v1.1.3 (SuccessRate adapter parity
  fix) and v1.1.4 (build-failure tag); see git history for those releases.
## [1.1.2] - 2026-05-31
### Changed
- Lockstep bump for trinity parity with GearGoblin plugin v1.1.2 (which fixes the empirical cap math mapping).
## [1.1.1] - 2026-05-31
### Changed
- No internal changes to Core. Version bump to maintain trinity coherence with GearGoblin and TonberryTactics v1.1.1 (which fixes CI submodules and cap math logic in the plugin).


