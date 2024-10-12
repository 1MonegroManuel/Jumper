using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Importa SceneManager

public class StartGame : MonoBehaviour
{
    public string sceneName = "Game"; // Define el nombre de la escena

    // Esta funci�n se asigna al bot�n
    public void LoadGameScene()
    {
        SceneManager.LoadScene(sceneName); // Carga la escena definida
    }
}
