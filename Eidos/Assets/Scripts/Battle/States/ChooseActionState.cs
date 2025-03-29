using Eidos.EventBus;

namespace Eidos.Battles.States
{
    public class ChooseActionState : BattleState
    {
        public ChooseActionState(TurnSystem turnSystem) : base(turnSystem)
        {
        }

        public override void OnEnter() => EventBus<ChooseAction>.Raise(new ChooseAction());
    }
}
