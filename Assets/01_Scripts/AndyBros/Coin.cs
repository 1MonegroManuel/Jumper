using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;  // Valor de la moneda (puede ser 1, pero puedes cambiarlo)
    public AudioClip coinPickupSound; // Sonido al recoger la moneda
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Sumar el valor de la moneda al puntaje directamente desde GameManager
            GameManager.Coins += coinValue;

            // Actualizar el texto de UI en GameManager
            GameManager.UpdateCoinText();

            // Reproducir el sonido de la moneda si el AudioSource y el AudioClip están asignados
            if (audioSource != null && coinPickupSound != null)
            {
                audioSource.PlayOneShot(coinPickupSound);
            }

            // Desactivar la moneda visualmente
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            // Destruir la moneda después de que el sonido termine de reproducirse
            Destroy(gameObject, coinPickupSound.length);
        }
    }
}
