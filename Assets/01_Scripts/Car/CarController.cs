using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public float speed = 10f;  // Velocidad de movimiento
    private Rigidbody2D rb;
    private Dictionary<string, Vector2> controlMappings;
    private string forwardKey;
    private string backwardKey;
    private float controlChangeInterval = 5f;  // Intervalo para cambiar los controles
    private float timer = 0f;

    // Sonidos
    public AudioClip controlChangeSound;  // Sonido para cuando cambian las teclas
    public AudioClip turnSound;  // Sonido para cuando el coche da la vuelta
    private AudioSource audioSource;  // Componente de AudioSource

    private bool movingForward = true;  // Estado para saber si está avanzando

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();  // Obtener el componente AudioSource
        AssignRandomControls();  // Asignar controles al inicio
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Cambiar controles cada 5 segundos
        if (timer >= controlChangeInterval)
        {
            AssignRandomControls();
            timer = 0f;  // Reiniciar el temporizador
        }

        // Control del movimiento según las teclas actuales
        if (Input.GetKey(forwardKey))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);  // Mover hacia adelante


        }
        else if (Input.GetKey(backwardKey))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);  // Mover hacia atrás

        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);  // No hay movimiento
        }
    }

    // Asignar aleatoriamente teclas de movimiento hacia adelante y hacia atrás
    void AssignRandomControls()
    {
        // Todas las posibles teclas de movimiento
        List<string> availableKeys = new List<string>() { "w", "a", "s", "d" };

        // Asignar la tecla para moverse hacia adelante de forma aleatoria
        int forwardIndex = Random.Range(0, availableKeys.Count);
        forwardKey = availableKeys[forwardIndex];
        availableKeys.RemoveAt(forwardIndex);  // Remover la tecla seleccionada para no repetirla

        // Asignar la tecla para moverse hacia atrás de forma aleatoria
        int backwardIndex = Random.Range(0, availableKeys.Count);
        backwardKey = availableKeys[backwardIndex];

        // Reproducir sonido de cambio de control
        PlayControlChangeSound();

        // Mostrar las teclas asignadas en la consola para debug
        Debug.Log("Forward key: " + forwardKey + ", Backward key: " + backwardKey);
    }

    // Reproducir sonido cuando cambian los controles
    void PlayControlChangeSound()
    {
        if (audioSource != null && controlChangeSound != null)
        {
            audioSource.PlayOneShot(controlChangeSound);
        }
    }

    // Reproducir sonido cuando el coche da la vuelta (cambia de dirección)
    void PlayTurnSound()
    {
        if (audioSource != null && turnSound != null)
        {
            audioSource.PlayOneShot(turnSound);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            SceneManager.LoadScene("MiniMarket");
        }
        else if (collision.gameObject.CompareTag("dead"))
        {
            GameManager.RestartGame();
        }
    }
}
