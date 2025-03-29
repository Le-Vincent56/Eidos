using Eidos.EventBus;

namespace Eidos.Battles.States
{
    public class StartBattleState : BattleState
    {
        public StartBattleState(TurnSystem turnSystem) : base(turnSystem)
        {
        }

        public override void OnEnter() => EventBus<StartBattle>.Raise(new StartBattle());
    }
}
