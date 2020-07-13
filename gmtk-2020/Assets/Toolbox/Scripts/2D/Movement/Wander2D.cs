using Toolbox;
using UnityEngine;

namespace Toolbox
{
    /// <summary>
    /// Wander to random points with in a circle around the character.
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Arrive2D))]
    public class Wander2D : MonoBehaviour
    {
        /// <summary>
        /// The radius around the character where the next wander target will
        /// be found. Keep it greater than or equal to arrive's slow radius.
        /// </summary>
        public float radius = 1f;

        /// <summary>
        /// How long it takes the character to pick a new wander target when
        /// the character is not making progress towards the current wander
        /// target.
        /// </summary>
        public float reactionTime = 0.2f;

        /// <summary>
        /// The physics layers that the character should avoid walking into
        /// (like the layer that the walls use).
        ///
        /// Sometimes it looks better if the character doesn't worry about
        /// walking into other characters, so don't just add every layer the
        /// character cannot move through (try each layer out on and off).
        /// </summary>
        public LayerMask lineOfSightMask = Physics2D.DefaultRaycastLayers;

        float colliderRadius;
        Arrive2D arrive;
        float lastDist;
        float lastMoveTime;
        Vector2 target;

        void Start()
        {
            colliderRadius = GetComponent<CircleCollider2D>().radius;
            arrive = GetComponent<Arrive2D>();
            target = transform.position;
            NextTarget();
        }

        public Steering2D GetSteering()
        {
            Steering2D steering = arrive.GetSteering(target);

            if (!steering.isMoving || IsNotMovingCloser())
            {
                NextTarget();
                steering = arrive.GetSteering(target);
            }

            return steering;
        }

        bool IsNotMovingCloser()
        {
            float dist = Vector2.Distance(transform.position, target);

            if (dist < lastDist)
            {
                lastMoveTime = Time.time;
            }

            lastDist = dist;

            return (Time.time - lastMoveTime) >= reactionTime;
        }

        void NextTarget()
        {
            /* Try 5 times to find a new wander target. */
            for (int i = 0; i < 5; i++)
            {
                Vector3 dir = Random.insideUnitSphere;

                RaycastHit2D hit = Utils.CircleCast(transform.position, colliderRadius, dir, radius, lineOfSightMask);

                if (hit.collider == null)
                {
                    lastDist = Mathf.Infinity;
                    lastMoveTime = Time.time;
                    target = transform.position + (dir * radius);

                    break;
                }
            }
        }
    }
}