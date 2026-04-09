// using UnityEngine;

// public class BossGameOverTrigger : MonoBehaviour
// {
//     [SerializeField] private GameOverController gameOverController;
//     [SerializeField] private string workerTag = "Worker";
//     [SerializeField] private float gameOverDistance = 1.2f;

//     private bool gameOverTriggered = false;
//     private Transform[] workers;

//     private void Start()
//     {
//         GameObject[] workerObjects = GameObject.FindGameObjectsWithTag(workerTag);
//         workers = new Transform[workerObjects.Length];

//         for (int i = 0; i < workerObjects.Length; i++)
//         {
//             workers[i] = workerObjects[i].transform;
//         }
//     }

//     private void Update()
//     {
//         if (gameOverTriggered || gameOverController == null || workers == null)
//             return;

//         for (int i = 0; i < workers.Length; i++)
//         {
//             if (workers[i] == null)
//                 continue;

//             float distance = Vector3.Distance(transform.position, workers[i].position);

//             if (distance <= gameOverDistance)
//             {
//                 gameOverTriggered = true;
//                 gameOverController.ShowGameOver();
//                 return;
//             }
//         }
//     }
// }
using UnityEngine;

public class BossGameOverTrigger : MonoBehaviour
{
    [SerializeField] private GameOverController gameOverController;
    [SerializeField] private float gameOverDistance = 1.2f;

    private bool gameOverTriggered = false;

    private void Update()
    {
        if (gameOverTriggered || gameOverController == null)
            return;

        for (int i = 0; i < WorkerRegistry.Workers.Count; i++)
        {
            Transform worker = WorkerRegistry.Workers[i];

            if (worker == null)
                continue;

            float distance = Vector3.Distance(transform.position, worker.position);

            if (distance <= gameOverDistance)
            {
                gameOverTriggered = true;
                gameOverController.ShowGameOver();
                return;
            }
        }
    }
}