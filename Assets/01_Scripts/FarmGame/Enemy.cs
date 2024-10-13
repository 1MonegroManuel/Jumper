using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float life = 5f;           // Vida del enemigo
    public float speed = 2f;          // Velocidad de movimiento del enemigo
    private Transform player;         // Transform del jugador
    public float damageToPlayer = 3f; // Da�o que el enemigo causa al jugador
    private Spawn spawnController;    // Controlador de Spawn para informar destrucci�n

    public AudioClip deathSound;      // Clip de sonido para la muerte del enemigo
    private AudioSource audioSource;  // Componente para reproducir el sonido

    void Start()
    {
        // Encontrar al jugador por su tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Verificar que exista un jugador y perseguirlo
        if (player != null)
        {
            ChasePlayer();
        }
    }

    // M�todo para que el enemigo persiga al jugador
    void ChasePlayer()
    {
        // Obtener la direcci�n hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;

        // Mover al enemigo hacia el jugador usando Translate
        transform.Translate(direction * speed * Time.deltaTime);

        // Asegurarse de que la rotaci�n del enemigo se mantenga fija
        transform.rotation = Quaternion.identity;
    }

    // M�todo para detectar colisiones con triggers o colisiones normales
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.ReduceHealth((int)damageToPlayer);
        }
    }

    // M�todo que se llama cuando el enemigo recibe da�o
    public void TakeDamage(float dmg)
    {
        life -= dmg;
        if (life <= 0)
        {
            DestroyEnemy();  // Destruir al enemigo si su vida llega a 0
        }
    }

    // M�todo para reproducir el sonido de muerte
    void PlayDeadSound()
    {
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }

    // M�todo para destruir al enemigo e informar al Spawn
    void DestroyEnemy()
    {
        PlayDeadSound();
        if (Random.Range(0f, 1f) <= 0.25f)
        {
            GameManager.Coins += 2;
            GameManager.UpdateCoinText();
        }
        if (spawnController != null)
        {
            spawnController.EnemyDestroyed();  // Informar al Spawn que el enemigo fue destruido
        }
        Destroy(gameObject);  // Destruir el objeto enemigo
    }

    // Asignar el controlador de Spawn a este enemigo
    public void SetSpawnController(Spawn spawn)
    {
        spawnController = spawn;
    }
}
