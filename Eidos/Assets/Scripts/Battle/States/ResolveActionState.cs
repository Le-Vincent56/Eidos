using Eidos.Battles.Resolution;
using Eidos.EventBus;
using Eidos.ServiceLocators;
using System.Collections.Generic;
using UnityEngine;

namespace Eidos.Battles.States
{
    public class ResolveActionState : BattleState
    {
        private LinkedList<Awaitable> processes;
        private readonly ResolutionMediator mediator;

        public ResolveActionState(TurnSystem turnSystem) : base(turnSystem)
        {
            processes = new LinkedList<Awaitable>();
            mediator = ServiceLocator.ForSceneOf(turnSystem).Get<ResolutionMediator>();
        }

        public override void OnEnter()
        {
            EventBus<ResolveAction>.Raise(new ResolveAction());


        }
    }
}
