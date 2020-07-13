using UnityEngine;

namespace Toolbox
{
    public struct Steering3D
    {
        public Vector3 force;
        public bool isMoving;

        /// <summary>
        /// A steering with no force that is not considered moving so the rigidbody receives stopping drag.
        /// </summary>
        public static readonly Steering3D Stop = new Steering3D
        {
            force = Vector3.zero,
            isMoving = false
        };

        /// <summary>
        /// A steering with no force, but is still considered moving so the rigidbody receives moving drag.
        /// </summary>
        public static readonly Steering3D None = new Steering3D
        {
            force = Vector3.zero,
            isMoving = true
        };

        public override bool Equals(object obj)
        {
            return obj is Steering3D && this == (Steering3D)obj;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + force.GetHashCode();
            hash = (hash * 7) + isMoving.GetHashCode();
            return hash;
        }

        public static bool operator ==(Steering3D left, Steering3D right)
        {
            return left.force == right.force && left.isMoving == right.isMoving;
        }

        public static bool operator !=(Steering3D left, Steering3D right)
        {
            return left.force != right.force || left.isMoving != right.isMoving;
        }

        public static Steering3D operator +(Steering3D left, Steering3D right)
        {
            return new Steering3D
            {
                force = left.force + right.force,
                isMoving = left.isMoving || right.isMoving
            };
        }
    }
}