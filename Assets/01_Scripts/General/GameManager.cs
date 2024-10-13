using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class GameManager
{
    public static int PlayerHealth { get; set; }
    public static int Coins { get; set; }
    public static List<string> Levels { get; private set; }

    private static bool isPaused = false; // Nueva variable para controlar la pausa
    private static Text coinText;
    private static Text healthText;
    private static Text pauseText; // Texto para mostrar el estado de pausa

    static GameManager()
    {
        InitializeGame();
        SceneManager.sceneLoaded += OnSceneLoaded; // Registrar evento para cada carga de escena
    }

    public static void InitializeGame()
    {
        PlayerHealth = 100;
        Coins = 0;
        Levels = new List<string> { "GalaxyShooter", "Farm", "Game", "CarScene" };
    }

    public static void InitializeUI()
    {
        CreateCoinTextUI();
        CreateHealthTextUI();
        CreatePauseTextUI(); // Crear el texto de pausa
        UpdateCoinText();
        UpdateHealthText();
        UpdatePauseText(); // Actualizar el texto de pausa
    }

    public static void AddItem(string item)
    {
        Levels.Add(item);
    }

    public static void ShowPlayerInfo()
    {
        Debug.Log("Player Health: " + PlayerHealth);
        Debug.Log("Coins: " + Coins);
        Debug.Log("Player Items: " + string.Join(", ", Levels));
    }

    public static void CreateCoinTextUI()
    {
        Canvas canvas = GameObject.FindObjectOfType<Canvas>() ?? CreateCanvas();

        if (coinText == null)
        {
            GameObject textObj = new GameObject("CoinText");
            textObj.transform.SetParent(canvas.transform);

            coinText = textObj.AddComponent<Text>();
            coinText.font = Resources.Load<Font>("MyCustomFont");
            coinText.fontSize = 36;
            coinText.color = Color.yellow;
            coinText.alignment = TextAnchor.MiddleRight;

            RectTransform rectTransform = coinText.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(200, 50);
            rectTransform.anchorMin = rectTransform.anchorMax = rectTransform.pivot = new Vector2(1, 1);
            rectTransform.anchoredPosition = new Vector2(-20, -80);
        }
    }

    public static void CreateHealthTextUI()
    {
        Canvas canvas = GameObject.FindObjectOfType<Canvas>() ?? CreateCanvas();

        if (healthText == null)
        {
            GameObject textObj = new GameObject("HealthText");
            textObj.transform.SetParent(canvas.transform);

            healthText = textObj.AddComponent<Text>();
            healthText.font = Resources.Load<Font>("MyCustomFont");
            healthText.fontSize = 36;
            healthText.color = Color.red;
            healthText.alignment = TextAnchor.MiddleRight;

            RectTransform rectTransform = healthText.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(200, 50);
            rectTransform.anchorMin = rectTransform.anchorMax = rectTransform.pivot = new Vector2(1, 1);
            rectTransform.anchoredPosition = new Vector2(-20, -20);
        }
    }

    public static void CreatePauseTextUI()
    {
        Canvas canvas = GameObject.FindObjectOfType<Canvas>() ?? CreateCanvas();

        if (pauseText == null)
        {
            GameObject textObj = new GameObject("PauseText");
            textObj.transform.SetParent(canvas.transform);

            pauseText = textObj.AddComponent<Text>();
            pauseText.font = Resources.Load<Font>("MyCustomFont");
            pauseText.fontSize = 36;
            pauseText.color = Color.white;
            pauseText.alignment = TextAnchor.MiddleCenter;

            RectTransform rectTransform = pauseText.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(300, 50);
            rectTransform.anchorMin = rectTransform.anchorMax = rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = new Vector2(0, 0); // Centrado en la pantalla
        }
    }

    public static void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + Coins;
        }
    }

    public static void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + PlayerHealth;
        }
    }

    public static void UpdatePauseText()
    {
        if (pauseText != null)
        {
            pauseText.text = isPaused ? "Game Paused" : "";
        }
    }

    private static Canvas CreateCanvas()
    {
        GameObject canvasObj = new GameObject("GameCanvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();
        return canvas;
    }

    public static void ReduceHealth(int damage)
    {
        if (!isPaused)
        {
            PlayerHealth -= damage;
            UpdateHealthText();

            if (PlayerHealth <= 0)
            {
                RestartGame();
            }
        }
    }

    public static void RestartGame()
    {
        PlayerHealth = 100;
        Coins = 0;
        Levels = new List<string> { "GalaxyShooter", "Farm", "Game", "CarScene" };
        SceneManager.LoadScene("Tutorial");
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeUI();
    }

    public static void TogglePause()
    {
        isPaused = !isPaused; // Cambia el estado de pausa
        UpdatePauseText(); // Actualiza el texto de pausa
        Time.timeScale = isPaused ? 0 : 1; // Pausa o reanuda el tiempo del juego
    }

    public static void Portal()
    {
        if (!isPaused)
        {
            System.Random rd = new System.Random();
            int randomIndex = rd.Next(Levels.Count);
            string selectedLevel = Levels[randomIndex];
            Levels.RemoveAt(randomIndex);
            SceneManager.LoadScene(selectedLevel);
        }
    }
}
