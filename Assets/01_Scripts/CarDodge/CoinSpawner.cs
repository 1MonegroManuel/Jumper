using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab de la moneda
    public Transform[] spawnPoints; // Array de puntos de spawn
    public float spawnInterval = 1.5f; // Intervalo entre cada spawn

    void Start()
    {
        // Iniciar la generación de monedas repetidamente
        InvokeRepeating("SpawnCoin", 1f, spawnInterval);
    }

    void SpawnCoin()
    {
        // Escoger un punto de spawn aleatorio
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Generar la moneda
        Instantiate(coinPrefab, spawnPoint.position, Quaternion.identity);
    }
}
