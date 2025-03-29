using Eidos.EventBus;

namespace Eidos.Battles.States
{
    public class EndBattleState : BattleState
    {
        public EndBattleState(TurnSystem turnSystem) : base(turnSystem)
        {
        }

        public override void OnEnter() => EventBus<EndBattle>.Raise(new EndBattle());
        public override void OnExit() => EventBus<ExitBattle>.Raise(new ExitBattle());
    }
}
