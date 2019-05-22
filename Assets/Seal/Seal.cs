using UnityEngine;

public class Seal : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("activate");
        }
    }
}
