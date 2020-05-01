using UnityEngine;

public class DispControl : MonoBehaviour
{
    public float dispAmount;
    MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        dispAmount = Mathf.Lerp(dispAmount, 0, Time.deltaTime);
        meshRenderer.material.SetFloat("_Amount", dispAmount);

        if (Input.GetButtonDown("Jump"))
        {
            dispAmount += 1f;
        }
    }
}
