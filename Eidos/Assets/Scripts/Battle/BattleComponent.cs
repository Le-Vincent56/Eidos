using Eidos.Visitor;
using UnityEngine;

namespace Eidos.Battles
{
    public class BattleComponent : MonoBehaviour, IVisitable
    {
        public virtual void Accept(IVisitor message) { }
    }
}
