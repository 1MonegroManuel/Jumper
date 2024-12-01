using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    public float speed = 8f; // Velocidad inicial aumentada para mayor desafío
    private static float speedMultiplier = 1f; // Multiplicador de velocidad global
    private GameUIControllerNew levelUI;

    void Start()
    {
        levelUI = FindObjectOfType<GameUIControllerNew>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * speedMultiplier * Time.deltaTime);

        if (transform.position.y < -10f)
        {
            if (levelUI != null)
            {
                levelUI.AddScore(10);
            }
            Destroy(gameObject);
        }
    }

    public static void IncreaseSpeedMultiplier(float amount)
    {
        speedMultiplier += amount; // Incremento dinámico de velocidad
    }
}
