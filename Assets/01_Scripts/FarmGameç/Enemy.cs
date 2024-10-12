using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float life = 5f;
    public float speed = 2f;
    private Transform player;
    public float damageToPlayer = 3f;
    private Rigidbody2D rb; 

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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Aplica daño al jugador
            Player playerScript = collision.collider.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(damageToPlayer);
            }
            Destroy(gameObject); 
        }
    }

    public void TakeDamage(float dmg)
    {
        life -= dmg;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
