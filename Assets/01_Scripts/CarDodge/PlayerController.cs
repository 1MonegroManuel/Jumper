using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Velocidad del jugador
    public Vector2 movementLimits; // Límites de movimiento

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
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameManager.ReduceHealth(30); // Lógica global
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Coin"))
        {
            GameManager.AddCoin(); // Lógica global
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Fuel"))
        {
            levelUI.UpdateFuel(35); // Incrementar combustible en el nivel actual
            Destroy(collision.gameObject);
        }
    }
}
