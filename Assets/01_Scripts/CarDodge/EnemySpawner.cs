using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo
    public Transform[] spawnPoints; // Array de puntos de spawn
    public float spawnInterval = 1.5f; // Intervalo entre cada spawn

    void Start()
    {
        // Iniciar la generación de enemigos repetidamente
        InvokeRepeating("SpawnEnemy", 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Escoger un punto de spawn aleatorio
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Generar el enemigo
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
