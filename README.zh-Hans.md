# WukongMP 模组模板

![version](https://img.shields.io/badge/版本-0.4.0-green)

<img src="https://flagcdn.com/gb.svg" width="18" alt="English"/> [English version](README.md)。

对于其他版本，请查看[标签](https://github.com/readycodeio/wukongmp-mod-template/tags)列表。

这是一个用于使用 WukongMP SDK 开发模组（Mod）的模板项目。

有关如何使用 SDK 和创建模组的详细信息，请参阅 [WukongMP SDK 文档](https://docs.ready.mp)。

## 系统要求

* [.NET 10.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) 或更高版本
* 如果您使用的是 Visual Studio，则需要 2026 或更高版本。

## 快速上手

1. 克隆此仓库到您的本地机器。
2. 在您偏好的 C# IDE（例如 JetBrains Rider、Visual Studio）中打开解决方案（Solution）。
3. 构建（Build）解决方案，以确保所有依赖项（Dependencies）都已正确解析。
4. 通过修改 `ExampleMod/Mod.cs` 和 `ExampleMod.Serverside/Mod.cs` 文件并添加您自己的代码，开始开发您的模组。
5. SDK 以 NuGet 包的形式提供，每个项目各引用一个，无需手动引用 DLL 文件。

## 仓库结构

一个完整的模组由三个项目组成：共享代码、在游戏内运行的客户端模组，以及在中继服务器内运行的服务端模组。共享项目是另外两个项目的依赖项。

- `ExampleMod.Common`: 双方必须保持一致的类型，最重要的是 RPC 契约（Contracts）。目标框架为 `netstandard2.0`，因此客户端模组和服务端模组都可以引用它。
- `ExampleMod`: 客户端模组，由游戏加载。目标框架为 `netstandard2.0`。
  - `Mod.cs`: 入口点，您可以在此处初始化和设置模组的功能。
  - `ExampleServerRpc.cs`: 共享 RPC 契约的客户端部分。
  - `ExampleRpc.cs`: 客户端之间的事件，不涉及服务端模组。
- `ExampleMod.Serverside`: 服务端模组，由中继服务器加载。目标框架为 `net10.0`。
  - `Mod.cs`: 入口点，您可以在此处注册您的系统（Systems）和 RPC 处理程序。
  - `RpcHandlers.cs`: 共享 RPC 契约的服务端部分。
  - `ExampleStateSystem.cs`: 一个示例系统，由服务器逐帧调用。
- `Content/manifest.json`: 客户端模组的清单文件，包含名称、版本和描述等元数据。服务端模组不需要清单文件。

## SDK 包

SDK 由三个 NuGet 包提供，每个项目对应一个，因此每个项目只能看到其运行进程中实际存在的程序集。

- `ReadyM.SDK.Wukong.Common`: 共享类型与源生成器，由 `ExampleMod.Common` 引用。
- `ReadyM.SDK.Wukong.Client`: 客户端 SDK 与模组加载器程序集，由 `ExampleMod` 引用。
- `ReadyM.SDK.Wukong.Server`: 服务端 SDK，由 `ExampleMod.Serverside` 引用。

Client 与 Server 都依赖 Common，且互不依赖。这样共享代码就无法引用仅限客户端的 API，
避免了服务端加载时才暴露的错误。

游戏本体的程序集来自 [`ReadyM.Wukong.GameRefs`](https://github.com/readycodeio/wukong-game-refs)
NuGet 包，由客户端项目引用。这些是「仅引用」程序集：只包含 API 签名，不含方法实现，
仅在编译时使用。游戏运行时进程中已加载真实程序集，因此无需随模组一起分发。

需要注意：使用这些「仅引用」程序集时，您无法单步调试游戏代码，也无法在游戏之外执行游戏代码。
如有需要，请移除 `ReadyM.Wukong.GameRefs` 包引用，改为添加指向您自己游戏安装中完整程序集的
`<Reference>` 项。两种方式的编译结果完全相同。

## 打包模组

1. 请务必使用正确的模组信息（例如名称、版本和描述）编辑 `manifest.json` 文件。
2. 编辑 `ModFiles.ps1` 以添加模组使用的任何额外文件。
3. 运行带有 `Release` 参数的 `MakeModFolder.ps1` 脚本。该脚本会构建全部三个项目并完成打包。
4. 结果是 `Output` 目录中的一个模组文件夹：
   - `Output/mods/ExampleMod/manifest.json`: 客户端与服务端都会读取。
   - `Output/mods/ExampleMod/client`: 玩家加入时下发给玩家。
   - `Output/mods/ExampleMod/server`: 仅由服务器加载。
5. 将整个 `Output/mods/ExampleMod` 文件夹复制到服务器的 `mods/` 目录。
6. 重启服务器。

服务器会把 `client` 文件夹下发给玩家，并将 `server` 文件夹保留在服务端，因此只需安装一个文件夹，
两端便都能获得所需内容。任何不应交给玩家的内容，例如仅供服务端使用的配置文件，都只能放在 `server` 中。

## 调试

使用带有 `Debug` 参数的 `MakeModFolder.ps1` 脚本来创建模组的调试版本，其中包含用于调试目的的额外文件。

在调试模组之前，您需要在 WukongMP 中启用调试器。

### 启用调试器

若要在模组化的 WukongMP 中启用调试器，请按照以下步骤操作：

1. 导航至 `%APPDATA%\ReadyM.Launcher\DownloadCache\Loader`
2. 进入版本号最新的文件夹，例如 `0.7.457.1630`
3. 找到 `@APPDATA\CSharpLoader\b1cs.ini` 文件并编辑以下设置：

```ini
[Settings]
Develop=1       # 启用调试器
Console=1       # 显示控制台窗口
EnableJit=1     # 必需，请勿更改
```

下次启动游戏时，Mono 调试器服务器将在端口 `44446` 上启用。
您可以通过编辑同一文件夹中的 `debugger-agent.txt` 文件来更改调试器设置。

调试器代理（Debugger agent）的默认设置如下：

```txt
transport=dt_socket,loglevel=0,address=127.0.0.1:44446,server=y,suspend=n
```

### 从 JetBrains Rider 连接

1. 前往 `Run > Edit Configurations`。
2. 点击 `+` 按钮并选择 `Mono Remote`。
3. 将 `Name` 设置为类似 `WukongMP Debugger` 的名称。
4. 将 `Host` 设置为 `localhost`，并将 `Port` 设置为 `44446`（或您在 `debugger-agent.txt` 中配置的端口）。
5. 点击 `Apply` 然后点击 `OK`。

现在您可以启动游戏，然后在 JetBrains Rider 中运行 `WukongMP Debugger` 配置以连接到调试器。
您应该能在 Rider 中看到调试器控制台，并且可以在模组代码中设置断点进行调试。

> **注意**：调试器初次连接可能需要几秒钟，并显示消息 `Waiting for target to get ready`。一旦连接成功，状态将变为 `Target ready`。