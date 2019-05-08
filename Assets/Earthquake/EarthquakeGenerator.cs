using System.Collections.Generic;
using UnityEngine;

public class EarthquakeGenerator : MonoBehaviour
{
    public Transform prefab;
    public int width = 100;
    public int height = 100;
    public float maxDist = 71f;
    public float speed = 1f;
    public float amplitude = 5f;
    public Color lowColor;
    public Color startColor;
    public Color highColor;
    public float colorMultiplier = 1f;

    List<Transform> prefabs = new List<Transform>();
    List<Renderer> renderers = new List<Renderer>();

    void Start()
    {
        Vector3 start = new Vector3((-width / 2f) + 0.5f, 0f, (-height / 2f) + 0.5f);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 pos = start + new Vector3(i, 0f, j);
                Transform t = Instantiate(prefab, pos, Quaternion.identity, transform);
                prefabs.Add(t);
                renderers.Add(t.GetComponent<Renderer>());
            }
        }
    }

    void Update()
    {
        // foreach (Transform t in prefabs)
        for (int i = 0; i < prefabs.Count; i++)
        {
            Transform t = prefabs[i];

            Vector3 pos = t.position;
            // float dist = Mathf.Sqrt((pos.x * pos.x) + (pos.z * pos.z)) / maxDist;
            float dist = Mathf.Sqrt((pos.x * pos.x) + (pos.z * pos.z)) / maxDist;
            // float time = Mathf.Clamp(Time.time * speed, 0, 2f * Mathf.PI);
            if (dist <= 1)
            {
                // dist = Mathf.Clamp01(dist);
                float time = Time.time * speed;
                pos.y = amplitude * Mathf.Sin(time - dist) * (1 - dist);
                t.position = pos;

                float colorT = colorMultiplier * pos.y / amplitude;

                if (colorT < 0)
                {
                    renderers[i].material.color = Color.Lerp(startColor, lowColor, Mathf.Clamp01(-colorT));
                }
                else
                {
                    renderers[i].material.color = Color.Lerp(startColor, highColor, Mathf.Clamp01(colorT));
                }
            }
        }
    }
}
