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
    public Transform bodyTransform;

    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Detectar si el jugador est� tocando el suelo
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));

        // Detectar colisiones con paredes
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

        // Saltar si el player est� en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    // M�todo que mata al jugador
    public void Die()
    {
        // Aqu� puedes a�adir efectos de muerte o sonidos si es necesario
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reiniciar la escena
    }

    // Girar el personaje
    void Flip()
    {
        isFacingRight = !isFacingRight;

        // Girar el cuerpo (el objeto vac�o que contiene todos los sprites)
        Vector3 scale = bodyTransform.localScale;
        scale.x *= -1;
        bodyTransform.localScale = scale;
    }
}
