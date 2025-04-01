using Eidos.Eidras;

namespace Eidos.EventBus
{
    public struct EngageBattle : IEvent 
    {
        public string CatalystName;
        public EidraData[] Eidras;
    }

    public struct StartBattle : IEvent { }
    public struct ChooseAction : IEvent { }
    public struct ResolveAction : IEvent { }
    public struct EndBattle : IEvent { }
    public struct ExitBattle : IEvent { }
}
