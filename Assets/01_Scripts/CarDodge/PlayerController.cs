using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Velocidad del jugador
    public Vector2 movementLimits; // L�mites de movimiento
    public float tiltAngle = 15f; // �ngulo de inclinaci�n m�xima al girar
    public float rotationSpeed = 5f; // Velocidad de rotaci�n para suavizar el movimiento

    private GameUIControllerNew levelUI; // Controlador de la UI

    void Start()
    {
        // Encontrar el controlador de UI para el nivel
        levelUI = FindObjectOfType<GameUIControllerNew>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Movimiento del jugador
        Vector3 movement = new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;
        transform.Translate(movement);

        // Limitar movimiento
        float clampedX = Mathf.Clamp(transform.position.x, -movementLimits.x, movementLimits.x);
        float clampedY = Mathf.Clamp(transform.position.y, -movementLimits.y, movementLimits.y);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

        // Rotaci�n basada en direcci�n
        ApplyTilt(horizontal);
    }

    void ApplyTilt(float horizontalInput)
    {
        // Calcula el �ngulo de inclinaci�n basado en la direcci�n horizontal
        float targetTilt = -horizontalInput * tiltAngle; // Negativo para girar correctamente
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetTilt);

        // Suaviza la rotaci�n hacia el �ngulo deseado
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameManager.ReduceHealth(30); // L�gica global
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Coin"))
        {
            GameManager.AddCoin(); // L�gica global
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Fuel"))
        {
            levelUI.UpdateFuel(35); // Incrementar combustible en el nivel actual
            Destroy(collision.gameObject);
        }
    }
}
