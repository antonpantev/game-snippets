using UnityEngine;

namespace Toolbox
{
    public class FirstPersonCamera : MonoBehaviour
    {
        [Tooltip("X = Change in mouse position.\nY = Multiplicative factor for camera rotation.")]
        public AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

        [Tooltip("Whether or not to invert our Y axis for mouse input to rotation.")]
        public bool invertY = false;

        CameraState m_TargetCameraState = new CameraState();
        CameraState m_InterpolatingCameraState = new CameraState();

        int skipFrames = 3;

        void OnEnable()
        {
            m_TargetCameraState.SetFromTransform(transform);
            m_InterpolatingCameraState.SetFromTransform(transform);

            HideCursor();
        }

        void LateUpdate()
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

                    m_TargetCameraState.yaw += mouseMovement.x * mouseSensitivityFactor;
                    m_TargetCameraState.pitch += mouseMovement.y * mouseSensitivityFactor;
                }
            }

            float t = Utils.GetLerpPercent(0.99f, 0.01f, Time.deltaTime);
            m_InterpolatingCameraState.LerpTowards(m_TargetCameraState, t);

            m_InterpolatingCameraState.UpdateTransform(transform);
        }

        public void HideCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            skipFrames = 3;
        }

        class CameraState
        {
            public float yaw;
            public float pitch;
            public float roll;

            public void SetFromTransform(Transform t)
            {
                pitch = t.eulerAngles.x;
                yaw = t.eulerAngles.y;
                roll = t.eulerAngles.z;
            }

            public void LerpTowards(CameraState target, float t)
            {
                yaw = Mathf.Lerp(yaw, target.yaw, t);
                pitch = Mathf.Lerp(pitch, target.pitch, t);
                roll = Mathf.Lerp(roll, target.roll, t);
            }

            public void UpdateTransform(Transform t)
            {
                t.eulerAngles = new Vector3(pitch, yaw, roll);
            }
        }
    }
}