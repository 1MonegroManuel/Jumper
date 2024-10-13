using UnityEngine;

public class Volteado : MonoBehaviour
{
    public string animationTriggerName = "Roar"; // Nombre del trigger de la animaci�n
    public float respawnYPosition = 0.35f;  // Coordenada Y donde debe reaparecer el auto
    public float damageAmount = 15f;  // Da�o que tomar� el jugador al voltearse

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que toc� tiene el tag "Floor"
        if (other.CompareTag("Floor"))
        {
            // Obtener la referencia del auto (auto completo es el padre de "tocar")
            GameObject playerCar = transform.root.gameObject;

            // Teletransportar el auto a la misma coordenada X, nueva Y (0.35), y rotaci�n en Z = 0
            playerCar.transform.position = new Vector3(playerCar.transform.position.x, respawnYPosition, playerCar.transform.position.z);
            playerCar.transform.rotation = Quaternion.Euler(0, 0, 0); // Restablecer la rotaci�n en Z

            // Aplicar da�o al jugador
            TakeDamage(damageAmount); // Restar 15 puntos de vida

            // Iniciar la animaci�n (si corresponde)
            Animator animator = playerCar.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger(animationTriggerName); // Disparar la animaci�n utilizando el trigger
            }
            else
            {
                Debug.LogWarning("El prefab del auto no tiene un Animator asignado.");
            }
        }
    }

    public void TakeDamage(float dmg)
    {
        GameManager.ReduceHealth((int)dmg); 
    }
}
