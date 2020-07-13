using UnityEngine;

namespace Toolbox
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Movement2D))]
    public class TopDownPlayer2D : MonoBehaviour
    {
        public string horAxisName = "Horizontal";
        public string vertAxisName = "Vertical";

        float horAxis;
        float vertAxis;
        Rigidbody2D rb;
        Movement2D movement;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            movement = GetComponent<Movement2D>();
        }

        void Update()
        {
            horAxis = Input.GetAxisRaw(horAxisName);
            vertAxis = Input.GetAxisRaw(vertAxisName);
        }

        void FixedUpdate()
        {
            Vector2 dir = new Vector2(horAxis, vertAxis).normalized;
            movement.steering.force = rb.mass * movement.accel * dir;
            movement.steering.isMoving = dir != Vector2.zero;
        }
    }
}