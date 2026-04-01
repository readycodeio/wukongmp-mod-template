# WukongMP 模组模板

<img src="https://flagcdn.com/gb.svg" width="18" alt="English"/> [English version](README.md)。

一个用于使用 WukongMP SDK 开发模组的模板项目。

有关如何使用 SDK 和创建模组的详细信息，请参阅 [WukongMP SDK 文档](https://docs.ready.mp)。

## 快速开始

1. 将此仓库克隆到本地机器。
2. 在你偏好的 C# IDE 中打开解决方案（例如 JetBrains Rider、Visual Studio）。
3. 构建解决方案，确保所有依赖项均已正确解析。
4. 通过修改 `Main.cs` 文件并添加你自己的代码，开始开发你的模组。
5. 根据模组功能需要，引用 `SDK`、`Core` 或 `Game` 中的任意 DLL。

## 仓库结构

* `ExampleMod/Mod.cs`：你的模组主入口文件，可在此初始化并设置模组功能。
* `SDK/WukongMp.Sdk`：可在模组开发中引用的 WukongMP SDK 文件。服务器二进制包中也包含相同文件。
* `SDK/Overrides`：基础版 **Black Myth: Wukong** 游戏中存在的 DLL 的覆盖版本，SDK 会用这些文件来确保与模组框架的兼容性。这些文件同样包含在服务器二进制包中。
* `Core`：我们分支维护的 Harmony 库，用于补丁修改游戏代码。
* `Game`：包含从基础游戏中提取出的原始 DLL 文件。

## 打包模组

1. 编辑 `ModFiles.ps1`，将模组所需的所有文件包含进去。
2. 运行 `PackageMod.ps1` 脚本，创建包含模组的 `.zip` 文件。
3. 生成的 `.zip` 文件可在 `Output` 目录中找到。
4. 将该 `.zip` 文件上传到服务器的 `mods/` 目录中进行部署。
