using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Para cambiar de escena

public class MarkerController : MonoBehaviour
{
    public AudioClip coinSound;      // Clip de sonido para la moneda
    public AudioClip tipSound;       // Clip de sonido para la propina
    public AudioSource audioSource;  // Componente para reproducir el sonido, asignado desde el Inspector

    void Start()
    {
        GameManager.Coins = 100;
        // Aqu� no necesitas obtener el AudioSource desde c�digo si lo asignas manualmente en el Inspector.
    }

    // Funci�n para reproducir el sonido de la moneda
    void PlayCoinSound()
    {
        if (audioSource != null && coinSound != null)
        {
            audioSource.PlayOneShot(coinSound);
        }
        else
        {
            Debug.LogWarning("AudioSource o coinSound no est�n asignados.");
        }
    }

    // Funci�n para reproducir el sonido de la propina
    void PlayTipSound()
    {
        if (audioSource != null && tipSound != null)
        {
            audioSource.PlayOneShot(tipSound);
        }
        else
        {
            Debug.LogWarning("AudioSource o tipSound no est�n asignados.");
        }
    }

    // Funci�n para "Cura Grande"
    public void HealLarge()
    {
        if (GameManager.Coins >= 20)
        {
            PlayCoinSound(); // Reproducir el sonido de la moneda
            GameManager.Coins -= 20;
            GameManager.PlayerHealth += 25;
            GameManager.InitializeUI();
        }
    }

    // Funci�n para "Cura Mediana"
    public void HealMedium()
    {
        if (GameManager.Coins >= 10)
        {
            PlayCoinSound(); // Reproducir el sonido de la moneda
            GameManager.Coins -= 10;
            GameManager.PlayerHealth += 12;
            GameManager.InitializeUI();
        }
    }

    // Funci�n para "Cura Peque�a"
    public void HealShort()
    {
        if (GameManager.Coins >= 5)
        {
            PlayCoinSound(); // Reproducir el sonido de la moneda
            GameManager.Coins -= 5;
            GameManager.PlayerHealth += 5;
            GameManager.InitializeUI();
        }
    }

    // Funci�n para "Propina"
    public void Tip()
    {
        if (GameManager.Coins >= 5)
        {
            GameManager.Coins -= 5;
            Debug.Log("Propina aplicada");
            PlayTipSound(); // Reproducir el sonido de la propina
        }
    }

    // Funci�n para "Continuar"
    public void ContinueGame()
    {
        if (GameManager.PlayerHealth > 100)
        {
            GameManager.PlayerHealth = 100;
            GameManager.InitializeUI();
        }
        GameManager.Portal();
    }
}
