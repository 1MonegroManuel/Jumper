using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemyPrefab;    // Prefab del enemigo
    public Transform spawnPoint;      // Punto central de generación
    public float spawnRadius = 15f;   // Radio de generación
    public int numberOfEnemies = 5;   // Número total de enemigos a generar
    public float spawnInterval = 2f;  // Intervalo de generación en segundos

    private float timer;              // Temporizador para controlar el intervalo
    private int enemiesSpawned = 0;   // Cantidad de enemigos generados

    void Update()
    {
        if (enemiesSpawned < numberOfEnemies)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                SpawnEnemy();
                timer = 0f;
                enemiesSpawned++;
            }
        }
    }
    void SpawnEnemy()
    {
        // Calcula una posición aleatoria dentro del radio de generación
        Vector2 spawnPosition = (Vector2)spawnPoint.position + Random.insideUnitCircle * spawnRadius;

        // Instancia el enemigo con rotación 0
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
