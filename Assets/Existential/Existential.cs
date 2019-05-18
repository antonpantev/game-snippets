using UnityEngine;

public class Existential : MonoBehaviour
{
    public float delayStart = 1f;
    public float waitTime = 1f;
    public float deathTime = 1f;

    Animator animator;
    float startTime;

    void Start()
    {
        animator = GetComponent<Animator>();

        Invoke("Appear", delayStart);
        Invoke("Disappear", delayStart + waitTime);
        Invoke("Die", delayStart + waitTime + deathTime);
    }

    void Appear()
    {
        animator.SetBool("appear", true);
    }

    void Disappear()
    {
        animator.SetBool("appear", false);
        animator.SetBool("disappear", true);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
