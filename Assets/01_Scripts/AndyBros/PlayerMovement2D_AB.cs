using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement2D_AB : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isTouchingWall;

    public LayerMask wallLayer;
    public LayerMask enemyLayer;
    public LayerMask goalLayer;

    // Objeto vacío que contiene los sprites del personaje
    public Transform bodyTransform; // Asigna aquí el Empty que contiene los sprites del cuerpo

    private bool isFacingRight = true; // Controla hacia dónde está mirando el personaje

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Detectar si el jugador está tocando el suelo usando un raycast hacia abajo
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));

        // Detectar colisiones con paredes usando un raycast hacia la dirección del movimiento
        isTouchingWall = Physics2D.Raycast(transform.position, Vector2.right * Mathf.Sign(moveInput), 0.6f, wallLayer);

        // Si el jugador está tocando una pared y no está en el suelo, resbala hacia abajo
        if (isTouchingWall && !isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, -2f);
        }

        // Girar al jugador cuando cambie de dirección
        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }

        // Detectar colisión con el Goal usando un raycast hacia adelante
        RaycastHit2D hitGoal = Physics2D.Raycast(transform.position, Vector2.right * Mathf.Sign(moveInput), 0.6f, goalLayer);

        // Si se detecta el Goal, cambiar a la WinScene
        if (hitGoal.collider != null)
        {
            SceneManager.LoadScene("WinScene");
        }

        // Saltar si el player está en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    // Detectar colisiones físicas con enemigos
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Si el jugador está cayendo sobre el enemigo, lo destruye
            if (rb.velocity.y < 0)
            {
                Destroy(collision.gameObject);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce / 2); // Rebote del jugador (estética)
            }
            else
            {
                // Si el enemigo toca al jugador desde el lado o arriba, reinicia el juego
                RestartGame();
            }
        }
    }

    // Reiniciar el juego cargando la misma escena
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Girar el personaje
    void Flip()
    {
        isFacingRight = !isFacingRight; // Cambiar la dirección del personaje

        // Girar el cuerpo (el objeto vacío que contiene todos los sprites)
        Vector3 scale = bodyTransform.localScale;
        scale.x *= -1; // Invertir el eje X para girar el cuerpo
        bodyTransform.localScale = scale;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.54f);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * Mathf.Sign(transform.localScale.x) * 0.6f);
    }
}
