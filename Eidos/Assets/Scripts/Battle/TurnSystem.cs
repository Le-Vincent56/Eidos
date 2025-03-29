using Eidos.Battles.States;
using Eidos.StateMachines;
using UnityEngine;

namespace Eidos.Battles
{
    public class TurnSystem : MonoBehaviour
    {
        [Header("Fields")]
        [SerializeField] private int state = 0;
        private StateMachine stateMachine;

        public int NONE { get => 0; }
        public int START { get => 1; }
        public int CHOOSE { get => 2; }
        public int RESOLVE { get => 3; }
        public int END { get => 4; }

        private void Awake()
        {
            // Set up the state machine
            SetupStateMachine();
        }

        private void Update()
        {
            // Update the state machine
            stateMachine.Update();
        }

        /// <summary>
        /// Set up the Turn-based State Machine
        /// </summary>
        private void SetupStateMachine()
        {
            // Initialize the state machine
            stateMachine = new StateMachine();

            // Create the states
            NoBattleState none = new NoBattleState(this);
            StartBattleState start = new StartBattleState(this);
            ChooseActionState chooseAction = new ChooseActionState(this);
            ResolveActionState resolveAction = new ResolveActionState(this);
            EndBattleState end = new EndBattleState(this);

            // Define state transitions
            stateMachine.At(none, start, new FuncPredicate(() => state == START));
            stateMachine.At(start, chooseAction, new FuncPredicate(() => state == CHOOSE));
            stateMachine.At(chooseAction, resolveAction, new FuncPredicate(() => state == RESOLVE));
            stateMachine.At(resolveAction, chooseAction, new FuncPredicate(() => state == CHOOSE));
            stateMachine.At(resolveAction, end, new FuncPredicate(() => state == END));
            stateMachine.At(end, none, new FuncPredicate(() => state == NONE));

            // Set the initial state
            stateMachine.SetState(none);
        }

        /// <summary>
        /// Set the state of the Turn System
        /// </summary>
        public void SetState(int state) => this.state = state;
    }
}
