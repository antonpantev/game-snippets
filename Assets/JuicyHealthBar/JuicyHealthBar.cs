using UnityEngine;

public class JuicyHealthBar : MonoBehaviour
{
    public Transform bottomBar;
    public Transform topBar;
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    public float bottomBarSpeed = 1f;
    public float bottomBarWaitTime = 0.2f;

    float bottomBarStartTime = Mathf.Infinity;
    float maxWidth;

    void Start()
    {
        maxWidth = topBar.localScale.x;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            currentHealth -= Random.Range(0.05f, 0.1f) * maxHealth;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            bottomBarStartTime = Time.time + bottomBarWaitTime;
        }

        UpdateBar();
    }

    void UpdateBar()
    {
        float target = (currentHealth / maxHealth) * maxWidth;

        Vector3 scale = topBar.localScale;
        scale.x = target;
        topBar.localScale = scale;

        if (bottomBarStartTime < Time.time)
        {
            scale = bottomBar.localScale;
            scale.x = Mathf.MoveTowards(scale.x, target, Time.deltaTime * bottomBarSpeed);
            bottomBar.localScale = scale;
        }
    }
}
