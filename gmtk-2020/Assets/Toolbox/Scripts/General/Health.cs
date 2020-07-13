using UnityEngine;

namespace Toolbox
{
    public class Health : MonoBehaviour
    {
        public float currentHealth = 100;

        public float maxHealth = 100;

        /// <summary>
        /// How much time before you can take damage again after just taking damage.
        /// </summary>
        public float invincibleDuration;

        public float PercentHealth
        {
            get
            {
                return currentHealth / maxHealth;
            }
        }

        public AudioClip hurtClip;
        public float hurtVolume = 1f;

        Cooldown cooldown;

        public virtual void Start()
        {
            cooldown = new Cooldown(invincibleDuration);
        }

        public virtual bool ApplyDamage(float damage)
        {
            if (currentHealth <= 0 || (damage > 0 && !cooldown.CanUse()))
            {
                return false;
            }

            currentHealth -= damage;

            if (hurtClip != null && damage > 0)
            {
                AudioSource.PlayClipAtPoint(hurtClip, Camera.main.transform.position, hurtVolume);
            }

            if (currentHealth <= 0)
            {
                OutOfHealth();
            }

            return true;
        }

        public virtual void OutOfHealth()
        {
            Destroy(gameObject);
        }

        public virtual bool CanTakeDamage()
        {
            return currentHealth > 0 && cooldown.CanUseRaw();
        }
    }
}