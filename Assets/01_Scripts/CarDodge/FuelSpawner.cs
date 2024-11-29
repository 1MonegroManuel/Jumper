using UnityEngine;

public class FuelSpawner : MonoBehaviour
{
    public GameObject fuelPrefab; // Prefab del combustible
    public Transform[] spawnPoints; // Array de puntos de spawn
    public float spawnInterval = 1.5f; // Intervalo entre cada spawn

    void Start()
    {
        // Iniciar la generación de combustible repetidamente
        InvokeRepeating("SpawnFuel", 1f, spawnInterval);
    }

    void SpawnFuel()
    {
        // Escoger un punto de spawn aleatorio
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Generar el combustible
        Instantiate(fuelPrefab, spawnPoint.position, Quaternion.identity);
    }
}
