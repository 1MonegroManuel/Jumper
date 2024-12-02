using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    public float speed = 8f; // Velocidad inicial
    private static float speedMultiplier = 1f; // Multiplicador de velocidad global
    private GameUIControllerNew levelUI;

    [Header("Sprites")]
    public Sprite[] carSprites; // Array de sprites para los autos enemigos
    private SpriteRenderer spriteRenderer; // Componente SpriteRenderer del objeto

    void Start()
    {
        levelUI = FindObjectOfType<GameUIControllerNew>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Asignar un sprite aleatorio
        if (carSprites != null && carSprites.Length > 0)
        {
            int randomIndex = Random.Range(0, carSprites.Length);
            spriteRenderer.sprite = carSprites[randomIndex];
        }
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
