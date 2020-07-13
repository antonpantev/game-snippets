using System.Collections;
using UnityEngine;

namespace Toolbox
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Movement2D))]
    public class WanderSeek2D : MonoBehaviour
    {
        public float angleOffset = 80f;

        public float minDuration = 0.15f;

        public float maxDuration = 0.5f;

        /// <summary>
        /// The physics layers that the character should avoid walking into
        /// (like the layer that the walls use).
        ///
        /// You will probably want to exclude the layer that your target uses
        /// so the character can seek into the target.
        ///
        /// Sometimes it looks better if the character doesn't worry about
        /// walking into other characters, so don't just add every layer the
        /// character cannot move through.
        /// </summary>
        public LayerMask lineOfSightMask = Physics2D.DefaultRaycastLayers;

        float colliderRadius;
        Rigidbody2D rb;
        Movement2D movement;
        Vector2 dir;
        Vector2? targetPos;

        void Start()
        {
            colliderRadius = GetComponent<CircleCollider2D>().radius;
            rb = GetComponent<Rigidbody2D>();
            movement = GetComponent<Movement2D>();

            StartCoroutine(NextDirection());
        }

        IEnumerator NextDirection()
        {
            float duration = Random.Range(minDuration, maxDuration);

            if (targetPos != null)
            {
                /* Try 5 times to find a new wander direction. */
                for (int i = 0; i < 5; i++)
                {
                    Vector2 delta = (targetPos.Value - (Vector2)transform.position);

                    float distToPlayer = delta.magnitude;

                    float angle = Random.Range(-angleOffset, angleOffset);
                    Quaternion rotation = Quaternion.Euler(0, 0, angle);

                    delta.Normalize();
                    delta = rotation * delta;

                    float maxDist = Mathf.Min(movement.maxSpeed * duration, distToPlayer);

                    RaycastHit2D hit = Utils.CircleCast(transform.position, colliderRadius, delta, maxDist, lineOfSightMask);

                    if (hit.collider == null)
                    {
                        dir = delta;
                        break;
                    }
                }
            }

            yield return new WaitForSeconds(duration);

            StartCoroutine(NextDirection());
        }

        public Steering2D GetSteering(Vector2 targetPos)
        {
            this.targetPos = targetPos;

            float accel = movement.accel * Random.value;

            return new Steering2D
            {
                force = rb.mass * dir * accel,
                isMoving = true
            };
        }
    }
}