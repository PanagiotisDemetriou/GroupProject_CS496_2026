using UnityEngine;

public class PalletInteract : MonoBehaviour
{
    [Header("Spawned object")]
    public GameObject boxPrefab;

    [Header("Spawn settings")]
    public Transform spawnPoint;
    
    [Header("Interaction")]
    public KeyCode interactKey = KeyCode.E;

    private bool playerInRange = false;
    private bool used = false;

    private void Update()
    {
        if (playerInRange && !used && Input.GetKeyDown(interactKey))
        {
            ReplaceWithBox();
        }
    }

    private void ReplaceWithBox()
    {
        used = true;

        Vector3 spawnPosition = spawnPoint != null ? spawnPoint.position : transform.position;
        Quaternion spawnRotation = spawnPoint != null ? spawnPoint.rotation : transform.rotation;

        Instantiate(boxPrefab, spawnPosition, spawnRotation);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            
        }
    }
    public void SetPlayerInRange(bool inRange)
    {
        playerInRange = inRange;
    }
}