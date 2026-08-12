namespace ExampleMod.Common;

// Any type both sides need to agree on belongs in this project, so the client and
// the server can never drift apart.
public enum ExampleState : byte
{
    Idle,
    Busy
}
