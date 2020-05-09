using UnityEngine;

public class Tangent : MonoBehaviour
{
    public float speed = 1f;

    MeshRenderer mr;
    float t = 0;
    float startTime;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();

        startTime = Time.time + 1f;
    }

    void Update()
    {
        if (Time.time > startTime)
        {
            mr.enabled = true;

            Vector3 scale = transform.localScale;
            scale.y = Mathf.SmoothStep(0, 1, t);
            transform.localScale = scale;

            if (t < 1)
            {
                t += speed * Time.deltaTime;
            }
            else
            {
                startTime = Time.time + 3f;
                t = 0;
            }
        }
    }
}
