using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTrigger_AB : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // verificar si el objeto que toca al "Goal" es el Player
        if (other.CompareTag("Player"))
        {
            //cambiar a la escena de victoria (WinScene)
            SceneManager.LoadScene("WinScene");
        }
    }
}
