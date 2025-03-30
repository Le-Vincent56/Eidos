using UnityEngine;

namespace Eidos.Visitor
{
    public interface IVisitor
    {
        void Visit<T>(T visitable) where T : Component, IVisitable;
    }
}
