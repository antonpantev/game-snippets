using UnityEngine;

namespace Toolbox
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Movement3D))]
    public class TopDownPlayer3D : MonoBehaviour
    {
        public string horAxisName = "Horizontal";
        public string vertAxisName = "Vertical";

        float horAxis;
        float vertAxis;
        Rigidbody rb;
        Movement3D movement;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            movement = GetComponent<Movement3D>();
        }

        void Update()
        {
            horAxis = Input.GetAxisRaw(horAxisName);
            vertAxis = Input.GetAxisRaw(vertAxisName);
        }

        void FixedUpdate()
        {
            Vector3 dir = new Vector3(horAxis, 0f, vertAxis).normalized;
            movement.steering.force = rb.mass * movement.accel * dir;
            movement.steering.isMoving = dir != Vector3.zero;
        }
    }
}