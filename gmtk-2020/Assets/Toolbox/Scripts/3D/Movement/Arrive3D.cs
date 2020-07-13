using UnityEngine;

namespace Toolbox
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Movement3D))]
    public class Arrive3D : MonoBehaviour
    {
        /* The radius from the target that means we are close enough and have arrived */
        public float targetRadius = 0.005f;

        /* The radius from the target where we start to slow down  */
        public float slowRadius = 0.6f;

        /* The time in which we want to achieve the targetSpeed */
        public float timeToTarget = 0.1f;

        Rigidbody rb;
        Movement3D movement;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            movement = GetComponent<Movement3D>();
        }

        public Steering3D GetSteering(Vector3 targetPos)
        {
            Vector3 targetVelocity = targetPos - (Vector3)transform.position;

            float dist = targetVelocity.magnitude;

            /* If we are within the stopping radius then stop. */
            if (dist < targetRadius)
            {
                return Steering3D.Stop;
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
            Vector3 acceleration = targetVelocity - rb.velocity;
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

            return new Steering3D
            {
                force = rb.mass * acceleration,
                isMoving = true
            };
        }
    }
}