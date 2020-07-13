using System.Collections.Generic;
using UnityEngine;

namespace Toolbox
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement2D : MonoBehaviour
    {
        public float accel = 35f;
        public float stoppingDrag = 25f;
        public float movingDrag;
        public float maxSpeed = 4f;

        /// <summary>
        /// The steering force that the game object should move each FixedUpdate.
        /// </summary>
        /// <remarks>
        /// The steering force will be applied every FixedUpdate so make sure you
        /// keep it correct by setting it every FixedUpdate or be sure you want
        /// it applied for all future FixedUpdates if you set it once.
        /// </remarks>
        [System.NonSerialized]
        public Steering2D steering = Steering2D.Stop;

        Rigidbody2D rb;
        List<ForceTime2D> knockbacks = new List<ForceTime2D>();

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            knockbacks.RemoveAll(ft => ft.endTime <= Time.fixedTime);

            if (!steering.isMoving && knockbacks.Count == 0)
            {
                rb.drag = stoppingDrag;
            }
            else
            {
                rb.drag = movingDrag;

                /* If there are knockbacks on the character then only apply
                 * those forces and not the character movement force. */
                if (knockbacks.Count > 0)
                {
                    foreach (ForceTime2D ft in knockbacks)
                    {
                        rb.AddForce(ft.force);
                    }
                }
                else
                {
                    rb.AddForce(steering.force);
                }
            }

            if (knockbacks.Count == 0 && rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }

        public void AddKnockback(Vector2 force, float duration)
        {
            ForceTime2D ft = new ForceTime2D(force, Time.fixedTime + duration);
            knockbacks.Add(ft);
        }
    }

    struct ForceTime2D
    {
        public Vector2 force;
        public float endTime;

        public ForceTime2D(Vector2 force, float endTime)
        {
            this.force = force;
            this.endTime = endTime;
        }
    }
}