using UnityEngine;

public class Ring : MonoBehaviour
{
    public float size = 2f;
    public int count = 10;
    public Transform prefab;

    void Start()
    {
        float deltaAngle = (360f / count) * Mathf.Deg2Rad;

        for (int i = 0; i < count; i++)
        {
            float x = Mathf.Cos(i * deltaAngle) * size;
            float z = Mathf.Sin(i * deltaAngle) * size;

            Instantiate(prefab, new Vector3(x, z, 0f), Quaternion.identity);
        }
    }
}
