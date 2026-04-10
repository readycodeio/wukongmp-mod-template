using ReadyM.Api.Command;
using ReadyM.Api.DI;
using ReadyM.Api.Idents;
using ReadyM.Api.Multiplayer.Client;
using ReadyM.Api.Multiplayer.Generators;
using ReadyM.Api.Multiplayer.Protocol.Enums;
using ReadyM.Api.Multiplayer.RPC;
using ReadyM.Api.Multiplayer.Serialization;
using UnrealEngine.Runtime;
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
        var rpc = services.Resolve<ExampleRpc>();

        // use the WukongApi class to interact with the SDK, for example:
        WukongApi.Console.AddCommand("example_command", ConsoleCommand.Create(() =>
        {
            WukongApi.Local.AddChatMessage("Example command executed!", FLinearColor.Orange);
            rpc.SendExampleEvent("Hello from the example command!");
        }));
    }
}

public partial class ExampleRpc(IRpcClient client, IRelaySerializer serializer) : RpcClassBase(client, serializer)
{
    // define your RPC methods here, for example:
    [RpcEvent(RelayMode.AreaOfInterestAll)]
    private void OnExampleEvent(PlayerId __sender, string message)
    {
        WukongApi.Local.AddChatMessage($"Received message from {__sender}: {message}", FLinearColor.Green);
    }
}