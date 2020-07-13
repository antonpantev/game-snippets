using UnityEngine;

namespace Toolbox
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Movement2D))]
    public class Arrive2D : MonoBehaviour
    {
        /* The radius from the target that means we are close enough and have arrived */
        public float targetRadius = 0.005f;

        /* The radius from the target where we start to slow down  */
        public float slowRadius = 0.6f;

        /* The time in which we want to achieve the targetSpeed */
        public float timeToTarget = 0.1f;

        Rigidbody2D rb;
        Movement2D movement;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            movement = GetComponent<Movement2D>();
        }

        public Steering2D GetSteering(Vector2 targetPos)
        {
            Vector2 targetVelocity = targetPos - (Vector2)transform.position;

            float dist = targetVelocity.magnitude;

            /* If we are within the stopping radius then stop. */
            if (dist < targetRadius)
            {
                return Steering2D.Stop;
            }

            /* Calculate the target speed, full speed at slowRadius distance and 0 speed at 0 distance */
            float targetSpeed = movement.maxSpeed;

            if (dist < slowRadius)
            {
                targetSpeed = movement.maxSpeed * (dist / slowRadius);
            }

            /* Give targetVelocity the correct speed */
            targetVelocity.Normalize();
            targetVelocity *= targetSpeed;

            /* Calculate the linear acceleration we want */
            Vector2 acceleration = targetVelocity - rb.velocity;
            /* Rather than accelerate the character to the correct speed in 1 second, 
             * accelerate so we reach the desired speed in timeToTarget seconds 
             * (if we were to actually accelerate for the full timeToTarget seconds). */
            acceleration *= 1 / timeToTarget;

            /* Make sure we are accelerating at max acceleration */
            if (acceleration.magnitude > movement.accel)
            {
                acceleration.Normalize();
                acceleration *= movement.accel;
            }

            return new Steering2D
            {
                force = rb.mass * acceleration,
                isMoving = true
            };
        }
    }
}