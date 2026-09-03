#!powershell.exe -ExecutionPolicy Bypass -File

# Edit these lists to specify files that should be included in the mod output.
#
# MakeModFolder.ps1 produces one folder per mod, with a side each:
#   Output/mods/ExampleMod/manifest.json  shared by both sides
#   Output/mods/ExampleMod/client         client DLLs, sent to players
#   Output/mods/ExampleMod/server         server DLLs, never sent to players
#
# The server keeps the server folder to itself, so anything you would not hand a player
# belongs there and nowhere else.

# Project folder names. Rename these together with the projects themselves.
# The mod folder in Output takes its name from $clientProject.
$clientProject = "ExampleMod"
$serverProject = "ExampleMod.Serverside"

# Copied from the client build folder (ExampleMod/bin/<Configuration>/netstandard2.0)
# into the client folder
$clientBuildFiles = @(
    "ExampleMod.dll",
    "ExampleMod.Common.dll"
)

# Copied from the "Content" folder into the mod folder root
$manifestFiles = @("manifest.json")

# Copied from the "Content" folder into the client folder
$clientContentFiles = @(
    # Add any non-code client files here, e.g. save files or .paks.
)

# Copied from the server build folder (ExampleMod.Serverside/bin/<Configuration>/net10.0)
# into the server folder
$serverBuildFiles = @(
    "ExampleMod.Serverside.dll",
    "ExampleMod.Common.dll"
)

# Copied from the "Content" folder into the server folder. Never sent to players, so this is
# where a config file with server-only settings goes.
$serverContentFiles = @(
    # "config.json"
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