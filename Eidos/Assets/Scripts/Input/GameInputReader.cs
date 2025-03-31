using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Eidos.Input.GameplayActions;

namespace Eidos.Input
{
    [CreateAssetMenu(fileName = "Game Input Reader", menuName = "Input/Game Input Reader")]
    public class GameInputReader : ScriptableObject, IPlayerActions
    {
        private GameplayActions inputActions;

        public event Action<bool> Interact = delegate { };

        public Vector2 NormalizedInput { get; private set; }

        private void OnEnable() => Enable();

        private void OnDisable() => Disable();


        /// <summary>
        /// Enable the input actions
        /// </summary>
        public void Enable()
        {
            // Check if the input actions have been initialized
            if (inputActions == null)
            {
                // Initialize the input actions and set callbacks
                inputActions = new GameplayActions();
                inputActions.Player.SetCallbacks(this);
            }

            // Enable the input actions
            inputActions.Enable();
        }

        /// <summary>
        /// Disable the input actions
        /// </summary>
        public void Disable() => inputActions.Disable();



        public void OnMove(InputAction.CallbackContext context)
        {
            // Get the raw movement input from the control
            Vector2 rawMovementInput = context.ReadValue<Vector2>();

            // Normalize the input vector
            NormalizedInput = rawMovementInput.normalized;
        }

        public void OnInteract(InputAction.CallbackContext context) { }
    }
}
