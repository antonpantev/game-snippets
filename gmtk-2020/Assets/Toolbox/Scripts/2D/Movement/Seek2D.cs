using UnityEngine;

namespace Toolbox
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Movement2D))]
    public class Seek2D : MonoBehaviour
    {
        Rigidbody2D rb;
        Movement2D movement;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            movement = GetComponent<Movement2D>();
        }

        public Steering2D GetSteering(Vector2 targetPos)
        {
            Vector2 dir = (targetPos - (Vector2)transform.position).normalized;

            return new Steering2D
            {
                force = rb.mass * dir * movement.accel,
                isMoving = true
            };
        }
    }
}