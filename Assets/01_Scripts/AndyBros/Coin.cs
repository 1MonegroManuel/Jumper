using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;  // Valor de la moneda (puede ser 1, pero puedes cambiarlo)

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Sumar el valor de la moneda al puntaje directamente desde GameManager
            GameManager.Coins += coinValue;

            // Actualizar el texto de UI en GameManager
            GameManager.UpdateCoinText();

            // Destruir la moneda al recogerla
            Destroy(gameObject);
        }
    }
}
