using System.Collections;
using UnityEngine;

namespace Toolbox
{
    public class Health2D : Health
    {
        public SpriteRenderer[] models;
        public Color hurtTint = Color.red;
        public float hurtFlashDuration = 0.1f;

        Color[] originalTints;

        public override void Start()
        {
            base.Start();

            originalTints = new Color[models.Length];

            for (int i = 0; i < models.Length; i++)
            {
                originalTints[i] = models[i].color;
            }
        }

        public override bool ApplyDamage(float damage)
        {
            bool result = base.ApplyDamage(damage);

            if (damage > 0 && result)
            {
                for (int i = 0; i < models.Length; i++)
                {
                    models[i].color = hurtTint;
                }

                StartCoroutine(TurnOffTint());
            }

            return result;
        }

        IEnumerator TurnOffTint()
        {
            yield return new WaitForSeconds(hurtFlashDuration);

            for (int i = 0; i < models.Length; i++)
            {
                models[i].color = originalTints[i];
            }
        }
    }
}