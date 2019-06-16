using UnityEngine;

public class JuicyHealthBarStep1 : MonoBehaviour
{
    public float currentHealth = 100f;
    public float maxHealth = 100f;

    public Transform topBar;

    float maxWidth;

    void Start()
    {
        maxWidth = topBar.localScale.x;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            currentHealth -= Random.Range(0.1f, 0.2f) * maxHealth;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

            UpdateBar();
        }
    }

    void UpdateBar()
    {
        float t = currentHealth / maxHealth;
        float width = t * maxWidth;

        Vector3 scale = topBar.localScale;
        scale.x = width;
        topBar.localScale = scale;
    }
}
