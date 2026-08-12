using ExampleMod.Common;
using ReadyM.Api.Multiplayer;
using ReadyM.Api.Multiplayer.RPC;
using UnrealEngine.Runtime;
using WukongMp.Sdk.Api;

namespace ExampleMod;

// The client half of the shared contracts: every [ServerToClient] call shows up as
// an On* partial method, and every [ClientToServer] call as a Send* method.
// RPCs are dispatched off the game thread, so wrap anything that touches the game
// in RunOnGameThread.
[ServerRpcFor(typeof(ExampleRpcContracts))]
public partial class ExampleServerRpc : ServerRpcClient
{
    partial void OnPlayerCountReply(int players)
    {
        RunOnGameThread(() =>
        {
            WukongApi.Chat.ShowLocalMessage($"The server reports {players} player(s) in game.", FLinearColor.Gray);
        });
    }

    partial void OnExampleStateChanged(byte state)
    {
        var exampleState = (ExampleState)state;

        RunOnGameThread(() =>
        {
            WukongApi.Chat.ShowLocalMessage($"Example state is now {exampleState}.", FLinearColor.Gray);
        });
    }
}
