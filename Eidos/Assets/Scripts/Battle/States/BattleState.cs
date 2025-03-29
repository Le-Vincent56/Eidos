using Eidos.StateMachines;

namespace Eidos.Battles.States
{
    public class BattleState : IState
    {
        protected readonly TurnSystem turnSystem;

        public BattleState(TurnSystem turnSystem)
        {
            this.turnSystem = turnSystem;
        }

        public virtual void OnEnter() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void OnExit() { }
    }
}
