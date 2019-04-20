using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GrowingBar : MonoBehaviour
{
    public float startPercent = 0.44f;
    public float time = 0.5f;
    public float width;
    public float height;
    public float waitTime;
    public Color color;


    RectTransform rt;
    Image image;
    float speed;
    bool isStarted;

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

        isStarted = true;
    }

    void Update()
    {
        if (isStarted)
        {
            Vector2 size = rt.sizeDelta;
            size.x = Mathf.MoveTowards(size.x, width, width * speed * Time.deltaTime);
            rt.sizeDelta = size;

            color.a = Mathf.MoveTowards(color.a, 1f, 1f * speed * Time.deltaTime);
            image.color = color;
        }
    }
}
