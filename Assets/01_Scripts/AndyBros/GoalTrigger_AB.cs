using UnityEngine.SceneManagement;
using UnityEngine;

public class GoalTrigger_AB : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // SceneManager.LoadScene("SampleScene"); 
            Destroy(other.gameObject);
        }
    }
}
