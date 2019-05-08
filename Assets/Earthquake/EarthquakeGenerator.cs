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

    Vector3 start;
    List<Transform> prefabs = new List<Transform>();
    List<Renderer> renderers = new List<Renderer>();

    void Start()
    {
        start = new Vector3((-width / 2f) + 0.5f, 0f, (-height / 2f) + 0.5f);

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

        RandomQuake();
    }

    void RandomQuake()
    {
        startTime = Time.time + 0.5f;
        center.x = Random.Range(-start.x, start.x);
        center.z = Random.Range(-start.z, start.z);
    }

    float startTime;
    Vector3 center;

    void Update()
    {
        bool ready = true;

        for (int i = 0; i < prefabs.Count; i++)
        {
            Transform t = prefabs[i];

            Vector3 pos = t.position;
            pos.y = 0;

            float dx = pos.x - center.x;
            float dz = pos.z - center.z;
            float dist = Mathf.Sqrt((dx * dx) + (dz * dz)) / maxDist;

            float time = (Time.time - startTime - dist) * speed;
            time = Mathf.Clamp(time, 0, Mathf.PI * 2);

            if (time < Mathf.PI * 2)
            {
                ready = false;
            }

            pos.y += amplitude * Mathf.Sin(time) * (1 - dist);

            float colorT = colorMultiplier * pos.y / amplitude;

            if (colorT < 0)
            {
                renderers[i].material.color = Color.Lerp(startColor, lowColor, Mathf.Clamp01(-colorT));
            }
            else
            {
                renderers[i].material.color = Color.Lerp(startColor, highColor, Mathf.Clamp01(colorT));
            }

            t.position = pos;
        }

        if (ready)
        {
            RandomQuake();
        }
    }
}
