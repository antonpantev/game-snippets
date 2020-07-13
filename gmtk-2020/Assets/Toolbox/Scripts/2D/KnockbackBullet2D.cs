using System.Linq;
using UnityEngine;

namespace Toolbox
{
    public class KnockbackBullet2D : MonoBehaviour
    {
        public float damage = 1f;
        public Vector2 velocity;
        /* A good starting place would be 40% of the screen width, so the player
         * can't shoot infinitely far. */
        public float maxDist = 6f;
        public float knockbackForce;
        public float knockbackDuration = 0.1f;
        public string[] hurtableTags;

        Rigidbody2D rb;
        Vector3 startPos;

        public virtual void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = velocity;
            startPos = transform.position;
        }

        public virtual void Update()
        {
            if (Vector3.Distance(startPos, transform.position) > maxDist)
            {
                Destroy(gameObject);
            }
        }

        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            Health h = collision.GetComponent<Health>();

            if (h != null && isHurtableTag(h.tag))
            {
                if (h.ApplyDamage(damage))
                {
                    AddKnockback(collision);
                }
            }

            Destroy(gameObject);
        }

        public virtual bool isHurtableTag(string tag)
        {
            return hurtableTags.Length == 0 || hurtableTags.Contains(tag);
        }

        public virtual void AddKnockback(Collider2D collision)
        {
            Movement2D movement = collision.GetComponent<Movement2D>();

            if (movement != null)
            {
                Vector2 force = velocity.normalized * knockbackForce;
                movement.AddKnockback(force, knockbackDuration);
            }
        }
    }
}