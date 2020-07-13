using UnityEngine;

namespace Toolbox
{
    /* You will most likely want to make any bullet prefab have a sensor layer so
     * they don't collide with each other. Possible create a SensePlayer and
     * SenseEnemy layer which only collide with Default and what they are
     * sensing. */
    public class Bullet : MonoBehaviour
    {
        public float damage;
        public Vector3 velocity;
        public string hurtableTag = "";

        protected Rigidbody rb;

        public virtual void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = velocity;
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            Health h = other.GetComponent<Health>();

            if (h != null && (hurtableTag == "" || h.tag == hurtableTag))
            {
                h.ApplyDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}