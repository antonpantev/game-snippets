using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foo : MonoBehaviour
{
    public GameObject prefab;
    public Color[] colors;
    public float width = 1f;
    public float delay = 0.2f;
    public float globalDelay = 1f;

    void Start()
    {
        for (int i = 0; i < colors.Length; i++)
        {
            GameObject go = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            EllipseMesh em = go.GetComponent<EllipseMesh>();
            em.innerRadius = i * width;
            em.outerRadius = ((i + 1) * width) + (width * 0.02f);
            em.color = colors[i];
            em.delay = globalDelay + (i * delay);
        }
    }
}
