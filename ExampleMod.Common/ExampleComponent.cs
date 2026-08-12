using System.Runtime.InteropServices;
using ReadyM.Api.Multiplayer.Generators;

namespace ExampleMod.Common;

[DeriveINetworkedComponent]
[StructLayout(LayoutKind.Auto)]
public partial struct ExampleComponent
{
    private ExampleState _someState;
}