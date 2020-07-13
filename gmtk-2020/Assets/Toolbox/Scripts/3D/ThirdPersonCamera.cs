using UnityEngine;

namespace Toolbox
{
    //TODO Turn character model if given one
    //TODO Start raycasting from the target + offset to the desired camera position and move the camera closer if it hits anything
    //TODO add zoom in / out for camera
    public class ThirdPersonCamera : MonoBehaviour
    {
        public float distance = 5f;
        public float minY = 0f;
        public float maxY = 89.9f;

        [Tooltip("X = Change in mouse position.\nY = Multiplicative factor for camera rotation.")]
        public AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

        [Tooltip("Whether or not to invert our Y axis for mouse input to rotation.")]
        public bool invertY = false;

        public Transform target;

        /// <summary>
        /// Target Offset is added to the the target position for the final
        /// position the camera follows.
        /// </summary>
        /// <remarks>
        /// This is not the camera's offset from the target, that is determined
        /// by the the distance and rotation around the target.
        /// </remarks>
        public Vector3 targetOffset = new Vector3(0f, 1f, 0f);

        float currentX;
        float currentY;
        float targetX;
        float targetY;

        int skipFrames = 3;

        void Start()
        {
            if (target == null)
            {
                Debug.Log("ThirdPersonCamera has no target.");
            }

            HideCursor();
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                HideCursor();
            }

            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (invertY ? 1 : -1));

                if (skipFrames > 0 && mouseMovement.magnitude > 0)
                {
                    skipFrames--;
                }
                else
                {
                    float mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

                    targetX += mouseMovement.x * mouseSensitivityFactor;
                    targetY += mouseMovement.y * mouseSensitivityFactor;

                    targetY = Mathf.Clamp(targetY, minY, maxY);
                }
            }
        }

        public void HideCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            skipFrames = 3;
        }

        void LateUpdate()
        {
            if (target != null)
            {
                float t = Utils.GetLerpPercent(0.99f, 0.01f, Time.deltaTime);
                currentX = Mathf.Lerp(currentX, targetX, t);
                currentY = Mathf.Lerp(currentY, targetY, t);

                Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
                Vector3 dir = Vector3.back * distance;
                transform.position = target.position + targetOffset + (rotation * dir);
                transform.LookAt(target.position + targetOffset);
            }
        }

        public void SetRotationAroundTarget(float x, float y)
        {
            currentX = x;
            targetX = x;

            y = Mathf.Clamp(y, minY, maxY);

            currentY = y;
            targetY = y;
        }
    }
}