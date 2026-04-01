# WukongMP Mod Template

A template project for developing a mod using the WukongMP SDK.

Refer to the [WukongMP SDK documentation](https://docs.ready.mp) for detailed information on how to use the SDK and create your mod.

## Getting started

1. Clone this repository to your local machine.
2. Open the solution in your preferred C# IDE (e.g., JetBrains Rider, Visual Studio).
3. Build the solution to ensure that all dependencies are correctly resolved.
4. Start developing your mod by modifying the `Main.cs` file and adding your own code.

## Repository structure

- `ExampleMod/Mod.cs`: The main entry point for your mod where you can initialize and set up your mod's functionality.
- `SDK/WukongMp.Sdk`: Contains the WukongMP SDK files that you can reference in your mod development. The same files are present in the server binary package.
- `SDK/Overrides`: Contains versions of DLLs present in the base **Black Myth: Wukong** game, but the SDK overrides them to different versions to ensure compatibility with the modding framework. These files are also present in the server binary package.

## Packaging the mod

1. Edit `ModFiles.ps1` to include all the necessary files for your mod.
2. Run the `PackageMod.ps1` script to create a `.zip` file containing your mod.
3. The generated `.zip` file can be found in the `Output` directory.
4. Upload the `.zip` file to your server's `mods/` directory to deploy your mod.