# GearGoblin.Core v0.6.5.2 dropin — "Release Hardening"

**Pure lockstep version bump + release.ps1 symmetry update.** No source
changes to the library itself.

## What's in this dropin

```
release.ps1                overwrite — fetch + pull --rebase --autostash preamble
GearGoblin.Core.csproj     overwrite — version 0.6.5.1 → 0.6.5.2
CHANGELOG.md               overwrite — v0.6.5.2 entry on top
```

## Build & deploy

```
cd D:\GearGoblin-Core-v0.1\GearGoblin.Core
Move-Item $env:USERPROFILE\Downloads\GearGoblin.Core-v0.6.5.2-dropin.zip ..\ -Force
Expand-Archive -Path ..\GearGoblin.Core-v0.6.5.2-dropin.zip -DestinationPath . -Force
Unblock-File .\release.ps1
.\release.ps1 -DryRun
.\release.ps1
```

You'll see the new "Syncing with origin/main…" line near the top of
the release output — same step Plugin and Web now have, for workflow
symmetry.

## Pairing

- **GearGoblin plugin v0.6.5.2** — same sync step. Critical
  destination because of the repo.json bot.
- **TonberryTactics web v0.6.5.2** — same sync step **plus** the
  build gate that Web's release.ps1 was missing.

## Ship order

Doesn't matter mechanically (Core has no consumers' compile-time
references that would change), but by convention ship **Core → Plugin
→ Web** so the version trio is consistent at every intermediate state.
