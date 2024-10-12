using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemyPrefab;    // Prefab del enemigo
    public Transform spawnPoint;      // Punto central de generaci�n
    public float spawnRadius = 15f;   // Radio de generaci�n
    public int numberOfEnemies = 5;   // N�mero total de enemigos a generar
    public float spawnInterval = 2f;  // Intervalo de generaci�n en segundos

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
        // Calcula una posici�n aleatoria dentro del radio de generaci�n
        Vector2 spawnPosition = (Vector2)spawnPoint.position + Random.insideUnitCircle * spawnRadius;

        // Instancia el enemigo con rotaci�n 0
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
