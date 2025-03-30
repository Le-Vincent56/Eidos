using UnityEngine;
using Eidos.Visitor;
using System.Collections.Generic;
using System;
using Sirenix.Utilities;
using System.Linq;

namespace Eidos.Mediator
{
    public abstract class Mediator<T> : MonoBehaviour where T : Component, IVisitable
    {
        protected readonly List<T> systems = new List<T>();

        /// <summary>
        /// Register a system to the mediator
        /// </summary>
        public void Register(T system)
        {
            // Exit case - system is already registered
            if (systems.Contains(system)) return;

            systems.Add(system);
        }

        /// <summary>
        /// Deregister a system from the mediator
        /// </summary>
        public void Deregister(T system)
        {
            // Exit case - the system is not already registered
            if(!systems.Contains(system)) return;

            systems.Remove(system);
        }

        public void Message(T source, T target, IVisitor message) 
        {
            // Get the first or default system that matches the target
            T targetSystem = systems.FirstOrDefault(system => system.Equals(target));

            // If it exists, accept the message
            targetSystem?.Accept(message);
        }


        public void Broadcast(T source, IVisitor message, Func<T, bool> predicate = null)
        {
            // Get all targets that are not the source and that meet the conditions of the predicate
            IEnumerable<T> targets = systems.Where(
                target => source != target 
                && SenderConditionMet(target, predicate)
                && MediatorConditionMet(target));

            // Send the message to each target and have them accept
            targets.ForEach(target => target.Accept(message));

        }

        private bool SenderConditionMet(T target, Func<T, bool> predicate) => predicate == null || predicate(target);
        protected abstract bool MediatorConditionMet(T target);
    }
}
