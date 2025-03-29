using Eidos.EventBus;

namespace Eidos.Battles.States
{
    public class ResolveActionState : BattleState
    {
        public ResolveActionState(TurnSystem turnSystem) : base(turnSystem)
        {
        }

        public override void OnEnter() => EventBus<ResolveAction>.Raise(new ResolveAction());
    }
}
