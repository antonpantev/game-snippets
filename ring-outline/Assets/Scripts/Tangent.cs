using UnityEngine;

public class Tangent : MonoBehaviour
{
    public float speed = 1f;
    public float angle = 0f;
    public float length = 1f;
    public bool loop = false;

    MeshRenderer mr;
    float t = 0;
    float startTime;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();

        startTime = Time.time + 1f;

        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.z = angle;
        transform.eulerAngles = eulerAngles;
    }

    void Update()
    {
        if (Time.time > startTime)
        {
            mr.enabled = true;

            Vector3 scale = transform.localScale;
            scale.y = Mathf.SmoothStep(0, 1, t) * length;
            transform.localScale = scale;

            if (t < 1)
            {
                t += speed * Time.deltaTime;
            }
            else if (loop)
            {
                startTime = Time.time + 3f;
                t = 0;
            }
        }
    }
}
