using ReadyM.Api.Multiplayer;

namespace ExampleMod.Common;

// Single source of truth for the RPCs between your client mod and your server mod.
// Both sides declare [ServerRpcFor(typeof(ExampleRpcContracts))] on a partial class;
// the generator gives each side a Send* method for the calls it may make and an On*
// partial method for the calls it has to handle.
[ServerRpcContracts]
public static partial class ExampleRpcContracts
{
    [ClientToServer] public static partial void RequestPlayerCount();
    [ServerToClient] public static partial void PlayerCountReply(int players);
    [ServerToClient] public static partial void ExampleStateChanged(byte state);
}
