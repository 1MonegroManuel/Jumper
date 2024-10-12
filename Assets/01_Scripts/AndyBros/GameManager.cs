using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // Singleton para acceder fácilmente desde otros scripts
    public int totalCoins = 0;  // Total de monedas recolectadas
    public Text coinText;  // Referencia al texto UI que mostrará las monedas

    void Awake()
    {
        // Configurar el singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para agregar monedas
    public void AddCoins(int amount)
    {
        totalCoins += amount;
        coinText.text = "Coins: " + totalCoins;
    }
}
