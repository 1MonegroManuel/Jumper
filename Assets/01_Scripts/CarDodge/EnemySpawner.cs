using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo
    public Transform[] spawnPoints; // Array de puntos de spawn
    public float initialSpawnInterval = 1.0f; // Intervalo inicial reducido para mayor concurrencia
    private float currentSpawnInterval; // Intervalo din�mico entre spawns
    private float difficultyMultiplier = 0.85f; // Factor de reducci�n del intervalo (mayor impacto en el aumento)

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        InvokeRepeating("SpawnEnemy", 1f, currentSpawnInterval);
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void IncreaseDifficulty()
    {
        // Reduce el intervalo de spawn din�micamente
        currentSpawnInterval = Mathf.Max(0.4f, currentSpawnInterval * difficultyMultiplier); // M�nimo ajustado a 0.4s
        CancelInvoke("SpawnEnemy");
        InvokeRepeating("SpawnEnemy", 0f, currentSpawnInterval);
    }
}
