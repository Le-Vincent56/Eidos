using Eidos.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Eidos.Interactables
{
    public class Interactor : SerializedMonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameInputReader inputReader;

        [Header("Fields")]
        [SerializeField] private IInteractable currentInteractable;

        private void OnEnable()
        {
            inputReader.Interact += Interact;
        }

        private void OnDisable()
        {
            inputReader.Interact -= Interact;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Exit case - if the object being collided with isn't an interactable
            if (!collision.TryGetComponent(out IInteractable interactable)) return;

            // Set the current interactable to the interactable being collided with
            currentInteractable = interactable;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // Exit case - if the object being collided with isn't the current interactable
            if (!collision.TryGetComponent(out IInteractable interactable)) return;

            // Exit case - if the interactable being collided with isn't the current interactable
            if (interactable != currentInteractable) return;

            // Set the current interactable to null
            currentInteractable = null;
        }

        /// <summary>
        /// Attempt to interact with the current interactable
        /// </summary>
        private void Interact(bool started)
        {
            // Exit case - if the button is being lifted
            if (!started) return;

            // Interact with the current interactable, if it exists
            currentInteractable?.Interact();
        }
    }
}
