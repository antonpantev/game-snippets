using UnityEngine;

public class Ring : MonoBehaviour
{
    public float ringSize = 2f;
    public float tangentLength = 1f;
    public int count = 10;
    public Tangent prefab;

    void Start()
    {
        float deltaAngle = (360f / count) * Mathf.Deg2Rad;

        for (int i = 0; i < count; i++)
        {
            float x = Mathf.Cos(i * deltaAngle) * ringSize;
            float z = Mathf.Sin(i * deltaAngle) * ringSize;

            Tangent t = Instantiate(prefab, new Vector3(x, z, 0f), Quaternion.identity);
            t.angle = i * deltaAngle * Mathf.Rad2Deg;
            t.length = tangentLength;
        }
    }
}
