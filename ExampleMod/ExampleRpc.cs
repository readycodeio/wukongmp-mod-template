using ReadyM.Api.Idents;
using ReadyM.Api.Multiplayer.Generators;
using ReadyM.Api.Multiplayer.Protocol.Enums;
using ReadyM.Api.Multiplayer.RPC;
using UnrealEngine.Runtime;
using WukongMp.Sdk.Api;

namespace ExampleMod;

// Client to client events. Both ends of an [RpcEvent] are game clients, so these
// live in the client project rather than in the shared one. For anything the server
// mod takes part in, see ExampleServerRpc and ExampleMod.Common.
public partial class ExampleRpc : ClientRpcHandler
{
    [RpcEvent(RelayMode.AreaOfInterestAll)]
    private void OnExampleEvent(PlayerId __sender, string message)
    {
        WukongApi.Chat.ShowLocalMessage($"Received message from {__sender}: {message}", FLinearColor.Green);
    }
}
