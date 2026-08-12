using ExampleMod.Common;
using Microsoft.Extensions.Logging;
using ReadyM.Api.Multiplayer;
using ReadyM.Relay.Server.Sdk.Ecs;
using ReadyM.Relay.Server.Sdk.Rpc;
using ReadyM.Wukong.Common.ECS.Components;

namespace ExampleMod.Serverside;

// The server half of the shared contracts: every [ClientToServer] call shows up as
// an On* partial method, and every [ServerToClient] call as a Send* method that
// takes the target player as its first argument.
[ServerRpcFor(typeof(ExampleRpcContracts))]
public partial class RpcHandlers(EcsApi ecs, ILogger logger) : ServerRpcHandlersBase
{
    partial void OnRequestPlayerCount(RpcContext context)
    {
        var players = 0;
        ecs.Query<MainCharacterComponent>((ref _) => { players++; });

        logger.LogDebug("{Player} asked for the player count, answering {Players}", context.Sender, players);
        SendPlayerCountReply(context.Sender, players);
    }
}
