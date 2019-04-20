using UnityEngine;
using UnityEngine.UI;

public class GrowingBar : MonoBehaviour
{
    public float width;
    public float height;
    public Color color;

    void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(width, height);

        Image image = GetComponent<Image>();
        image.color = color;
    }
}
