using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Transform cameraTransform;

    private Rigidbody rb;
    private Animator animator;
    private Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        direction = forward * v + right * h;

        if (direction.magnitude > 1f)
            direction.Normalize();

        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        float currentSpeed = horizontalVelocity.magnitude;
        animator.SetFloat("Speed", currentSpeed);
    }

    void FixedUpdate()
    {
        Vector3 desiredVelocity = direction * speed;

        // Keep current Y velocity (gravity)
        rb.linearVelocity = new Vector3(desiredVelocity.x, rb.linearVelocity.y, desiredVelocity.z);


        if (direction != Vector3.zero) { Quaternion targetRotation = Quaternion.LookRotation(direction); transform.rotation = Quaternion.Slerp( transform.rotation, targetRotation, 10f * Time.fixedDeltaTime ); }
    }
}