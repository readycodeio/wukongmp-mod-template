using Microsoft.Extensions.Logging;
using ReadyM.Api.Command;
using ReadyM.Api.DI;
using WukongMp.Sdk;
using WukongMp.Sdk.Api;

namespace ExampleMod;

public class Mod : ModBase
{
    public override string Name => "Example Mod"; // TODO: CHANGE ME

    public override string Version => "1.0.0";

    protected override void Initialize(IDependencyContainer services)
    {
        // register and resolve your services here, for example:
        // services.RegisterSingleton<IMyService, MyService>();
        // var myService = services.Resolve<IMyService>();

        // use the WukongApi class to interact with the SDK, for example:
        WukongApi.Console.AddCommand("example_command", ConsoleCommand.Create(() => { Logger.LogInformation("Example command executed!"); }));
    }
}