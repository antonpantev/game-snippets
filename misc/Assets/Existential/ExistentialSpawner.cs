using UnityEngine;

public class ExistentialSpawner : MonoBehaviour
{
    public Existential[] prefabs;
    public int minCount;
    public int maxCount;
    public float size;
    public float tooCloseToCamera;

    public Vector2 delayStart;
    public Vector2 waitTime;
    public Vector2 deathTime;

    void Start()
    {
        InvokeRepeating("Spawn", 0, delayStart.y + waitTime.y + deathTime.y);
    }

    void Spawn()
    {
        int count = Random.Range(minCount, maxCount);

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = Random.insideUnitSphere * size;
            
            if (Vector3.Distance(pos, Camera.main.transform.position) > tooCloseToCamera)
            {
                Existential prefab = prefabs[Random.Range(0, prefabs.Length)];
                Existential e = Instantiate(prefab, pos, Random.rotation);
                e.delayStart = Random.Range(delayStart.x, delayStart.y);
                e.waitTime = Random.Range(waitTime.x, waitTime.y);
                e.deathTime = Random.Range(deathTime.x, deathTime.y);
            }
        }
    }
}
