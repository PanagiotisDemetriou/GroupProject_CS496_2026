using UnityEngine;

public class WorkerRegister : MonoBehaviour
{
    private void OnEnable()
    {
        if (!WorkerRegistry.Workers.Contains(transform))
        {
            WorkerRegistry.Workers.Add(transform);
            // Debug.Log(name + " registered. Total workers: " + WorkerRegistry.Workers.Count);
        }
    }

    private void OnDisable()
    {
        WorkerRegistry.Workers.Remove(transform);
    }

    private void OnDestroy()
    {
        WorkerRegistry.Workers.Remove(transform);
    }
}