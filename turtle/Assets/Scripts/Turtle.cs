using UnityEngine;

public class Turtle : MonoBehaviour
{
    ParticleSystem ps;

    void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
    }

    public void LandParticles()
    {
        Vector3 angles = ps.transform.eulerAngles;
        angles.y = Random.Range(0f, 360f);
        ps.transform.eulerAngles = angles;

        ps.Play();
    }
}
