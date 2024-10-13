using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemyPrefab;    // Prefab del enemigo
    public Transform spawnPoint;      // Punto central de generaci�n
    public float spawnRadius = 15f;   // Radio de generaci�n
    public int numberOfEnemies = 30;  // N�mero total de enemigos a generar
    public float spawnInterval = 2f;  // Intervalo de generaci�n en segundos
    public Transform Portalpoint;     // Posici�n donde aparecer� el portal
    private float timer;              // Temporizador para controlar el intervalo
    private int enemiesSpawned = 0;   // Cantidad de enemigos generados
    private int enemiesAlive = 0;     // Cantidad de enemigos vivos
    public GameObject Porta;          // Prefab del portal
    private bool portalInstantiated = false;  // Controla si el portal ya fue instanciado
    public AudioClip deathSound;      // Clip de sonido para la muerte del enemigo
    private AudioSource audioSource;  // Componente para reproducir el sonido
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        // Generar enemigos hasta alcanzar el n�mero total especificado
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
    void PlayDeadSound()
    {
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }
    void SpawnEnemy()
    {
        // Calcula una posici�n aleatoria dentro del radio de generaci�n
        Vector2 spawnPosition = (Vector2)spawnPoint.position + Random.insideUnitCircle * spawnRadius;

        // Instancia el enemigo y aumenta el contador de enemigos vivos
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemiesAlive++;  // Incrementa el n�mero de enemigos vivos

        // Informar al enemigo cu�l es el `Spawn` para reducir el contador al morir
        newEnemy.GetComponent<Enemy>().SetSpawnController(this);
    }

    // M�todo que ser� llamado por los enemigos cuando mueran
    public void EnemyDestroyed()
    {
        PlayDeadSound();
        enemiesAlive--;  // Decrementa el n�mero de enemigos vivos
        Debug.Log("Enemigo destruido. Enemigos vivos: " + enemiesAlive);
    }
}
