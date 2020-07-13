using UnityEngine;

namespace Toolbox
{
    public class Spawner : MonoBehaviour
    {

        public Transform prefab;
        public float spawnRate = 2f;
        public float spawnChance = 0.5f;

        float nextSpawn;

        public virtual void Start()
        {
            nextSpawn = Time.time + spawnRate;
        }

        public virtual void Update()
        {
            if (nextSpawn < Time.time)
            {
                if (Random.value < spawnChance)
                {
                    Spawn();
                }
                nextSpawn = Time.time + spawnRate;
            }
        }

        public virtual void Spawn()
        {
            CreatePrefab();
        }

        public Transform CreatePrefab()
        {
            return Instantiate(prefab, prefab.position + transform.position, Quaternion.identity);
        }
    }
}