using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private GameObject collectiblePrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float refreshInterval = 5f;

    [Header("Pickup Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private float pickupVolume = 0.8f;

    private float timer;

    void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0f; // 2D
        }
    }

    void Start()
    {
        RefreshCollectibles();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= refreshInterval)
        {
            timer = 0f;
            RefreshCollectibles();
        }
    }

    void RefreshCollectibles()
    {
        DestroyExistingCollectibles();
        SpawnAtEverySpawnPoint();
    }

    void DestroyExistingCollectibles()
    {
        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectible");

        foreach (GameObject collectible in collectibles)
        {
            Destroy(collectible);
        }
    }

    void SpawnAtEverySpawnPoint()
    {
        if (collectiblePrefab == null || spawnPoints == null)
            return;

        foreach (Transform point in spawnPoints)
        {
            if (point == null) continue;

            Instantiate(collectiblePrefab, point.position, point.rotation);
        }
    }

    public void PlayPickupSound()
    {
        if (audioSource != null && pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound, pickupVolume);
        }
    }
}