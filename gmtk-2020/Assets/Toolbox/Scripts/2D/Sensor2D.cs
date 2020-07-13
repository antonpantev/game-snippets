using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Toolbox
{
    public abstract class Sensor2D<T> : MonoBehaviour where T : Component
    {
        HashSet<T> targets = new HashSet<T>();

        public List<T> Targets
        {
            get
            {
                /* Remove any targets that have been destroyed */
                targets.RemoveWhere(IsNull);
                return targets.ToList();
            }
        }

        static bool IsNull(T t)
        {
            return t == null || t.Equals(null);
        }

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            T t = other.GetComponent<T>();

            if (t != null && IsValidTarget(t) && !targets.Contains(t))
            {
                targets.Add(t);
            }
        }

        public virtual bool IsValidTarget(T target)
        {
            return true;
        }

        public virtual void OnTriggerExit2D(Collider2D other)
        {
            T t = other.GetComponent<T>();

            if (t != null)
            {
                targets.Remove(t);
            }
        }
    }
}