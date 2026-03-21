using UnityEngine;

public class BarrelInteract : MonoBehaviour
{
    public enum BarrelState
    {
        Standing,
        Lying,
        Rolling
    }

    public BarrelState currentState = BarrelState.Standing;

    public float interactDistance = 3f;
    public float rollForce = 12f;
    public float pushForce = 8f;
    public float rollTime = 2f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Lock barrel so player cannot push it around while standing
        LockBarrelStanding();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist > interactDistance) return;

        if (currentState == BarrelState.Standing)
        {
            MakeLieDown();
        }
        else if (currentState == BarrelState.Lying)
        {
            RollBarrel(player);
        }
    }

    void LockBarrelStanding()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    void UnlockBarrelForPhysics()
    {
        rb.constraints = RigidbodyConstraints.None;
    }

    void MakeLieDown()
    {
        currentState = BarrelState.Lying;

        UnlockBarrelForPhysics();

        transform.Rotate(Vector3.right, 90f, Space.World);
    }

    void RollBarrel(GameObject player)
    {
        PlayerController input = player.GetComponent<PlayerController>();
        if (input == null) return;

        Vector3 dir = input.GetMoveDirection();
        if (dir.magnitude < 0.1f) return;

        currentState = BarrelState.Rolling;

        rb.AddForce(dir.normalized * rollForce, ForceMode.Impulse);

        Invoke(nameof(ResetState), rollTime);
    }

    void ResetState()
    {
        currentState = BarrelState.Lying;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (currentState != BarrelState.Rolling) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                Vector3 pushDir = transform.forward;
                playerRb.AddForce(pushDir * pushForce, ForceMode.Impulse);
            }
        }
    }
}