using UnityEngine;
using UnityEngine.AI;

public class MoveToBoss : MonoBehaviour
{
    [SerializeField] private Transform target; // Joe

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target == null) return;

        // Continuously update destination to player's position
        agent.SetDestination(target.position);

        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}