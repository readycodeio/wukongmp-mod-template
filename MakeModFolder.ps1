#!powershell.exe -ExecutionPolicy Bypass -File

param (
    [string] $Configuration,
    [switch] $NoExplorer
)

# Check params
if (-not $Configuration)
{
    Write-Host "Usage: .\MakeModFolder.ps1 <Debug|Release>"
    Exit 1
}

$scriptDir = $PSScriptRoot

# Source the mod file list
. (Join-Path $scriptDir 'ModFiles.ps1')

# Find .sln files
$solutionFiles = Get-ChildItem -Path $scriptDir -Filter *.sln

# Check if exactly one .sln file was found
if ($solutionFiles.Count -eq 1)
{
    $solutionPath = $solutionFiles[0].FullName
}
else
{
    Write-Host "Error: Expected exactly one .sln file in $scriptDir, found $( $solutionFiles.Count )."
    Exit 1
}

# Build solution. This builds the shared, client and server projects in one go.
Write-Output "Building solution $solutionPath in configuration $Configuration..."
dotnet build $solutionPath -c $Configuration -v minimal /t:Rebuild | Tee-Object -FilePath (Join-Path $scriptDir 'build.log')

if ($LASTEXITCODE -ne 0)
{
    Write-Error "Build failed, see build.log for details."
    Exit 1
}

# Prepare temporary output directory
$outputRoot = Join-Path $scriptDir 'Output'
if (-not (Test-Path $outputRoot))
{
    New-Item -ItemType Directory -Path $outputRoot -Force | Out-Null
}
else
{
    Get-ChildItem $outputRoot -Recurse | Remove-Item -Force -Recurse
}

# One folder per mod, holding a client and a server side plus the shared manifest. The server
# hands out the client side and keeps the server side to itself.
$modRoot = Join-Path (Join-Path $outputRoot 'mods') $clientProject
$clientRoot = Join-Path $modRoot 'client'
$serverRoot = Join-Path $modRoot 'server'
New-Item -ItemType Directory -Path $clientRoot -Force | Out-Null
New-Item -ItemType Directory -Path $serverRoot -Force | Out-Null

# Helper for copying files from ModFiles.ps1
function Copy-BuildArtifacts
{
    param(
        [Parameter(Mandatory = $true)]
        [AllowEmptyCollection()]
        [string[]]$Files,

        [Parameter(Mandatory = $true)]
        [string]$BaseDir,

        [Parameter(Mandatory = $true)]
        [string]$DestDir
    )

    foreach ($file in $Files)
    {
        $sourceFile = Join-Path -Path $BaseDir -ChildPath $file
        $destFile = Join-Path -Path $DestDir -ChildPath $file

        if (Test-Path -Path $sourceFile -PathType Container)
        {
            Copy-Item -Path $sourceFile -Destination $destFile -Recurse -Force
            Write-Output "Copied $file/ to $( Split-Path $DestDir -Leaf )."
        }
        elseif (Test-Path -Path $sourceFile)
        {
            # Ensure destination directory exists
            $destDir = Split-Path -Parent $destFile
            if (-not (Test-Path -Path $destDir))
            {
                New-Item -ItemType Directory -Path $destDir -Force | Out-Null
            }

            Copy-Item -Path $sourceFile -Destination $destFile -Force
            Write-Output "Copied $file to $( Split-Path $DestDir -Leaf )."
        }
        else
        {
            Write-Warning "Warning: Source file not found: $sourceFile"
        }
    }
}

# Copy files
$clientBuildDir = Join-Path $scriptDir "$clientProject/bin/$Configuration/netstandard2.0"
$serverBuildDir = Join-Path $scriptDir "$serverProject/bin/$Configuration/net10.0"
$contentDir = Join-Path $scriptDir "Content"

$clientFiles = $clientBuildFiles
$serverFiles = $serverBuildFiles
if ($Configuration -eq "Debug")
{
    $clientFiles += $clientDebugBuildFiles
    $serverFiles += $serverDebugBuildFiles
}

Copy-BuildArtifacts -Files $clientFiles -BaseDir $clientBuildDir -DestDir $clientRoot
Copy-BuildArtifacts -Files $manifestFiles -BaseDir $contentDir -DestDir $modRoot
Copy-BuildArtifacts -Files $clientContentFiles -BaseDir $contentDir -DestDir $clientRoot
Copy-BuildArtifacts -Files $serverFiles -BaseDir $serverBuildDir -DestDir $serverRoot
Copy-BuildArtifacts -Files $serverContentFiles -BaseDir $contentDir -DestDir $serverRoot

# Open explorer to the output directory
if ($NoExplorer)
{
    # nothing to open, this run is scripted
}
elseif ($PSVersionTable.PSEdition -eq 'Core')
{
    Start-Process "explorer.exe" -ArgumentList "`"$outputRoot`""
}
else
{
    Invoke-Item $outputRoot
}
