using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento hacia abajo

    void Update()
    {
        // Mover el objeto hacia abajo a la velocidad especificada
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Destruir el objeto si sale de los límites de la pantalla
        if (transform.position.y < -10f) // Ajusta este valor según el tamaño de tu escena
        {
            Destroy(gameObject);
        }
    }
}
