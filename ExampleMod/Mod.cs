using CSharpModBase.Input;
using ExampleMod.Common;
using ReadyM.Api.Command;
using ReadyM.Api.DI;
using ReadyM.Api.ECS.Registry;
using UnrealEngine.Runtime;
using WukongMp.Api;
using WukongMp.Sdk;
using WukongMp.Sdk.Api;

namespace ExampleMod;

public class Mod : ModBase
{
    public override string Name => "Example Mod"; // TODO: CHANGE ME

    protected override void Initialize(IDependencyContainer services)
    {
        // register and resolve your services here, for example:
        services.RegisterSingleton<ExampleRpc>();
        services.RegisterSingleton<ExampleServerRpc>();

        // register custom components and attach them to archetypes, for example:
        services.RegisterSingleton<IArchetypeRegistration, ExampleComponentRegistration>();
        services.Resolve<IComponentApi>().RegisterComponent<ExampleComponent>();
    }

    public override void LateInit()
    {
        var rpc = WukongApi.Services.Resolve<ExampleRpc>();
        var serverRpc = WukongApi.Services.Resolve<ExampleServerRpc>();

        // use the WukongApi class to interact with the SDK, for example:
        WukongApi.Console.AddCommand("example_command", ConsoleCommand.Create(() =>
        {
            WukongApi.Chat.ShowLocalMessage("Example command executed!", FLinearColor.Orange);
        
            // send an event to the other players
            rpc.SendExampleEvent("Hello from the example command!");
        
            // ask the server mod something; the answer arrives in ExampleServerRpc
            serverRpc.SendRequestPlayerCount();
        }));

        // register input bindings, for example:
        WukongApi.Input.RegisterKeyBind(Key.F5, () => { WukongApi.Chat.ShowLocalMessage("F5 key pressed!", FLinearColor.Blue); });
    }
}

// use Harmony to patch a game method, for example: