using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Toolbox
{
    public class GrowingBar : MonoBehaviour
    {
        public float time = 0.2f;
        public float width;
        public float height;
        public float waitTime;
        public Color color;

        RectTransform rt;
        Image image;
        float speed;
        bool isEnding;
        float xTarget;
        float aTarget;

        void Start()
        {
            rt = GetComponent<RectTransform>();
            image = GetComponent<Image>();
            speed = 1f / time;

            StartCoroutine(StartAnimation());
        }

        IEnumerator StartAnimation()
        {
            yield return new WaitForSeconds(waitTime);

            rt.sizeDelta = new Vector2(0f, height);

            color.a = 0f;
            image.color = color;

            xTarget = width;
            aTarget = 1f;
        }

        void Update()
        {
            Vector2 size = rt.sizeDelta;
            size.x = Mathf.MoveTowards(size.x, xTarget, width * speed * Time.deltaTime);
            rt.sizeDelta = size;

            color.a = Mathf.MoveTowards(color.a, aTarget, 1f * speed * Time.deltaTime);
            image.color = color;

            if (isEnding && size.x == xTarget)
            {
                Destroy(gameObject);
            }
        }

        public void Hide()
        {
            StartCoroutine(SetIsEnding());
        }

        IEnumerator SetIsEnding()
        {
            yield return new WaitForSeconds(waitTime);
            isEnding = true;
            xTarget = 0;
            aTarget = 0;
        }
    }
}