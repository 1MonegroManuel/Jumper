using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;  // Valor de la moneda (puede ser 1, pero puedes cambiarlo)

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Sumar el valor de la moneda al puntaje
            GameManager.instance.AddCoins(coinValue);

            // Destruir la moneda al recogerla
            Destroy(gameObject);
        }
    }
}
