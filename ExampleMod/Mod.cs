using ReadyM.Api.DI;
using WukongMp.Sdk;

namespace ExampleMod;

public class Mod : ModBase
{
    public override string Name => "Example Mod";

    public override string Version => "1.0.0";

    protected override void Initialize(IDependencyContainer services)
    {
        // register your services here, for example:
        // services.RegisterSingleton<IMyService, MyService>();
    }
}