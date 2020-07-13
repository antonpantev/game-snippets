using UnityEngine;

namespace Toolbox
{
    /// <summary>
    /// A class to keep track of Casting progress. Similar to Cooldown, but you must manually update the casting time.
    /// </summary>
    public class Cast
    {
        public float duration;
        public bool loop;
        public float time;
        public bool isPaused;

        public Cast(float duration)
        {
            this.duration = duration;
            loop = true;
        }

        public Cast(float duration, bool loop)
        {
            this.duration = duration;
            this.loop = loop;
        }

        public void Update(float dt)
        {
            if (isPaused)
            {
                return;
            }

            if (loop && time >= duration)
            {
                time = 0;
            }

            time += dt;
        }

        public bool CanUse()
        {
            return (time >= duration);
        }

        public float Percent()
        {
            return Mathf.Clamp(time / duration, 0, 1);
        }

        public void Reset()
        {
            time = 0;
        }
    }
}