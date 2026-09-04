using ExampleMod.Common;
using Microsoft.Extensions.Logging;
using ReadyM.Api.ECS.Worlds;
using ReadyM.Relay.Server.Sdk;
using ReadyM.Relay.Server.Sdk.Ecs.Components;
using WukongMp.Sdk.Serverside;

namespace ExampleMod.Serverside;

// The relay server reads the manifest at the mod folder root, then instantiates the ServerModBase
// it finds in the server subfolder. Both sides share that one manifest.
public class Mod : ServerModBase
{
    protected override void RegisterComponents(IComponentRegistry registry)
    {
        // register a component to be synchronized over the network.
        registry.RegisterComponent<ExampleComponent>();
    }

    protected override void Init()
    {
        // RPC handler classes and systems have to be registered before they run
        Services.RegisterSingleton<RpcHandlers>();
        Services.RegisterSystem<ExampleStateSystem>();
        
        // Archetypes are the "prefabs" of ECS entities. You can modify existing archetypes or create new ones.
        var archetypeRegistry = Services.Resolve<IArchetypeRegistry>();
        archetypeRegistry.ModifyArchetype(WukongArchetypes.GlobalPlayerArchetype, archetype =>
        {
            archetype.Add<ExampleComponent>();
        });

        var logger = Services.Resolve<ILogger>();
        logger.LogInformation("Example server mod initialized");
    }
}
