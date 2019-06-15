using DG.Tweening;
using UnityEngine;

/*
 * Shrink the health bar
 * Have a copy of the health bar behind it and shrink it on a delay
 * Temporarily change the bar color to white
 * Temporarily scale the bar's size
 */
public class JuicyHealthBar : MonoBehaviour
{
    public float currentHealth = 100f;
    public float maxHealth = 100f;

    [Header("Top Bar")]
    public Transform topBar;
    public float topShrinkTime = 0.075f;

    [Header("Bottom Bar")]
    public Transform bottomBar;
    public float bottomShrinkTime = 0.15f;
    public float waitTime = 0.2f;

    float maxWidth;
    Tween bottomBarTween = null;

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
            
            UpdateBar();
        }
    }

    void UpdateBar()
    {
        if (bottomBarTween != null)
        {
            bottomBarTween.Kill();
        }

        float target = (currentHealth / maxHealth) * maxWidth;
        topBar.DOScaleX(target, topShrinkTime).SetEase(Ease.InOutSine);
        bottomBarTween = bottomBar.DOScaleX(target, bottomShrinkTime).SetEase(Ease.InOutSine).SetDelay(waitTime);
    }
}
