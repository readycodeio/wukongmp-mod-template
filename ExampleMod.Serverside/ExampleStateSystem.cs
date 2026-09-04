using ExampleMod.Common;
using Microsoft.Extensions.Logging;
using ReadyM.Relay.Server.Sdk.Ecs;
using ReadyM.Relay.Server.Sdk.Ecs.Systems;
using ReadyM.Wukong.Common.ECS.Components;

namespace ExampleMod.Serverside;

// Systems get ticked by the server every frame. Query the ECS world for the state
// you need and push changes to the clients over RPC.
public class ExampleStateSystem(EcsApi ecs, RpcHandlers rpc, ILogger logger) : ModSystemBase
{
    private const float ToggleIntervalSeconds = 30f;

    private ExampleState _state = ExampleState.Idle;
    private float _timer = ToggleIntervalSeconds;

    protected override void OnUpdate(UpdateTick tick)
    {
        _timer -= tick.DeltaTime;
        if (_timer > 0f)
            return;

        _timer += ToggleIntervalSeconds;
        _state = _state == ExampleState.Idle ? ExampleState.Busy : ExampleState.Idle;

        BroadcastState();
    }

    private void BroadcastState()
    {
        logger.LogDebug("Broadcasting example state {State}", _state);
        
        ecs.Query<MainCharacterComponent>((ref player) =>
        {
            rpc.SendExampleStateChanged(player.PlayerId, (byte)_state);
        });
    }
}