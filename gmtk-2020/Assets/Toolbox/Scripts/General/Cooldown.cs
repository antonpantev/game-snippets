using UnityEngine;

namespace Toolbox
{
    public class Cooldown
    {
        public float duration;
        public bool autoUse;
        public float lastUse = float.NegativeInfinity;

        public Cooldown(float duration)
        {
            this.duration = duration;
            autoUse = true;
        }

        public Cooldown(float duration, bool autoUse)
        {
            this.duration = duration;
            this.autoUse = autoUse;
        }

        public bool CanUse()
        {
            if (CanUseRaw())
            {
                if (autoUse)
                {
                    Use();
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Tells you if you can use the cooldown but will never auto use the cooldown.
        /// </summary>
        public bool CanUseRaw()
        {
            return Time.time >= (lastUse + duration);
        }

        public void Use()
        {
            lastUse = Time.time;
        }

        public void Reset()
        {
            lastUse = float.NegativeInfinity;
        }

        public float RawPercent()
        {
            return (Time.time - lastUse) / duration;
        }

        public float Percent()
        {
            return Mathf.Clamp(RawPercent(), 0, 1);
        }
    }
}