using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement_AB : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private bool movingRight = false;
    public LayerMask wallLayer;
    private bool isActive = false;
    public List<SpriteRenderer> enemyRenderers = new List<SpriteRenderer>();

    public float enemyHeight = 0.5f;  // Altura del enemigo

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Obtener todos los SpriteRenderers de este objeto o hijos
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in renderers)
        {
            enemyRenderers.Add(sr);
        }
    }

    void Update()
    {
        // Verificar si alguno de los SpriteRenderers es visible en la cámara
        foreach (SpriteRenderer renderer in enemyRenderers)
        {
            if (renderer.isVisible)
            {
                isActive = true;
                break;
            }
        }

        // Si el enemigo está activo, empieza a moverse
        if (isActive)
        {
            rb.velocity = new Vector2(moveSpeed * (movingRight ? 1 : -1), rb.velocity.y);
        }
        else
        {
            // Si el enemigo no es visible, no se mueve
            rb.velocity = Vector2.zero;
        }
    }

    // Detectar colisiones con paredes o enemigos
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Cambiar de dirección si choca con una pared
            movingRight = !movingRight;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            // Cambiar de dirección si colisiona con otro enemigo
            movingRight = !movingRight;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                // Verificar si el jugador está cayendo y está por encima del enemigo
                bool playerIsFalling = playerRb.velocity.y < 0;
                bool playerAboveEnemy = playerRb.transform.position.y > transform.position.y + enemyHeight;

                // Si el jugador está cayendo y está por encima del enemigo, destruir el enemigo
                if (playerIsFalling && playerAboveEnemy)
                {
                    Destroy(gameObject);  // Destruir al enemigo
                    playerRb.velocity = new Vector2(playerRb.velocity.x, 5f); // Rebote del jugador
                }
                else
                {
                    // Si el jugador no está cayendo desde arriba, el jugador muere
                    collision.gameObject.GetComponent<PlayerMovement2D_AB>().Die();  // Método que mata al jugador
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (movingRight ? Vector3.right : Vector3.left) * 0.5f);
    }
}
