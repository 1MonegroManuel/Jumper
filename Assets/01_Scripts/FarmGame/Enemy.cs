using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float life = 5f;           // Vida del enemigo
    public float speed = 2f;          // Velocidad de movimiento del enemigo
    private Transform player;         // Transform del jugador
    private Rigidbody2D rb;           // Componente Rigidbody2D para controlar el movimiento
    public float damageToPlayer = 3f; // Da�o que el enemigo causa al jugador
    private Spawn spawnController;    // Controlador de Spawn para informar destrucci�n

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player != null)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
        rb.rotation = 0;
    }

    // M�todo para detectar colisiones con triggers (jugador)
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player playerScript = collision.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(damageToPlayer);
            }
            Destroy(gameObject);  // Destruye al enemigo tras hacer da�o
        }
    }

    // M�todo que se llama cuando el enemigo recibe da�o
    public void TakeDamage(float dmg)
    {
        life -= dmg;
        if (life <= 0)
        {
            DestroyEnemy();  // Destruye al enemigo si su vida llega a 0
        }
    }

    // M�todo para destruir al enemigo e informar al Spawn
    void DestroyEnemy()
    {
        if (spawnController != null)
        {
            spawnController.EnemyDestroyed();  // Informa que el enemigo fue destruido
        }
        Destroy(gameObject);  // Destruye el enemigo
    }

    // Asigna el controlador de Spawn a este enemigo
    public void SetSpawnController(Spawn spawn)
    {
        spawnController = spawn;
    }
}
