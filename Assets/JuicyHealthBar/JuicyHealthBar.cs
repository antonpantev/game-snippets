using DG.Tweening;
using UnityEngine;

/*
 * Shrink the health bar
 * Have a copy of the health bar behind it and shrink it on a delay
 * CHANGE COLOR!!
 * Temporarily change the bar color to white
 * Temporarily scale the bar's size
 * Outline to show full bar size even on low health
 */
public class JuicyHealthBar : MonoBehaviour
{
    public float currentHealth = 100f;
    public float maxHealth = 100f;

    [Header("Bar")]
    public float barScale = 1.25f;
    public float barScaleTime = 0.035f;

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
        topBar.DOScaleX(target, topShrinkTime);
        bottomBarTween = bottomBar.DOScaleX(target, bottomShrinkTime).SetDelay(waitTime);
        transform.DOScale(barScale, barScaleTime).SetLoops(2, LoopType.Yoyo);
    }
}
