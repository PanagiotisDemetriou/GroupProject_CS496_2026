using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform playerTarget;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private int maxEnemies = 50;

    private float spawnTimer = 0f;
    private int currentEnemyCount = 0;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval && currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0 || playerTarget == null)
            return;

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        int spawnIndex = Random.Range(0, spawnPoints.Length);

        GameObject enemy = Instantiate(
            enemyPrefabs[enemyIndex],
            spawnPoints[spawnIndex].position,
            spawnPoints[spawnIndex].rotation
        );

        MoveToBoss chaseScript = enemy.GetComponent<MoveToBoss>();
        if (chaseScript != null)
        {
            chaseScript.SetTarget(playerTarget);
        }

        currentEnemyCount++;
    }

    public void NotifyEnemyDestroyed()
    {
        currentEnemyCount--;
    }
}