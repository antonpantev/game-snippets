using System.Collections;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public float ringSize = 2f;
    public float tangentSpeed = 1f;
    public float tangentLength = 1f;
    public int maxCount = 10;
    public float delayTime = 0.25f;
    public Tangent prefab;

    int count = 0;
    float deltaAngle;

    void Start()
    {
        deltaAngle = (360f / maxCount) * Mathf.Deg2Rad;

        StartCoroutine(SpawnTangent());
    }

    IEnumerator SpawnTangent()
    {
        // float angle = Random.Range(0f, 2 * Mathf.PI);
        float angle = count * deltaAngle;
        angle = (0.0f * Mathf.PI) + (2f * Mathf.PI) - angle;

        float x = Mathf.Cos(angle) * ringSize;
        float z = Mathf.Sin(angle) * ringSize;

        Tangent t = Instantiate(prefab, new Vector3(x, z, 0f), Quaternion.identity);
        t.angle = angle * Mathf.Rad2Deg;
        t.speed = tangentSpeed;
        t.length = tangentLength;

        if (count < maxCount)
        {
            count++;
            yield return new WaitForSeconds(delayTime);
            StartCoroutine(SpawnTangent());
        }
    }
}
