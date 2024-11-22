using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo
    public Transform[] spawnPoints; // Array de puntos de spawn
    public float spawnInterval = 2f; // Intervalo entre cada spawn

    void Start()
    {
        // Llamar al método de spawn repetidamente
        InvokeRepeating("SpawnEnemy", 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Escoger un punto de spawn aleatorio
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Instanciar el enemigo en el punto de spawn seleccionado
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
