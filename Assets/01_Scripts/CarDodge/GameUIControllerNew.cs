using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUIControllerNew : MonoBehaviour
{
    // Referencias p�blicas para los elementos de UI
    public TextMeshProUGUI scoreText;  // Texto para los puntos
    public Slider fuelSlider;          // Slider para el combustible
    public Slider progressBar;         // Slider para el progreso del nivel

    // Variables del nivel
    private float fuel = 100f;          // Combustible inicial
    private float levelProgress = 0f;  // Progreso inicial del nivel
    public float levelDuration = 150f; // Duraci�n del nivel (2.5 minutos)
    private int score = 0;             // Puntuaci�n inicial

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        // Reducir el combustible autom�ticamente
        fuel -= (100f / 30f) * Time.deltaTime; // Baja de 100 a 0 en 30 segundos
        fuel = Mathf.Clamp(fuel, 0, 100);

        // Incrementar el progreso del nivel autom�ticamente
        levelProgress += Time.deltaTime;
        levelProgress = Mathf.Clamp(levelProgress, 0, levelDuration);

        // Actualizar la interfaz gr�fica
        UpdateUI();

        // Comprobar si el combustible se ha agotado
        if (fuel <= 0f)
        {
            Debug.Log("Game Over: Sin combustible");
            GameManager.RestartGame(); // Reiniciar juego desde el GameManager
        }

        // Comprobar si el nivel ha terminado
        if (levelProgress >= levelDuration)
        {
            Debug.Log("Nivel completado");
            GameManager.Portal(); // Cambiar al siguiente nivel
        }
    }

    /// <summary>
    /// Actualiza los elementos visuales de la interfaz del juego.
    /// </summary>
    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString(); // Actualiza la puntuaci�n

        if (fuelSlider != null)
            fuelSlider.value = fuel; // Actualiza la barra de combustible

        if (progressBar != null)
            progressBar.value = (levelProgress / levelDuration) * 100f; // Actualiza el progreso del nivel
    }

    /// <summary>
    /// Incrementa la puntuaci�n del jugador.
    /// </summary>
    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    /// <summary>
    /// Ajusta el combustible del jugador.
    /// </summary>
    /// <param name="amount">Cantidad a a�adir o restar al combustible.</param>
    public void UpdateFuel(float amount)
    {
        fuel = Mathf.Clamp(fuel + amount, 0, 100); // Asegura que el combustible est� entre 0 y 100
    }
}
