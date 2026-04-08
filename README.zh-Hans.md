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

- `ExampleMod/Mod.cs`：你的模组主入口文件，可在此初始化并设置模组功能。
- `ExampleMod/manifest.json`: 你的模组的清单文件，包含名称、版本和描述等元数据。
- `SDK/WukongMp.Sdk`：可在模组开发中引用的 WukongMP SDK 文件。服务器二进制包中也包含相同文件。
- `SDK/Overrides`：基础版 **Black Myth: Wukong** 游戏中存在的 DLL 的覆盖版本，SDK 会用这些文件来确保与模组框架的兼容性。这些文件同样包含在服务器二进制包中。
- `Core`：我们分支维护的 Harmony 库，用于补丁修改游戏代码。
- `Game`：包含从基础游戏中提取出的原始 DLL 文件。

## 打包模组

1. 编辑 `ModFiles.ps1`，将模组所需的所有文件包含进去。
2. 运行带有参数 `Release` 的 `PackageMod.ps1` 脚本，以创建一个包含您的 mod 的 `.zip` 文件。
3. 生成的 `.zip` 文件可在 `Output` 目录中找到。
4. 将该 `.zip` 文件上传到服务器的 `mods/` 目录中进行部署。

## 调试

使用带有 `Debug` 参数的 `PackageMod.ps1` 脚本来创建模组（Mod）的调试版本，该版本会包含用于调试目的的额外文件。

在开始调试模组之前，您需要在 WukongMP 中启用调试器。

### 启用调试器

要在已安装模组的 WukongMP 中启用调试器，请按照以下步骤操作：

1. 导航至目录 `%APPDATA%\ReadyM.Launcher\DownloadCache\Loader`
2. 进入版本号最新的文件夹，例如 `0.7.457.1630`
3. 找到 `@APPDATA\CSharpLoader\b1cs.ini` 文件并编辑以下设置：

```ini
[Settings]
Develop=1       # 启用调试器
Console=1       # 显示控制台窗口
EnableJit=1     # 必须开启，请勿更改
```

下次启动游戏时，Mono 调试服务器将在端口 `44446` 上启用。
您可以通过编辑同一文件夹中的 `debugger-agent.txt` 文件来更改调试器设置。

调试代理的默认设置如下：

```txt
transport=dt_socket,loglevel=0,address=127.0.0.1:44446,server=y,suspend=n
```

---

### 从 JetBrains Rider 连接

1. 前往 **Run > Edit Configurations**（运行 > 编辑配置）。
2. 点击 **+** 按钮并选择 **Mono Remote**。
3. 将 **Name**（名称）设置为类似 `WukongMP Debugger` 的名称。
4. 将 **Host**（主机）设置为 `localhost`，将 **Port**（端口）设置为 `44446`（或您在 `debugger-agent.txt` 中配置的端口）。
5. 点击 **Apply**（应用），然后点击 **OK**（确定）。

现在您可以启动游戏，然后在 JetBrains Rider 中运行 `WukongMP Debugger` 配置以连接到调试器。
您应该能在 Rider 中看到调试控制台，此时您可以在模组代码中设置**断点**进行调试。

> **注意：** 调试器初次连接时可能需要几秒钟，并显示消息 `Waiting for target to get ready`（等待目标就绪）。连接成功后，状态将变为 `Target ready`（目标已就绪）。