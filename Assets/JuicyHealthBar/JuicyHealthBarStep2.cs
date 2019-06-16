using DG.Tweening;
using UnityEngine;

public class JuicyHealthBarStep2 : MonoBehaviour
{
    public float currentHealth = 100f;
    public float maxHealth = 100f;

    [Header("Top Bar")]
    public Transform topBar;
    public float topShrinkTime = 0.075f;

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

        topBar.DOScaleX(width, topShrinkTime);
    }
}
