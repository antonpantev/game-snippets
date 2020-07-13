using UnityEngine;

namespace Toolbox
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Arrive2D))]
    public class FollowPath2D : MonoBehaviour
    {
        public float stopRadius = 0.005f;

        public float pathOffset = 0.6f;

        public float pathDirection = 1f;

        Rigidbody2D rb;
        Arrive2D arrive;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            arrive = GetComponent<Arrive2D>();
        }

        public Steering2D GetSteering(LinePath path, bool pathLoop = false)
        {
            Vector2 targetPosition;

            /* If the path has only one node then just go to that position. */
            if (path.Length == 1)
            {
                targetPosition = path[0];
            }
            /* Else find the closest spot on the path to the character and go to that instead. */
            else
            {
                /* Get the param for the closest position point on the path given the character's position */
                float param = path.GetParam(transform.position);

                //Debug.DrawLine(transform.position, path.getPosition(param, pathLoop), Color.red, 0, false);

                /* Move down the path */
                param += pathDirection * pathOffset;

                /* Set the target position */
                targetPosition = path.GetPosition(param, pathLoop);

                //Debug.DrawLine(transform.position, targetPosition, Color.red, 0, false);
            }

            return arrive.GetSteering(targetPosition);
        }

        /// <summary> 
        /// Will return true if the character is at the end of the given path 
        /// </summary>
        public bool IsAtEndOfPath(LinePath path)
        {
            /* If the path has only one node then just check the distance to that node. */
            if (path.Length == 1)
            {
                return Vector2.Distance(rb.position, path[0]) < stopRadius;
            }
            /* Else see if the character is at the end of the path. */
            else
            {
                Vector2 finalDestination;

                /* Get the param for the closest position point on the path given the character's position */
                float param = path.GetParam(transform.position);

                return IsAtEndOfPath(path, param, out finalDestination);
            }
        }

        private bool IsAtEndOfPath(LinePath path, float param, out Vector2 finalDestination)
        {
            bool result;

            /* Find the final destination of the character on this path */
            finalDestination = (pathDirection > 0) ? path[path.Length - 1] : path[0];

            /* If the param is closest to the last segment then check if we are at the final destination */
            if (param >= path.distances[path.Length - 2])
            {
                result = Vector2.Distance(rb.position, finalDestination) < stopRadius;
            }
            /* Else we are not at the end of the path */
            else
            {
                result = false;
            }

            return result;
        }
    }
}