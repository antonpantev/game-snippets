using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spoon : MonoBehaviour
{
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public float speed = 1f;

    LineRenderer lr;
    int lengthOfLineRenderer;

    void Start()
    {
        lr = GetComponent<LineRenderer>();

        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lr.colorGradient = gradient;
    }

    float maxAngle = (Mathf.PI * 2f);
    float lastT;

    void Update()
    {
        if (lastT < maxAngle)
        {
            float t = Mathf.Clamp(Time.time * speed, 0f, maxAngle);
            Vector3 point = new Vector3(Mathf.Cos(t), Mathf.Sin(t), 0.0f);
            lr.positionCount++;
            lr.SetPosition(lr.positionCount - 1, point);
            lastT = t;
        }
    }
}
