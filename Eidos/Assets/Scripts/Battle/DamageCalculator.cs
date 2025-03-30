using Eidos.Battles.Resolution;
using Eidos.ServiceLocators;
using Eidos.Visitor;

namespace Eidos.Battles
{
    public class DamageCalculator : BattleComponent
    {
        private ResolutionMediator mediator;

        private void Start()
        {
            // Register this to the mediator as a component
            mediator = ServiceLocator.ForSceneOf(this).Get<ResolutionMediator>();
            mediator.Register(this);
        }

        public override void Accept(IVisitor message)
        {
            message.Visit(this);
        }

        private float CalculateDamage()
        {
            return 20f;
        }
    }
}
