using UnityEngine;
using System.Collections.Generic;

public class CrowdAudioEmitter : MonoBehaviour
{
    public string workerTag = "Worker";
    public float refreshRate = 0.5f; // how often we rescan for workers
    public float followSpeed = 8f;

    private List<Transform> workers = new List<Transform>();
    private float refreshTimer;

    void Update()
    {
        // Periodically refresh worker list
        refreshTimer += Time.deltaTime;
        if (refreshTimer >= refreshRate)
        {
            refreshTimer = 0f;
            FindWorkers();
        }

        UpdateCenterPosition();
    }

    void FindWorkers()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(workerTag);

        workers.Clear();

        foreach (GameObject obj in objs)
        {
            workers.Add(obj.transform);
        }
    }

    void UpdateCenterPosition()
    {
        if (workers.Count == 0) return;

        Vector3 center = Vector3.zero;
        int count = 0;

        foreach (Transform worker in workers)
        {
            if (worker == null) continue;

            center += worker.position;
            count++;
        }

        if (count == 0) return;

        center /= count;

        transform.position = Vector3.Lerp(
            transform.position,
            center,
            Time.deltaTime * followSpeed
        );
    }
}