using UnityEngine;

namespace Toolbox
{
    /// <summary>
    /// Add to sprites that should be facing the camera.
    /// </summary>
    public class FaceCamera : MonoBehaviour
    {
        void Start()
        {
            transform.localRotation = Camera.main.transform.localRotation;
        }
    }
}