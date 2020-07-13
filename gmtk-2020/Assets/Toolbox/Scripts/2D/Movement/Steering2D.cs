using UnityEngine;

namespace Toolbox
{
    public struct Steering2D
    {
        public Vector2 force;
        public bool isMoving;

        /// <summary>
        /// A steering with no force that is not considered moving so the rigidbody receives stopping drag.
        /// </summary>
        public static readonly Steering2D Stop = new Steering2D
        {
            force = Vector2.zero,
            isMoving = false
        };

        /// <summary>
        /// A steering with no force, but is still considered moving so the rigidbody receives moving drag.
        /// </summary>
        public static readonly Steering2D None = new Steering2D
        {
            force = Vector2.zero,
            isMoving = true
        };

        public override bool Equals(object obj)
        {
            return obj is Steering2D && this == (Steering2D)obj;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + force.GetHashCode();
            hash = (hash * 7) + isMoving.GetHashCode();
            return hash;
        }

        public static bool operator ==(Steering2D left, Steering2D right)
        {
            return left.force == right.force && left.isMoving == right.isMoving;
        }

        public static bool operator !=(Steering2D left, Steering2D right)
        {
            return left.force != right.force || left.isMoving != right.isMoving;
        }

        public static Steering2D operator +(Steering2D left, Steering2D right)
        {
            return new Steering2D
            {
                force = left.force + right.force,
                isMoving = left.isMoving || right.isMoving
            };
        }
    }
}