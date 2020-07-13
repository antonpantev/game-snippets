using UnityEngine;

namespace Toolbox
{
    /* Be sure to set gravity to be faster than real gravity (something like -30f)
     * in the Physics settings.
     * 
     * Make sure the capsule collider has enough height that it isn't a circle or
     * ground check will overlap with the ground and it could ignore the ground. */
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Movement3D))]
    public class Player3D : MonoBehaviour
    {
        public string horAxisName = "Horizontal";
        public string vertAxisName = "Vertical";
        public string jumpButtonName = "Jump";
        public Transform playerCamera;
        public float jumpAccel = 500f;

        [Header("Ground Check")]
        public LayerMask whatIsGround = Physics.DefaultRaycastLayers;
        public float groundCheckDist = 0.2f;

        CapsuleCollider capsule;
        float capsuleRadius;
        float touchingGroundDist;
        Rigidbody rb;
        Movement3D movement;
        float horAxis;
        float vertAxis;
        bool shouldJump;

        void Start()
        {
            if (playerCamera == null)
            {
                playerCamera = Camera.main.transform;
            }

            capsule = GetComponent<CapsuleCollider>();
            capsuleRadius = Mathf.Max(transform.localScale.x, transform.localScale.z) * capsule.radius;
            touchingGroundDist = (transform.localScale.y * capsule.height / 2f) - capsuleRadius + Physics.defaultContactOffset;

            rb = GetComponent<Rigidbody>();
            movement = GetComponent<Movement3D>();
        }

        void Update()
        {
            horAxis = Input.GetAxisRaw(horAxisName);
            vertAxis = Input.GetAxisRaw(vertAxisName);

            if (Input.GetButtonDown(jumpButtonName))
            {
                shouldJump = true;
            }
        }

        int numUpdatesSinceJump;

        void FixedUpdate()
        {
            float distToGround = GetDistToGround();

            Vector3 right = Vector3.ProjectOnPlane(playerCamera.right, Vector3.up).normalized;
            Vector3 forward = Vector3.ProjectOnPlane(playerCamera.forward, Vector3.up).normalized;
            Vector3 dir = (right * horAxis + forward * vertAxis).normalized;

            Vector3 accel = movement.accel * dir;

            if (distToGround <= (touchingGroundDist + groundCheckDist) && shouldJump)
            {
                accel.y = jumpAccel;
                numUpdatesSinceJump = 0;
            }

            movement.steering.force = rb.mass * accel;

            /* Depending on the order of execution of the Player3D and Movement3D scripts
             * there could be two updates after jumping before the character moves so make
             * sure they don't feel drag until they are in the air. */
            movement.steering.isMoving = dir != Vector3.zero || distToGround > touchingGroundDist || numUpdatesSinceJump < 2;

            shouldJump = false;
            numUpdatesSinceJump++;
        }

        float GetDistToGround()
        {
            float distToGround = Mathf.Infinity;

            Vector3 origin = transform.TransformPoint(capsule.center);
            float maxDist = touchingGroundDist + groundCheckDist;

            RaycastHit[] hits = Utils.SphereCastAll(origin, capsuleRadius, Vector3.down, maxDist, whatIsGround);

            foreach (RaycastHit hit in hits)
            {
                /* For colliders that overlap the sphere at the start of the sweep the hit
                 * distance is 0, but the ground will never overlap the sphere at the center
                 * of the capsule (unless the capsule is circle shaped). */
                if (hit.distance > 0 && hit.distance < distToGround)
                {
                    distToGround = hit.distance;
                }
            }

            return distToGround;
        }
    }
}