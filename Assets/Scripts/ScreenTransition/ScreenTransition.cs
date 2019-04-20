using UnityEngine;

//TODO animate
//TODO rotate
public class ScreenTransition : MonoBehaviour
{
    public Transform prefab;
    public int count = 7;
    public float defaultWeight = 1f;
    public float centerWeight = 1.5f;
    public Color start;
    public Color end;

    /* Use https://learnui.design/tools/data-color-picker.html to generate a range of colors */
    public Color[] colors;

    void Start()
    {
        CreateColors();

        Canvas canvas = GetComponentInParent<Canvas>();
        Rect rect = canvas.GetComponent<RectTransform>().rect;

        float totalWeight = ((count - 1) * defaultWeight) + centerWeight;
        float defaultPercent = defaultWeight / totalWeight;
        float centerPercent = centerWeight / totalWeight;

        float totalWidth = Mathf.Sqrt(rect.width * rect.width + rect.height * rect.height);
        float defaultWidth = defaultPercent * totalWidth;
        float centerWidth = centerPercent * totalWidth;

        int middleIndex = count / 2;

        for (int i = 0; i < middleIndex; i++)
        {
            Vector3 position = Vector3.zero;
            position.x = -1 * ((0.5f * centerWidth) + (0.5f * defaultWidth) + (i * defaultWidth));
            CreateGrowingBar(position, defaultWidth, rect.height, colors[middleIndex - i - 1]);
        }

        CreateGrowingBar(Vector3.zero, centerWidth, rect.height, colors[middleIndex]);

        for (int i = 0; i < middleIndex; i++)
        {
            Vector3 position = Vector3.zero;
            position.x = (0.5f * centerWidth) + (0.5f * defaultWidth) + (i * defaultWidth);
            CreateGrowingBar(position, defaultWidth, rect.height, colors[middleIndex + i + 1]);
        }
    }

    void CreateColors()
    {
        float startH, startS, startV;
        float endH, endS, endV;
        Color.RGBToHSV(start, out startH, out startS, out startV);
        Color.RGBToHSV(end, out endH, out endS, out endV);

        colors = new Color[count];
        float delta = 1f / (count - 1f);

        for (int i = 0; i < count; i++)
        {
            float t = i * delta;
            float h = Mathf.Lerp(startH, endH, t);
            float s = Mathf.Lerp(startS, endS, t);
            float v = Mathf.Lerp(startV, endV, t);
            colors[i] = Color.HSVToRGB(h, s, v);
        }
    }

    void CreateGrowingBar(Vector3 position, float width, float height, Color color)
    {
        Transform t = Instantiate(prefab, position, Quaternion.identity);
        t.SetParent(transform, false);

        GrowingBar gb = t.GetComponent<GrowingBar>();
        gb.width = width;
        gb.height = height;
        gb.color = color;
    }
}
