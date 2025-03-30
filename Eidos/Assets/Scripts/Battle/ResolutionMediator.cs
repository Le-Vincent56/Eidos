using Eidos.Mediator;
using Eidos.ServiceLocators;

namespace Eidos.Battles.Resolution
{
    public class ResolutionMediator : Mediator<BattleComponent>
    {
        private void Awake() => ServiceLocator.ForSceneOf(this).Register(this);

        protected override bool MediatorConditionMet(BattleComponent target) => systems.Contains(target);
    }
}
