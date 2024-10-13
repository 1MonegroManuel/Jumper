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

    public Transform bodyTransform;
    private bool isFacingRight = true;
    public AudioClip deadPlayerSound;
    public AudioClip jumpSound; // Clip de sonido para el salto
    private AudioSource audioSource; // Componente para reproducir el sonido

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>(); // Obtiene el componente AudioSource
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
        isTouchingWall = Physics2D.Raycast(transform.position, Vector2.right * Mathf.Sign(moveInput), 0.6f, wallLayer);

        if (isTouchingWall && !isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, -2f);
        }

        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
            PlayJumpSound(); // Reproducir el sonido al saltar
        }
    }

    public void Die()
    {
        if(GameManager.PlayerHealth-40 <= 0)
        {
            PlayDeadPlayerSound();
        }
        GameManager.ReduceHealth(40);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = bodyTransform.localScale;
        scale.x *= -1;
        bodyTransform.localScale = scale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            GameManager.Portal();
        }
        else if (collision.gameObject.CompareTag("dead"))
        {
            PlayDeadPlayerSound();
            GameManager.RestartGame();
        }
    }

    void PlayJumpSound()
    {
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound); // Reproduce el sonido de salto
        }
    }
    void PlayDeadPlayerSound()
    {
        if (audioSource != null && deadPlayerSound != null)
        {
            audioSource.PlayOneShot(deadPlayerSound); // Reproduce el sonido de salto
        }
    }
}
