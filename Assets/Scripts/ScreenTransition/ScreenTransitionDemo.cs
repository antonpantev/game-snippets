using System.Collections;
using UnityEngine;

public class ScreenTransitionDemo : MonoBehaviour
{
    public ScreenTransition screenTransition;
    public float waitTime = 1.5f;

    void Start()
    {
        StartCoroutine(ShowTransition());
        StartCoroutine(HideTransition());
    }

    IEnumerator ShowTransition()
    {
        yield return new WaitForEndOfFrame();
        screenTransition.Show();
    }

    IEnumerator HideTransition()
    {
        yield return new WaitForSeconds(waitTime);
        screenTransition.Hide();
    }
}
