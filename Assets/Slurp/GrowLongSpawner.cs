using System.Collections;
using UnityEngine;

public class GrowLongSpawner : MonoBehaviour
{
    public GrowLongParticle prefab;
    public int[] spawnCount;
    public float spawnXOffset;
    public float[] delayStart;
    public float[] width;
    public float[] maxLength;
    public float growSpeed = 1f;
    public float[] maxDist;
    public Color[] colors;
    public float waitTime = 1f;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int count = Random.Range(spawnCount[0], spawnCount[1]);

        for (int i = 0; i < count; i++)
        {
            float ml = Random.Range(maxLength[0], maxLength[1]);
            float md = Random.Range(maxDist[0], maxDist[1]);
            float startY = Random.value * 0.75f * ((maxLength[1] - maxDist[1]) - (ml + md));

            Vector3 pos = transform.position;
            pos.x += Random.Range(-spawnXOffset, spawnXOffset);
            pos.y -= startY;

            GrowLongParticle glp = Instantiate(prefab, pos, Quaternion.identity);

            Vector3 scale = glp.transform.localScale;
            scale.x = Random.Range(width[0], width[1]);
            scale.y = scale.x;
            glp.transform.localScale = scale;

            glp.delayStart = Random.Range(delayStart[0], delayStart[1]);
            glp.maxLength = ml;
            glp.growSpeed = growSpeed;
            glp.maxDist = md;

            glp.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
        }

        yield return new WaitForSeconds(waitTime);

        StartCoroutine(Spawn());
    }
}
