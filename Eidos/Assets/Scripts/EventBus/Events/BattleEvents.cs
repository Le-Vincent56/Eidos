using UnityEngine;

namespace Eidos.EventBus
{
    public struct StartBattle : IEvent { }
    public struct ChooseAction : IEvent { }
    public struct ResolveAction : IEvent { }
    public struct EndBattle : IEvent { }
    public struct ExitBattle : IEvent { }
}
