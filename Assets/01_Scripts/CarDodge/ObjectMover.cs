using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento hacia abajo

    void Update()
    {
        // Mover el objeto hacia abajo a la velocidad especificada
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Destruir el objeto si sale de los l�mites de la pantalla
        if (transform.position.y < -10f) // Ajusta este valor seg�n el tama�o de tu escena
        {
            Destroy(gameObject);
        }
    }
}
