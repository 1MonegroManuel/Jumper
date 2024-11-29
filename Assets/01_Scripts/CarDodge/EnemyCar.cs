using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    public float speed = 5f;

    private GameUIControllerNew levelUI;

    void Start()
    {
        levelUI = FindObjectOfType<GameUIControllerNew>(); // Encontrar el controlador de la UI
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -10f)
        {
            // Incrementar el puntaje al esquivar el auto
            if (levelUI != null)
            {
                levelUI.AddScore(10); // Sumar puntos (por ejemplo, 10 puntos por auto esquivado)
            }
            Destroy(gameObject); // Elimina el auto cuando sale de la pantalla
        }
    }
}
