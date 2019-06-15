using DG.Tweening;
using UnityEngine;

public class JuicyHealthBar : MonoBehaviour
{
    public float currentHealth = 100f;
    public float maxHealth = 100f;

    public Transform topBar;

    [Header("Bottom Bar")]
    public Transform bottomBar;
    public float waitTime = 0.2f;
    public float shrinkDuration = 0.15f;

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
            bottomBarStartTime = Time.time + waitTime;
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
            bottomBar.DOScaleX(target, shrinkDuration).SetEase(Ease.InOutSine);
        }
    }
}
