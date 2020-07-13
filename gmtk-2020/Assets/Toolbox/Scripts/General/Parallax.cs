using UnityEngine;

namespace Toolbox
{
    public class Parallax : MonoBehaviour
    {
        /* Parallax of 0 means no parallax. Parallax of 0.5 means 50% parallax. */
        public float parallaxX = 0.5f;
        public float parralaxY;

        Vector3 startPos;
        Vector3 startPosCam;

        Transform target;

        void Start()
        {
            target = Camera.main.transform;
            startPos = transform.position;
            startPosCam = target.transform.position;
        }

        void LateUpdate()
        {
            Vector3 pos = transform.position;
            pos.x = ((target.position.x - startPosCam.x) * parallaxX) + startPos.x;
            pos.y = ((target.position.y - startPosCam.y) * parralaxY) + startPos.y;
            transform.position = pos;
        }
    }
}