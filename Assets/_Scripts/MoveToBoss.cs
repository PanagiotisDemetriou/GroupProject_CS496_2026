// using UnityEngine;
// using UnityEngine.AI;

// public class MoveToBoss : MonoBehaviour
// {
//     [SerializeField] private Transform target; // Joe

//     private NavMeshAgent agent;
//     private Animator animator;

//     void Start()
//     {
//         agent = GetComponent<NavMeshAgent>();
//         animator = GetComponent<Animator>();
//     }

//     void Update()
//     {
//         if (target == null) return;

//         // Continuously update destination to player's position
//         agent.SetDestination(target.position);

//         float speed = agent.velocity.magnitude;
//         animator.SetFloat("Speed", speed);
//     }

//     public void SetTarget(Transform newTarget)
//     {
//         target = newTarget;
//     }
// }
// using UnityEngine;
// using UnityEngine.AI;

// public class MoveToBoss : MonoBehaviour
// {
//     [SerializeField] private Transform target; // normal chase target, e.g. player

//     private NavMeshAgent agent;
//     private Animator animator;

//     private Transform overrideTarget;
//     private bool useOverrideTarget = false;

//     void Start()
//     {
//         agent = GetComponent<NavMeshAgent>();
//         animator = GetComponent<Animator>();
//     }

//     void Update()
//     {
//         Transform currentTarget = useOverrideTarget ? overrideTarget : target;

//         if (currentTarget != null)
//         {
//             agent.SetDestination(currentTarget.position);
//         }

//         float speed = agent.velocity.magnitude;
//         animator.SetFloat("Speed", speed);
//     }

//     public void SetTarget(Transform newTarget)
//     {
//         target = newTarget;
//     }

//     public void SetOverrideTarget(Transform newTarget)
//     {
//         overrideTarget = newTarget;
//         useOverrideTarget = true;
//     }

//     public void ClearOverrideTarget()
//     {
//         overrideTarget = null;
//         useOverrideTarget = false;
//     }
// }
using UnityEngine;
using UnityEngine.AI;

public class MoveToBoss : MonoBehaviour
{
    [SerializeField] private Transform target;

    private NavMeshAgent agent;
    private Animator animator;

    private Transform overrideTarget;
    private bool useOverrideTarget = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // If this unit spawns while the alarm is already active,
        // immediately send it to the alarm point.
        if (AlarmController.Instance != null && AlarmController.Instance.IsAlarmActive)
        {
            SetOverrideTarget(AlarmController.Instance.AlarmPoint);
        }
    }

    void Update()
    {
        Transform currentTarget = useOverrideTarget ? overrideTarget : target;

        if (currentTarget != null)
        {
            agent.SetDestination(currentTarget.position);
        }

        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetOverrideTarget(Transform newTarget)
    {
        overrideTarget = newTarget;
        useOverrideTarget = true;
    }

    public void ClearOverrideTarget()
    {
        overrideTarget = null;
        useOverrideTarget = false;
    }
}