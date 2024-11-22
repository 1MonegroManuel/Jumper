using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -10f)
        {
            Destroy(gameObject); // Elimina el auto cuando sale de la pantalla
        }
    }
}
