using System;

namespace Eidos.StateMachines
{
    public class FuncPredicate : IPredicate
    {
        readonly Func<bool> func;

        public FuncPredicate(Func<bool> func)
        {
            this.func = func;
        }

        /// <summary>
        /// Evaluate the predicate
        /// </summary>
        public bool Evaluate() => func.Invoke();
    }
}