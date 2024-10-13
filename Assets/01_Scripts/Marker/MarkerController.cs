using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Para cambiar de escena

public class MarkerController : MonoBehaviour
{

    // Funci�n para "Cura Grande"
    public void HealLarge()
    {
        if (GameManager.Coins >= 20)
        {
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
            GameManager.Coins -= 10;
            GameManager.PlayerHealth += 12;
            GameManager.InitializeUI();
        }
    }

    public void HealShort()
    {
        if (GameManager.Coins >= 5)
        {
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
