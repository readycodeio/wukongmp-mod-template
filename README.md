# WukongMP Mod Template

<img src="https://flagcdn.com/cn.svg" width="18" alt="Chinese"/> [中文版](README.zh-Hans.md)

A template project for developing a mod using the WukongMP SDK.

Refer to the [WukongMP SDK documentation](https://docs.ready.mp) for detailed information on how to use the SDK and create your mod.

## Getting started

1. Clone this repository to your local machine.
2. Open the solution in your preferred C# IDE (e.g., JetBrains Rider, Visual Studio).
3. Build the solution to ensure that all dependencies are correctly resolved.
4. Start developing your mod by modifying the `Main.cs` file and adding your own code.
5. Reference any of the DLLs in `SDK`, `Core`, or `Game` as needed for your mod's functionality.

## Repository structure

- `ExampleMod/Mod.cs`: The main entry point for your mod where you can initialize and set up your mod's functionality.
- `ExampleMod/manifest.json`: The manifest file for your mod, containing metadata such as name, version, and description.
- `SDK/WukongMp.Sdk`: WukongMP SDK files that you can reference in your mod development. The same files are present in the server binary package.
- `SDK/Overrides`: Versions of DLLs present in the base **Black Myth: Wukong** game, which the SDK overrides to ensure compatibility with the modding framework. These files are also present in the server binary package.
- `Core`: Our fork of the Harmony library, which is used for patching game code.
- `Game`: Contains the game's original DLLs extracted from the base game.

## Packaging the mod

1. Edit `ModFiles.ps1` to include all the necessary files for your mod.
2. Run the `PackageMod.ps1` script with argument `Release` to create a `.zip` file containing your mod.
3. The generated `.zip` file can be found in the `Output` directory.
4. Upload the `.zip` file to your server's `mods/` directory to deploy your mod.

## Debugging

Use the `PackageMod.ps1` script with the `Debug` argument to create a debug version of your mod, which includes additional files for debugging purposes.

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