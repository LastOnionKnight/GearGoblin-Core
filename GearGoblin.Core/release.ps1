# release.ps1 — GearGoblin.Core
#
# Bumps version (already in csproj), builds, commits, tags, pushes.
# Mirrors the release scripts in TonberryTactics and GearGoblin.

$ErrorActionPreference = 'Stop'

$logPath = Join-Path $PSScriptRoot "release-error.log"
Start-Transcript -Path $logPath -Append -Force -ErrorAction SilentlyContinue | Out-Null

Write-Host ""
Write-Host "=================================================="
Write-Host "  release.ps1"
Write-Host "=================================================="

# ── Project metadata ──────────────────────────────────────
$projectName = 'GearGoblin.Core'
$csprojPath  = 'GearGoblin.Core.csproj'
$cwd         = (Get-Location).Path

# Read version from csproj
$csprojXml = [xml](Get-Content $csprojPath)
$version   = $csprojXml.Project.PropertyGroup.Version | Where-Object { $_ }
if (-not $version) {
    Write-Host "ERROR: Couldn't read <Version> from $csprojPath" -ForegroundColor Red
    exit 1
}
$tag = "v$version"

Write-Host "  Project:  $projectName"
Write-Host "  csproj:   $csprojPath"
Write-Host "  cwd:      $cwd"
Write-Host "  Version:  $version"
Write-Host "  Tag:      $tag"
Write-Host ""

# ── Git branch sanity ─────────────────────────────────────
$branch = (git rev-parse --abbrev-ref HEAD).Trim()
Write-Host "  Branch:   $branch"
Write-Host ""

# ── Show pending changes ──────────────────────────────────
$status = git status --porcelain
if ($status) {
    Write-Host "Changes to be committed:"
    git status --short
    Write-Host ""
}
else {
    Write-Host "No changes detected. Nothing to release." -ForegroundColor Yellow
    exit 0
}

# ── Build gate ────────────────────────────────────────────
Write-Host "Running build gate: dotnet build --configuration Release..."
Write-Host "----------------------------------------"
dotnet build --configuration Release
if ($LASTEXITCODE -ne 0) {
    Write-Host "Build gate FAILED. Aborting release." -ForegroundColor Red
    exit 1
}
Write-Host "----------------------------------------"
Write-Host "Build gate: OK."
Write-Host ""

# ── Compose commit message from CHANGELOG ─────────────────
$changelog = Get-Content 'CHANGELOG.md' -Raw
$entryRegex = "(?ms)^## \[$([regex]::Escape($version))\][^\n]*\n(.+?)(?=^## \[|\Z)"
$match = [regex]::Match($changelog, $entryRegex)
if ($match.Success) {
    $body = $match.Groups[1].Value.Trim()
}
else {
    $body = "Release $version. See CHANGELOG.md for details."
}

$commitMsg = "$projectName $version`n`n$body"
$tempMsg = Join-Path $env:TEMP "ggcore-commit-msg.txt"
[System.IO.File]::WriteAllText($tempMsg, $commitMsg, [System.Text.UTF8Encoding]::new($false))

Write-Host "Commit message preview:"
Write-Host "----------------------------------------"
Get-Content $tempMsg | Select-Object -First 30
Write-Host "----------------------------------------"
Write-Host ""

# ── Commit + tag + push ───────────────────────────────────
Write-Host "Staging all changes..."
git add -A

Write-Host "Committing..."
git commit -F $tempMsg
if ($LASTEXITCODE -ne 0) {
    Write-Host "Commit failed. Aborting." -ForegroundColor Red
    exit 1
}

Write-Host "Tagging $tag..."
git tag $tag
if ($LASTEXITCODE -ne 0) {
    Write-Host "Tag failed. Local commit is saved." -ForegroundColor Yellow
    exit 1
}

Write-Host "Pushing to origin/$branch with tags..."
git push origin $branch --follow-tags
if ($LASTEXITCODE -ne 0) {
    Write-Host "git push failed" -ForegroundColor Yellow
    Write-Host "Local commit + tag are saved. Retry with: git push origin $branch --follow-tags"
    exit 1
}

Write-Host ""
Write-Host "Release $tag pushed."

Stop-Transcript -ErrorAction SilentlyContinue | Out-Null
