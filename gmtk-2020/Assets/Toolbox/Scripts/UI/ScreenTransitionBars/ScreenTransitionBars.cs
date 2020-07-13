using System.Collections.Generic;
using UnityEngine;

namespace Toolbox
{
    public class ScreenTransitionBars : MonoBehaviour
    {
        public Transform prefab;
        public int count = 7;
        public float defaultWeight = 1f;
        public float centerWeight = 1.5f;
        public float delayTime = 0.1f;
        public Color start;
        public Color end;

        public Color[] colors;

        Rect rect;
        List<GrowingBar> bars;

        void Start()
        {
            Canvas canvas = GetComponentInParent<Canvas>();
            rect = canvas.GetComponent<RectTransform>().rect;
        }

        public void Show()
        {
            CreateColors();

            bars = new List<GrowingBar>();

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
                CreateGrowingBar(position, defaultWidth, totalWidth, i * delayTime, colors[middleIndex - i - 1]);
            }

            CreateGrowingBar(Vector3.zero, centerWidth, totalWidth, 0f, colors[middleIndex]);

            for (int i = 0; i < middleIndex; i++)
            {
                Vector3 position = Vector3.zero;
                position.x = (0.5f * centerWidth) + (0.5f * defaultWidth) + (i * defaultWidth);
                CreateGrowingBar(position, defaultWidth, totalWidth, i * delayTime, colors[middleIndex + i + 1]);
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


        void CreateGrowingBar(Vector3 position, float width, float height, float waitTime, Color color)
        {
            Transform t = Instantiate(prefab, position, Quaternion.identity);
            t.SetParent(transform, false);

            GrowingBar gb = t.GetComponent<GrowingBar>();
            gb.width = width;
            gb.height = height;
            gb.waitTime = waitTime;
            gb.color = color;

            bars.Add(gb);
        }

        public void Hide()
        {
            foreach (GrowingBar gb in bars)
            {
                gb.Hide();
            }
        }
    }
}