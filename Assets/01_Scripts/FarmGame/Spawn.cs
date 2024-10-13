using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemyPrefab;    // Prefab del enemigo
    public Transform spawnPoint;      // Punto central de generación
    public float spawnRadius = 15f;   // Radio de generación
    public int numberOfEnemies = 30;  // Número total de enemigos a generar
    public float spawnInterval = 2f;  // Intervalo de generación en segundos
    public Transform Portalpoint;     // Posición donde aparecerá el portal
    private float timer;              // Temporizador para controlar el intervalo
    private int enemiesSpawned = 0;   // Cantidad de enemigos generados
    private int enemiesAlive = 0;     // Cantidad de enemigos vivos
    public GameObject Porta;          // Prefab del portal
    private bool portalInstantiated = false;  // Controla si el portal ya fue instanciado

    void Update()
    {
        // Generar enemigos hasta alcanzar el número total especificado
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

        // Instanciar el portal si todos los enemigos han sido eliminados
        if (enemiesSpawned == numberOfEnemies && enemiesAlive == 0 && !portalInstantiated)
        {
            Debug.Log("Instanciando portal...");
            Instantiate(Porta, Portalpoint.position, Portalpoint.rotation);
            portalInstantiated = true;  // Asegura que el portal solo se instancie una vez
        }
    }

    void SpawnEnemy()
    {
        // Calcula una posición aleatoria dentro del radio de generación
        Vector2 spawnPosition = (Vector2)spawnPoint.position + Random.insideUnitCircle * spawnRadius;

        // Instancia el enemigo y aumenta el contador de enemigos vivos
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemiesAlive++;  // Incrementa el número de enemigos vivos

        // Informar al enemigo cuál es el `Spawn` para reducir el contador al morir
        newEnemy.GetComponent<Enemy>().SetSpawnController(this);
    }

    // Método que será llamado por los enemigos cuando mueran
    public void EnemyDestroyed()
    {
        enemiesAlive--;  // Decrementa el número de enemigos vivos
        Debug.Log("Enemigo destruido. Enemigos vivos: " + enemiesAlive);
    }
}
