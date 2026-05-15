# GearGoblin.Core v0.6.5.1 dropin — "Lockstep"

**Headline:** Pure lockstep version bump. No source changes.

Plugin v0.6.5.1 ("Quiet Info") and web v0.6.5.1 ("Audit reads right")
both ship meaningful work without needing Core changes. Core bumps
anyway to keep the strict-lockstep invariant.

## What's in this dropin

```
GearGoblin.Core.csproj    overwrite — version 0.6.5 → 0.6.5.1
CHANGELOG.md              overwrite — v0.6.5.1 entry on top
```

Two files. That's it.

## Build & deploy

```
cd D:\GearGoblin-Core-v0.1\GearGoblin.Core
Move-Item $env:USERPROFILE\Downloads\GearGoblin.Core-v0.6.5.1-dropin.zip ..\ -Force
Expand-Archive -Path ..\GearGoblin.Core-v0.6.5.1-dropin.zip -DestinationPath . -Force
Unblock-File .\release.ps1
git status
dotnet build -c Release
.\release.ps1 -DryRun
.\release.ps1
```

## Verify after push

1. `Get-Content GearGoblin.Core.csproj | Select-String "Version"` should
   show `<Version>0.6.5.1</Version>` and `0.6.5.1.0` AssemblyVersion / FileVersion.
2. `dotnet build -c Release` should complete with no warnings or errors.

## Pairing

- **GearGoblin plugin v0.6.5.1** — "Quiet Info". `/ttinfo` crash fix
  + About-tab What's New trim.
- **TonberryTactics web v0.6.5.1** — "Audit reads right". Off-by-one
  Tier XII display fix.

Ship order doesn't matter mechanically (no consumers' compile-time
references change), but by convention ship **Core → Plugin → Web**
so version trios are consistent at every intermediate state.
