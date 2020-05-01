using DG.Tweening;
using UnityEngine;

public class JuicyHealthBarStep5 : MonoBehaviour
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

    SpriteRenderer topBarSr;
    float maxWidth;
    Tween bottomBarTween = null;

    void Start()
    {
        topBarSr = topBar.GetComponent<SpriteRenderer>();
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
        if (bottomBarTween != null)
        {
            bottomBarTween.Kill();
        }

        float t = currentHealth / maxHealth;
        float width = t * maxWidth;

        topBar.DOScaleX(width, topShrinkTime);
        bottomBarTween = bottomBar.DOScaleX(width, bottomShrinkTime).SetDelay(waitTime);
        transform.DOScale(barScale, barScaleTime).SetLoops(2, LoopType.Yoyo);

        topBarSr.DOColor(Color.white, topShrinkTime).SetLoops(2, LoopType.Yoyo);
    }
}
