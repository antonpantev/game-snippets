using UnityEngine;

public class ExistentialSpawner : MonoBehaviour
{
    public Existential[] prefabs;
    public int minCount;
    public int maxCount;
    public float size;

    public Vector2 delayStart;
    public Vector2 waitTime;
    public Vector2 deathTime;

    void Start()
    {
        int count = Random.Range(minCount, maxCount);

        for (int i = 0; i < count; i++)
        {
            Existential prefab = prefabs[Random.Range(0, prefabs.Length)];
            Vector3 pos = Random.insideUnitSphere * size;
            Existential e = Instantiate(prefab, pos, Random.rotation);
            e.delayStart = Random.Range(delayStart.x, delayStart.y);
            e.waitTime = Random.Range(waitTime.x, waitTime.y);
            e.deathTime = Random.Range(deathTime.x, deathTime.y);
        }
    }
}
