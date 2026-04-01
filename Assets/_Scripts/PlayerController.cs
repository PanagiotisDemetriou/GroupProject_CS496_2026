// using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     [SerializeField] private float speed = 10f;
//     [SerializeField] private Transform cameraTransform;

//     private Rigidbody rb;
//     private Animator animator;
//     private Vector3 direction;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();
//         animator = GetComponent<Animator>();
//     }
//     public Vector3 GetMoveDirection()
//     {
//         float h = Input.GetAxisRaw("Horizontal");
//         float v = Input.GetAxisRaw("Vertical");

//         return new Vector3(h, 0f, v).normalized;
//     }
//     void Update()
//     {
//         float h = Input.GetAxis("Horizontal");
//         float v = Input.GetAxis("Vertical");

//         Vector3 forward = cameraTransform.forward;
//         Vector3 right = cameraTransform.right;

//         forward.y = 0f;
//         right.y = 0f;

//         forward.Normalize();
//         right.Normalize();

//         direction = forward * v + right * h;

//         if (direction.magnitude > 1f)
//             direction.Normalize();

//         Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
//         float currentSpeed = horizontalVelocity.magnitude;
//         animator.SetFloat("Speed", currentSpeed);
//     }

//     void FixedUpdate()
//     {
//         // Vector3 desiredVelocity = direction * speed;

//         // Keep current Y velocity (gravity)
//         // rb.linearVelocity = new Vector3(desiredVelocity.x, rb.linearVelocity.y, desiredVelocity.z);
//         Vector3 velocity = rb.linearVelocity;
//         Vector3 target = direction * speed;

//         velocity.x = target.x;
//         velocity.z = target.z;

//         rb.linearVelocity = velocity;

//         if (direction != Vector3.zero) { 
//         Quaternion targetRotation = Quaternion.LookRotation(direction); 
//         // transform.rotation = Quaternion.Slerp( transform.rotation, targetRotation, 10f * Time.fixedDeltaTime );
//         rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.fixedDeltaTime));
//          }
//     }
// }
// using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     [SerializeField] private float moveSpeed = 6f;
//     [SerializeField] private float gravity = -20f;
//     [SerializeField] private float rotationSpeed = 10f;
//     [SerializeField] private Transform cameraTransform;

//     private CharacterController controller;
//     private Animator animator;
//     private Vector3 verticalVelocity;

//     void Start()
//     {
//         controller = GetComponent<CharacterController>();
//         animator = GetComponent<Animator>();
//     }

//     void Update()
//     {
//         float h = Input.GetAxisRaw("Horizontal");
//         float v = Input.GetAxisRaw("Vertical");

//         Vector3 forward = cameraTransform.forward;
//         Vector3 right = cameraTransform.right;

//         forward.y = 0f;
//         right.y = 0f;

//         forward.Normalize();
//         right.Normalize();

//         Vector3 moveDirection = (forward * v + right * h).normalized;

//         if (controller.isGrounded && verticalVelocity.y < 0f)
//         {
//             verticalVelocity.y = -2f;
//         }

//         Vector3 move = moveDirection * moveSpeed;
//         controller.Move(move * Time.deltaTime);

//         verticalVelocity.y += gravity * Time.deltaTime;
//         controller.Move(verticalVelocity * Time.deltaTime);

//         if (moveDirection.sqrMagnitude > 0.001f)
//         {
//             Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
//             transform.rotation = Quaternion.Slerp(
//                 transform.rotation,
//                 targetRotation,
//                 rotationSpeed * Time.deltaTime
//             );
//         }

//         if (animator != null)
//         {
//             Vector3 horizontalMove = new Vector3(move.x, 0f, move.z);
//             animator.SetFloat("Speed", horizontalMove.magnitude);
//         }
//     }
// }
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Transform cameraTransform;

    private CharacterController controller;
    private Animator animator;
    private Vector3 verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * v + right * h).normalized;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        if (controller.isGrounded && verticalVelocity.y < 0f)
        {
            verticalVelocity.y = -2f;
        }

        Vector3 move = moveDirection * currentSpeed;
        controller.Move(move * Time.deltaTime);

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);

        if (moveDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", moveDirection.magnitude * currentSpeed);
        }
    }
}