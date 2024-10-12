using UnityEngine;

public class CameraController2D_AB : MonoBehaviour
{
    public Transform player; 
    public float offset = 0.5f;  
    public float leftLimit = 0f;

    void Update()
    {
        //calcular la nueva posición de la cámara
        Vector3 newPosition = new Vector3(player.position.x - offset, transform.position.y, transform.position.z);

        // No permitir que la cámara retroceda más allá del límite izquierdo (0)
        if (newPosition.x < leftLimit)
        {
            newPosition.x = leftLimit;
        }

        // Actualizar la posición de la camara
        transform.position = newPosition;
    }
}
