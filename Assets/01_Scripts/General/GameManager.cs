using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class GameManager
{
    public static int PlayerHealth { get; set; }
    public static int Coins { get; set; }
    public static List<string> Levels { get; private set; }

    private static bool isPaused = false;
    private static Text coinText;
    private static Text healthText;
    private static Text pauseText;
    private static Text continueText;

    static GameManager()
    {
        InitializeGame();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public static void InitializeGame()
    {
        PlayerHealth = 100;
        Coins = 0;
        Levels = new List<string> { "GalaxyShooter", "Farm", "Game", "CarScene", "DateGame" };
    }

    public static void InitializeUI()
    {
        CreateCoinTextUI();
        CreateHealthTextUI();
        CreatePauseTextUI();
        UpdateCoinText();
        UpdateHealthText();
        UpdatePauseText();
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

            AddShadowAndOutline(coinText);

            RectTransform rectTransform = coinText.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(300, 60);
            rectTransform.anchorMin = rectTransform.anchorMax = rectTransform.pivot = new Vector2(1, 1);
            rectTransform.anchoredPosition = new Vector2(-30, -90);
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

            AddShadowAndOutline(healthText);

            RectTransform rectTransform = healthText.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(300, 60);
            rectTransform.anchorMin = rectTransform.anchorMax = rectTransform.pivot = new Vector2(1, 1);
            rectTransform.anchoredPosition = new Vector2(-30, -30);
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
            pauseText.fontSize = 48;
            pauseText.color = Color.white;
            pauseText.alignment = TextAnchor.MiddleCenter;

            AddShadowAndOutline(pauseText);

            RectTransform rectTransform = pauseText.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(400, 80);
            rectTransform.anchorMin = rectTransform.anchorMax = rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = new Vector2(0, 0);
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

    private static void AddShadowAndOutline(Text text)
    {
        Shadow shadow = text.gameObject.AddComponent<Shadow>();
        shadow.effectColor = new Color(0, 0, 0, 0.5f);
        shadow.effectDistance = new Vector2(2, -2);

        Outline outline = text.gameObject.AddComponent<Outline>();
        outline.effectColor = Color.black;
        outline.effectDistance = new Vector2(2, -2);
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
        Levels = new List<string> { "GalaxyShooter", "Farm", "Game", "CarScene", "DateGame" };
        SceneManager.LoadScene("Tutorial");
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeUI();
    }

    public static void TogglePause()
    {
        isPaused = !isPaused;
        UpdatePauseText();
        Time.timeScale = isPaused ? 0 : 1;
    }

    public static void Portal()
    {
        try
        {
            if (!isPaused)
            {
                if (Levels.Count == 0)
                {
                    ShowContinueCanvas();
                }
                else
                {
                    System.Random rd = new System.Random();
                    int randomIndex = rd.Next(Levels.Count);
                    string selectedLevel = Levels[randomIndex];
                    Levels.RemoveAt(randomIndex);
                    SceneManager.LoadScene(selectedLevel);
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
            ShowContinueCanvas();
        }
    }

    public static void ShowContinueCanvas()
    {
        Canvas canvas = GameObject.FindObjectOfType<Canvas>() ?? CreateCanvas();

        if (continueText == null)
        {
            // Crear un panel de fondo para el canvas y cambiar su color a plomo
            GameObject panelObj = new GameObject("BackgroundPanel");
            panelObj.transform.SetParent(canvas.transform, false);

            Image panelImage = panelObj.AddComponent<Image>();
            panelImage.color = new Color(0.5f, 0.5f, 0.5f, 1f); // Color plomo (gris)

            RectTransform panelRect = panelObj.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0, 0);
            panelRect.anchorMax = new Vector2(1, 1);
            panelRect.offsetMin = Vector2.zero;  // Sin offsets, cubre toda la pantalla
            panelRect.offsetMax = Vector2.zero;

            // Crear el texto
            GameObject textObj = new GameObject("ContinueText");
            textObj.transform.SetParent(panelObj.transform, false);

            continueText = textObj.AddComponent<Text>();
            continueText.font = Resources.Load<Font>("MyCustomFont");
            continueText.fontSize = 48;
            continueText.color = Color.white;
            continueText.alignment = TextAnchor.MiddleCenter;

            AddShadowAndOutline(continueText);

            RectTransform textRect = continueText.GetComponent<RectTransform>();
            textRect.sizeDelta = new Vector2(400, 80);
            textRect.anchorMin = textRect.anchorMax = textRect.pivot = new Vector2(0.5f, 0.5f);
            textRect.anchoredPosition = new Vector2(0, 0);

            continueText.text = "Continuaraaa...";
        }
    }

}
