# WukongMP Mod Template

![version](https://img.shields.io/badge/version-0.3.0-green)

For other versions, check the list of [tags](https://github.com/readycodeio/wukongmp-mod-template/tags).

<img src="https://flagcdn.com/cn.svg" width="18" alt="Chinese"/> [中文版](README.zh-Hans.md)

A template project for developing a mod using the WukongMP SDK.

Refer to the [WukongMP SDK documentation](https://docs.ready.mp) for detailed information on how to use the SDK and create your mod.

## Requirements

* [.NET 10.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) or later
* If you are using Visual Studio, you need version 2026 or later

## Getting started

1. Clone this repository to your local machine.
2. Open the solution in your preferred C# IDE (e.g., JetBrains Rider, Visual Studio).
3. Build the solution to ensure that all dependencies are correctly resolved.
4. Start developing your mod by modifying `ExampleMod/Mod.cs` and `ExampleMod.Serverside/Mod.cs` and adding your own code.
5. Reference any of the DLLs in `Dependencies` as needed for your mod's functionality.

## Repository structure

A fully fledged mod is three projects: shared code, the client mod that runs inside the game, and the server mod that runs inside the relay server. The shared project is a dependency of the other two.

- `ExampleMod.Common`: Types both sides have to agree on, most importantly the RPC contracts. Targets `netstandard2.0` so the client mod and the server mod can both reference it.
- `ExampleMod`: The client mod, loaded by the game. Targets `netstandard2.0`.
  - `Mod.cs`: The entry point where you initialize and set up your mod's functionality.
  - `ExampleServerRpc.cs`: The client half of the shared RPC contracts.
  - `ExampleRpc.cs`: Client to client events, which do not involve the server mod.
- `ExampleMod.Serverside`: The server mod, loaded by the relay server. Targets `net10.0`.
  - `Mod.cs`: The entry point, where you register your systems and RPC handlers.
  - `RpcHandlers.cs`: The server half of the shared RPC contracts.
  - `ExampleStateSystem.cs`: An example system, ticked by the server.
- `Content/manifest.json`: The manifest file for your client mod, containing metadata such as name, version, and description. Server mods need no manifest.
- `Dependencies`: WukongMP SDK assemblies that you can reference in your mod development. The same files are present in the server binary package.
  - `SDK`: The `netstandard2.0` SDK builds, referenced by the shared and client projects.
  - `ServerSDK`: The `net10.0` SDK builds, referenced by the server project.
  - `Loader`: Mod loader assemblies, client side only.

The game's own assemblies come from the [`ReadyM.Wukong.GameRefs`](https://github.com/readycodeio/wukong-game-refs)
NuGet package, which the client project references. Those are reference-only assemblies:
API surface with no method bodies, used at compile time only. The real assemblies are
already loaded in the game process at runtime, so nothing needs shipping with your mod.

One consequence worth knowing: you cannot step into game code, or run game code outside
the game, against reference-only assemblies. If you need either, remove the
`ReadyM.Wukong.GameRefs` package reference and add `<Reference>` items pointing at a full
set of assemblies extracted from your own installation instead. The compile succeeds
identically either way.

## Packaging the mod

1. Make sure to edit `manifest.json` with the correct information for your mod, such as name, version, and description.
2. Edit `ModFiles.ps1` to add any extra files your mod uses.
3. Run the `MakeModFolder.ps1` script with argument `Release`. It builds all three projects and packages them.
4. The results can be found in the `Output` directory:
   - `Output/mods/ExampleMod`: the client mod folder.
   - `Output/server_mods`: the server mod files.
5. Copy the client mod folder into your server's `mods/` directory.
6. Copy the *contents* of `server_mods` into your server's `server_mods/` directory. Server mods do not get a folder of their own, every server mod's files sit next to each other in there.
7. Restart the server.

## Debugging

Use the `MakeModFolder.ps1` script with the `Debug` argument to create a debug version of your mod, which includes additional files for debugging purposes.

Before you can debug your mod, you need to enable the debugger in WukongMP.

### Enabling the debugger

In order to enable the debugger in modded WukongMP, you need to follow these steps:

1. Navigate to `%APPDATA%\ReadyM.Launcher\DownloadCache\Loader`
2. Enter the folder with the latest version number, e.g. `0.7.457.1630`
3. Go to the `@APPDATA\CSharpLoader\b1cs.ini` file and edit the following settings:

```ini
[Settings]
Develop=1       # enable debugger
Console=1       # show console window
EnableJit=1     # required, do not change
```

The next time you launch the game, the mono debugger server will be enabled on port `44446`.
You can change the debugger settings by editing the `debugger-agent.txt` file in the same folder.

The default settings for the debugger agent are as follows:

```txt
transport=dt_socket,loglevel=0,address=127.0.0.1:44446,server=y,suspend=n
```

### Connecting from JetBrains Rider

1. Go to `Run > Edit Configurations`.
2. Click the `+` button and select `Mono Remote`.
3. Set the `Name` to something like `WukongMP Debugger`.
4. Set the `Host` to `localhost` and the `Port` to `44446` (or the port you configured in `debugger-agent.txt`).
5. Click `Apply` and then `OK`.

Now you can start the game and then run the `WukongMP Debugger` configuration in JetBrains Rider to connect to the debugger.
You should see the debugger console in Rider, and you can set breakpoints in your mod code to debug it.

> **Note**: The debugger may take a few seconds to connect for the first time and will display the message `Waiting for target to get ready`. Once the connection is successful, the status will change to `Target ready`.