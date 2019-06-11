using UnityEngine;

public class JuicyHealthBar : MonoBehaviour
{
    public Transform bar;
    public float currentHealth = 100f;
    public float maxHealth = 100f;

    float maxWidth;

    void Start()
    {
        maxWidth = bar.localScale.x;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            currentHealth -= Random.Range(0.05f, 0.30f) * maxHealth;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        }

        UpdateBar();
    }

    void UpdateBar()
    {
        Vector3 scale = bar.localScale;
        scale.x = (currentHealth / maxHealth) * maxWidth;
        bar.localScale = scale;
    }
}
