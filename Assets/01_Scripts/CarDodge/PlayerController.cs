using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    public Vector2 movementLimits; // Límites para el movimiento (X, Y)

    void Update()
    {
        // Obtener entrada del jugador en los ejes horizontal y vertical
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calcular el desplazamiento del jugador
        Vector3 movement = new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;

        // Aplicar el movimiento
        transform.Translate(movement);

        // Restringir el movimiento dentro de los límites definidos
        float clampedX = Mathf.Clamp(transform.position.x, -movementLimits.x, movementLimits.x);
        float clampedY = Mathf.Clamp(transform.position.y, -movementLimits.y, movementLimits.y);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
