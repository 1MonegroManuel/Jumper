using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSceneLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Asegurarse de que el VideoPlayer est� asignado
        videoPlayer = GetComponent<VideoPlayer>();

        // Iniciar la corutina que cambiar� de escena despu�s de 33 segundos
        StartCoroutine(LoadSceneAfterDelay(33f));
    }

    System.Collections.IEnumerator LoadSceneAfterDelay(float delay)
    {
        // Esperar la cantidad de segundos especificada
        yield return new WaitForSeconds(delay);

        // Cargar la siguiente escena
        SceneManager.LoadScene("Menu");
    }
}
