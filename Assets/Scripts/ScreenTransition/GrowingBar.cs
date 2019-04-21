using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GrowingBar : MonoBehaviour
{
    public float startPercent = 0f;
    public float time = 0.2f;
    public float width;
    public float height;
    public float waitTime;
    public Color color;

    RectTransform rt;
    Image image;
    float speed;
    float xTarget;
    float aTarget;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        speed = (1f - startPercent) / time;

        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(waitTime);

        rt.sizeDelta = new Vector2(width * startPercent, height);

        color.a = startPercent;
        image.color = color;

        xTarget = width;
        aTarget = 1f;
    }

    void Update()
    {
        Vector2 size = rt.sizeDelta;
        size.x = Mathf.MoveTowards(size.x, xTarget, width * speed * Time.deltaTime);
        rt.sizeDelta = size;

        color.a = Mathf.MoveTowards(color.a, aTarget, 1f * speed * Time.deltaTime);
        image.color = color;
    }

    public void Hide()
    {
        StartCoroutine(SetIsEnding());
    }

    IEnumerator SetIsEnding()
    {
        yield return new WaitForSeconds(waitTime);
        xTarget = 0;
        aTarget = 0;
    }
}
