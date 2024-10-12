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

    // Objeto vac�o que contiene los sprites del personaje
    public Transform bodyTransform; // Asigna aqu� el Empty que contiene los sprites del cuerpo

    private bool isFacingRight = true; // Controla hacia d�nde est� mirando el personaje

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Detectar si el jugador est� tocando el suelo usando un raycast hacia abajo
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));

        // Detectar colisiones con paredes usando un raycast hacia la direcci�n del movimiento
        isTouchingWall = Physics2D.Raycast(transform.position, Vector2.right * Mathf.Sign(moveInput), 0.6f, wallLayer);

        // Si el jugador est� tocando una pared y no est� en el suelo, resbala hacia abajo
        if (isTouchingWall && !isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, -2f);
        }

        // Girar al jugador cuando cambie de direcci�n
        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }

        // Detectar colisi�n con el Goal usando un raycast hacia adelante
        RaycastHit2D hitGoal = Physics2D.Raycast(transform.position, Vector2.right * Mathf.Sign(moveInput), 0.6f, goalLayer);

        // Si se detecta el Goal, cambiar a la WinScene
        if (hitGoal.collider != null)
        {
            SceneManager.LoadScene("WinScene");
        }

        // Saltar si el player est� en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    // Detectar colisiones f�sicas con enemigos
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Si el jugador est� cayendo sobre el enemigo, lo destruye
            if (rb.velocity.y < 0)
            {
                Destroy(collision.gameObject);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce / 2); // Rebote del jugador (est�tica)
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
        isFacingRight = !isFacingRight; // Cambiar la direcci�n del personaje

        // Girar el cuerpo (el objeto vac�o que contiene todos los sprites)
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
