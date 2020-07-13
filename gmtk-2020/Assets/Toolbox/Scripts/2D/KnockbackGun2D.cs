using Toolbox;
using UnityEngine;

namespace Toolbox
{
    public class KnockbackGun2D : MonoBehaviour
    {
        public KnockbackBullet2D prefab;
        public float fireTime = 0.4f;
        public float damage = 1f;
        public float speed = 9f;
        public float maxDist = 6f;
        public float knockbackForce = 70f;
        public float knockbackDuration = 0.1f;

        [HideInInspector]
        public Cooldown cooldown;

        public virtual void Start()
        {
            cooldown = new Cooldown(fireTime, true);
        }

        /// <summary>
        /// Tries to fire a bullet in the given direction if the gun is ready to fire again.
        /// </summary>
        public virtual void Fire(Vector2 dir)
        {
            if (cooldown.CanUse())
            {
                SpawnBullet(dir);
            }
        }

        public virtual void SpawnBullet(Vector2 dir)
        {
            KnockbackBullet2D b = Instantiate(prefab, transform.position, Quaternion.identity);
            b.damage = damage;
            b.velocity = dir.normalized * speed;
            b.maxDist = maxDist;
            b.knockbackForce = knockbackForce;
            b.knockbackDuration = knockbackDuration;
        }
    }
}