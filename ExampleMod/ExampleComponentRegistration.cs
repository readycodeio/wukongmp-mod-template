using ExampleMod.Common;
using ReadyM.Api.ECS.Registry;
using ReadyM.Api.ECS.Worlds;
using WukongMp.Sdk.Api;

namespace ExampleMod;

public class ExampleComponentRegistration : IArchetypeRegistration
{
    public void Register(IArchetypeRegistry registry)
    {
        registry.ModifyArchetype(WukongApi.Archetypes.GlobalPlayerArchetype, b => b.Add<ExampleComponent>());
    }
}
