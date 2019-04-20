using UnityEngine;

public class GrowLongParticle : MonoBehaviour
{
    public float delayStart = 1f;
    public float maxLength = 1f;
    public float growSpeed = 1f;
    public float maxDist = 1f;

    float startTime;
    bool isGrowing = true;
    float distTraveled = 0;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time <= (startTime + delayStart))
        {
            return;
        }

        Vector3 scale = transform.localScale;
        Vector3 pos = transform.position;

        if (isGrowing)
        {
            float startScaleY = scale.y;

            scale.y = Mathf.MoveTowards(scale.y, maxLength, growSpeed * Time.deltaTime);
            transform.localScale = scale;

            pos.y += (scale.y - startScaleY) / 2;
            transform.position = pos;

            isGrowing = scale.y < maxLength;
        }
        else if (distTraveled < maxDist)
        {
            float dist = growSpeed * Time.deltaTime;
            float remainingDist = maxDist - distTraveled;

            if (remainingDist < dist)
            {
                dist = remainingDist;
            }

            pos.y += dist;
            transform.position = pos;

            distTraveled += dist;
        }
        else if (scale.y > 0)
        {
            float startScaleY = scale.y;

            scale.y = Mathf.MoveTowards(scale.y, 0, growSpeed * Time.deltaTime);
            transform.localScale = scale;

            pos.y += (startScaleY - scale.y) / 2;
            transform.position = pos;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
