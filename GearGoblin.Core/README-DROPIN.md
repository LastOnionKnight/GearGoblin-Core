# GearGoblin.Core v0.6.5 dropin — "Lockstep"

**Headline:** Pure lockstep version bump. No source changes.

Plugin v0.6.5 ("Crafted Visible") and web v0.6.5 ("Audit lit up")
both ship meaningful work without needing Core changes — the
existing `JobPriorities`, `MateriaTiers`, and `StatNames` tables
already support what both halves do this release. Core bumps
anyway to keep the strict-lockstep invariant (every release bumps
all three components even when one side is a no-op).

## What's in this dropin

```
GearGoblin.Core.csproj    overwrite — version 0.6.4 → 0.6.5
CHANGELOG.md              overwrite — v0.6.5 entry on top
```

That's it. Two files.

## Build & deploy

```
cd D:\GearGoblin-Core-v0.1\GearGoblin.Core
Move-Item $env:USERPROFILE\Downloads\GearGoblin.Core-v0.6.5-dropin.zip ..\ -Force
Expand-Archive -Path ..\GearGoblin.Core-v0.6.5-dropin.zip -DestinationPath . -Force
Unblock-File .\release.ps1
git status
dotnet build -c Release
.\release.ps1 -DryRun
.\release.ps1
```

## Verify after push

1. `Get-Content GearGoblin.Core.csproj | Select-String "Version"` should
   show `<Version>0.6.5</Version>` and `0.6.5.0` AssemblyVersion / FileVersion.
2. `git log --oneline -5` should show the v0.6.5 commit on top.
3. `dotnet build -c Release` should complete with no warnings or errors.

## Pairing

- **GearGoblin plugin v0.6.5** — "Crafted Visible". Critical
  HQ-offset `InventoryReader` fix.
- **TonberryTactics web v0.6.5** — "Audit lit up". Real audit
  logic + Sell/replace verdict row.

Ship order doesn't matter for this release since Core has no
source changes — none of the consumers' compile-time references
change. But by convention, ship Core first so the version trio is
visible in any subsequent debugging.
