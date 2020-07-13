using System.Collections.Generic;
using UnityEngine;

namespace Toolbox
{
    public static class Extensions
    {
        public static bool Approximately(this Vector3 a, Vector3 b, float epsilon)
        {
            return Vector3.Distance(a, b) < epsilon;
        }

        public static void ClampDirection(this Vector3Int v)
        {
            v.Clamp(Vector3Int.one * -1, Vector3Int.one);
        }

        public static Transform InstantiateHere(this Transform transform, Transform prefab)
        {
            return Object.Instantiate(prefab, prefab.position + transform.position, Quaternion.identity);
        }

        public static T PickRandom<T>(this IList<T> list)
        {
            T result = default(T);

            if (list.Count > 0)
            {
                result = list[Random.Range(0, list.Count)];
            }

            return result;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Checks if the layer is in the mask
        /// </summary>
        public static bool Contains(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }

        /// <summary>
        /// Creates a BoundsInt that contains both BoundsInts.
        /// </summary>
        public static BoundsInt Union(this BoundsInt a, BoundsInt b)
        {
            Vector3Int min = Vector3Int.Min(a.min, b.min);
            Vector3Int max = Vector3Int.Max(a.max, b.max);

            return new BoundsInt(min, max - min);
        }

        /// <summary>
        /// Get the size of the bounds (including the max of the bounds).
        /// </summary>
        public static Vector3Int SizeInclusive(this BoundsInt bounds)
        {
            return bounds.size + new Vector3Int(1, 1, 1);
        }

        /// <summary>
        /// Checks if the point is in the bounds (including the max of the bounds).
        /// </summary>
        /// <returns><c>true</c>, if inclusive was containsed, <c>false</c> otherwise.</returns>
        public static bool ContainsInclusive(this BoundsInt bounds, Vector3Int pos)
        {
            if (pos.x == bounds.xMax)
            {
                pos.x -= 1;
            }

            if (pos.y == bounds.yMax)
            {
                pos.y -= 1;
            }

            if (pos.z == bounds.zMax)
            {
                pos.z -= 1;
            }

            return bounds.Contains(pos);
        }

        /// <summary>
        /// Grows the bounds (including the max of the bounds) to contain the point if it did not already.
        /// </summary>
        public static void GrowInclusive(this BoundsInt bounds, Vector3Int pos)
        {
            if (pos.x < bounds.xMin)
            {
                bounds.xMin = pos.x;
            }

            if (pos.y < bounds.yMin)
            {
                bounds.yMin = pos.y;
            }

            if (pos.z < bounds.zMin)
            {
                bounds.zMin = pos.z;
            }

            if (pos.x > bounds.xMax)
            {
                bounds.xMax = pos.x;
            }

            if (pos.y > bounds.yMax)
            {
                bounds.yMax = pos.y;
            }

            if (pos.z > bounds.zMax)
            {
                bounds.zMax = pos.z;
            }
        }

        /// <summary>
        /// Grows the bounds (including the max of the bounds) to contain the point if it did not already.
        /// </summary>
        public static void GrowInclusive(this BoundsInt bounds, Vector2Int pos)
        {
            bounds.GrowInclusive(new Vector3Int(pos.x, pos.y, 0));
        }

        public static BoundsInt Clone(this BoundsInt bounds)
        {
            return new BoundsInt(bounds.position, bounds.size);
        }

        public static bool IsCurrentState(this Animator animator, int layerIndex, string name)
        {
            return animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(name);
        }

        public static bool IsCurrentState(this Animator animator, string name)
        {
            return animator.IsCurrentState(0, name);
        }
    }
}