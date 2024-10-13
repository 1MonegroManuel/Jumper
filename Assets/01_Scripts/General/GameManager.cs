using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class GameManager
{
    public static int PlayerHealth { get; set; }
    public static int Coins { get; set; }
    public static List<string> Levels { get; private set; }

    private static Text coinText;
    private static Text healthText;

    static GameManager()
    {
        InitializeGame();
        SceneManager.sceneLoaded += OnSceneLoaded; // Registrar evento para cada carga de escena
    }

    // Método para inicializar el estado del juego (variables)
    public static void InitializeGame()
    {
        PlayerHealth = 100; // Vida inicial
        Coins = 0;
        Levels = new List<string> { "GalaxyShooter", "Farm", "Game", "CarScene" };

    }

    // Método para crear la UI al cargar la escena
    public static void InitializeUI()
    {
        CreateCoinTextUI();
        CreateHealthTextUI();
        UpdateCoinText();
        UpdateHealthText();
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

    private static Canvas CreateCanvas()
    {
        GameObject canvasObj = new GameObject("GameCanvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();
        return canvas;
    }

    // Reduce la vida del jugador y actualiza la UI. Si llega a 0, reinicia la escena.
    public static void ReduceHealth(int damage)
    {
        PlayerHealth -= damage;
        UpdateHealthText();

        if (PlayerHealth <= 0)
        {
            RestartGame();
        }
    }

    // Reinicia el nivel cuando la vida del jugador llega a 0.
    public static void RestartGame()
    {
        PlayerHealth = 100;
        Coins = 0;
        Levels = new List<string> { "GalaxyShooter", "Farm", "Game", "CarScene"};
        SceneManager.LoadScene("Tutorial");
    }

    // Llamado cuando la escena se carga para inicializar nuevamente la UI
    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //InitializeGame(); // Re-inicializar variables de juego
        InitializeUI();   // Crear y actualizar la UI
    }
    public static void Portal()
    {
        // Inicializamos el generador de números aleatorios
        System.Random rd = new System.Random();

        // Obtenemos un índice aleatorio basado en la cantidad de niveles en la lista
        int randomIndex = rd.Next(Levels.Count);

        // Seleccionamos el nivel de la lista usando el índice aleatorio
        string selectedLevel = Levels[randomIndex];

        // Eliminamos el nivel seleccionado de la lista para que no se repita
        Levels.RemoveAt(randomIndex);

        // Cargamos la escena correspondiente al nivel seleccionado
        SceneManager.LoadScene(selectedLevel);
    }
}
