#!powershell.exe -ExecutionPolicy Bypass -File

# Edit these lists to specify files that should be included in the mod output.
#
# MakeModFolder.ps1 produces two things:
#   Output/mods/ExampleMod    the client mod folder, dropped into the game's Mods/ folder
#   Output/server_mods   loose files, dropped into the server's server_mods/ folder

# Project folder names. Rename these together with the projects themselves.
# The client mod folder in Output takes its name from $clientProject.
$clientProject = "ExampleMod"
$serverProject = "ExampleMod.Serverside"

# Copied from the client build folder (ExampleMod/bin/<Configuration>/netstandard2.0)
# into the client mod folder
$clientBuildFiles = @(
    "ExampleMod.dll",
    "ExampleMod.Common.dll"
)

# Copied from the "Content" folder into the client mod folder root
$contentFiles = @(
    # Add any non-code files here, e.g. save files or .paks.
    "manifest.json"
)

# Copied from the server build folder (ExampleMod.Serverside/bin/<Configuration>/net10.0)
# into server_mods. Server mods have no folder of their own, every file sits next to
# the SDK's own server mods, so only ship what is yours.
$serverBuildFiles = @(
    "ExampleMod.Serverside.dll",
    "ExampleMod.Common.dll"
)

# Copied only in Debug builds
$clientDebugBuildFiles = @(
    "ExampleMod.pdb",
    "ExampleMod.Common.pdb"
)

$serverDebugBuildFiles = @(
    "ExampleMod.Serverside.pdb",
    "ExampleMod.Common.pdb"
)
