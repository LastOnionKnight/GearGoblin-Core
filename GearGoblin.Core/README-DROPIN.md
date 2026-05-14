# GearGoblin.Core v0.6.4 dropin — "Quickarm Correction"

**Headline:** Fixes the Skill Speed materia prefix bug (`"Piety"` →
`"Quickarm"`) tracked since the v0.6.3 overnight ship. Lockstep
version bump alongside plugin v0.6.4. Adds persistent error-log
transcript to `release.ps1`.

## What's in this dropin

```
MateriaTiers.cs         overwrite — Skill Speed prefix corrected to "Quickarm"
GearGoblin.Core.csproj  overwrite — version 0.6.3 → 0.6.4, Description refresh
CHANGELOG.md            overwrite — v0.6.4 entry on top
release.ps1             overwrite — Start-Transcript / Stop-Transcript added
```

`StatNames.cs`, `JobPriorities.cs`, `README.md` are unchanged from
v0.6.3 and are NOT in this dropin.

## Why a "no-op-ish" version bump

The handoff-stated lockstep rule: all three components (Core, web,
plugin) carry the same version number at any time. Web shipped v0.6.4
already (via vendored Core source, since Cloudflare CI can't resolve
sibling-repo ProjectReferences). This Core v0.6.4 brings Core into
alignment AND folds in the Skill Speed prefix fix.

Web's vendored copy of `MateriaTiers.cs` still carries the v0.6.3
content — the Skill Speed prefix bug therefore remains visible on
the live web (cosmetic only — shows "Piety Materia XII" where it
should show "Quickarm Materia XII" for SkS recommendations) until
web v0.6.5 syncs the vendored copies forward. This is the documented
trade-off of the vendoring approach.

## Build & deploy

```
cd D:\GearGoblin-Core-v0.1\GearGoblin.Core
dotnet build -c Release
.\release.ps1
```

(Core's `release.ps1` doesn't have a `-DryRun` switch like the web's
does — it shows the commit message preview before staging anyway,
and you can Ctrl+C between the preview and the `git commit` line if
something looks wrong.)

## Verify after push

1. `https://github.com/LastOnionKnight/GearGoblin-Core/releases` —
   new `v0.6.4` tag present.
2. `Get-Content release-error.log` in the Core project folder — a
   transcript of this run should be appended at the bottom.
3. After plugin v0.6.4 ships (next step), build the plugin against
   this Core version. Plugin's ProjectReference path picks up the
   corrected prefix automatically; no plugin code change needed for
   this bug.

## Pairing

- **Plugin v0.6.4** — ships immediately after this. Same dropin
  pattern. Fixes the `/goblin → /tt` regression on line 730 of
  `StatusPanelInjector.cs` plus the matching `release.ps1`
  transcript addition.
- **Web v0.6.4** — already shipped. Carries v0.6.3 vendored copy of
  this file. Web v0.6.5 will sync forward to v0.6.4 Core content
  (Skill Speed prefix fix and any other Core changes that land
  between now and then).

## v0.7.x roadmap reminder

Plugin transitions to full CPR replacement, plugin-side `MeldOptimizer`
starts consuming `Core.JobPriorities`, web migrates from vendored
copies to a git submodule of this Core repo. None of that is in
v0.6.4 scope.
