using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUIControllerNew : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Slider fuelSlider;
    public Slider progressBar;

    private float fuel = 100f;
    private float levelProgress = 0f;
    public float levelDuration = 100f;
    private int score = 0;

    private EnemySpawner enemySpawner;
    private int difficultyStage = 0;

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        UpdateUI();
    }

    void Update()
    {
        // Combustible y progreso
        fuel -= (100f / 30f) * Time.deltaTime;
        fuel = Mathf.Clamp(fuel, 0, 100);
        levelProgress += Time.deltaTime;
        levelProgress = Mathf.Clamp(levelProgress, 0, levelDuration);
        UpdateUI();

        if (fuel <= 0f)
        {
            Debug.Log("Game Over: Sin combustible");
            GameManager.RestartGame();
        }

        if (levelProgress >= levelDuration)
        {
            Debug.Log("Nivel completado");
            GameManager.Portal();
        }

        HandleDifficultyIncrease();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (fuelSlider != null)
            fuelSlider.value = fuel;

        if (progressBar != null)
            progressBar.value = (levelProgress / levelDuration) * 100f;
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    public void UpdateFuel(float amount)
    {
        fuel = Mathf.Clamp(fuel + amount, 0, 100);
    }

    private void HandleDifficultyIncrease()
    {
        float progressPercentage = levelProgress / levelDuration;

        if (difficultyStage == 0 && progressPercentage >= 0.25f)
        {
            IncreaseDifficulty(0.3f); // Primer aumento
            difficultyStage = 1;
        }
        else if (difficultyStage == 1 && progressPercentage >= 0.5f)
        {
            IncreaseDifficulty(0.4f); // Segundo aumento
            difficultyStage = 2;
        }
        else if (difficultyStage == 2 && progressPercentage >= 0.75f)
        {
            IncreaseDifficulty(0.5f); // Tercer aumento
            difficultyStage = 3;
        }
    }

    private void IncreaseDifficulty(float speedIncrease)
    {
        if (enemySpawner != null)
        {
            enemySpawner.IncreaseDifficulty();
        }
        EnemyCar.IncreaseSpeedMultiplier(speedIncrease);
        Debug.Log($"Dificultad aumentada. Incremento de velocidad: {speedIncrease}");
    }
}
