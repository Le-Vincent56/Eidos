using Eidos.Input;
using UnityEngine;

namespace Eidos
{
    public class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameInputReader inputReader;
        private Rigidbody2D rb;

        [Header("Fields")]
        [SerializeField] private float speed;

        private void Awake()
        {
            // Get components
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // Get the normalized movement vector
            Vector2 normalizedMovement = inputReader.NormalizedInput;
            
            // Set the linear velocity of the rigidbody
            rb.linearVelocity = normalizedMovement * speed;
        }
    }
}
