using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerGalaxyShooter : MonoBehaviour
{
    public float speed = 2f;
    public float ExtraSpeed = 0f;
    public float timeBtwShoot = 1.5f;
    public float extraDamage = 0f;
    public float damage = 5f;
    public bool critic = false;
    public float bulletSpeed = 10f;
    public float extraBulletSpeed = 0f;
    float timer = 0;
    float timer2 = 0;
    public bool shield;
    bool canShoot = true;
    public Rigidbody2D rb;
    public Transform firePoint;
    public BulletGalaxyShooter bulletPrefab;

    // Variables para los sonidos
    public AudioClip shootSound;     // Sonido de disparo
    public AudioClip takeDamageSound; // Sonido de recibir daño
    private AudioSource audioSource;  // Componente de AudioSource

    void Start()
    {
        Debug.Log("Inició el juego");
        audioSource = GetComponent<AudioSource>(); // Obtener el componente AudioSource
        if (audioSource == null)
        {
            Debug.LogError("Falta el componente AudioSource en el jugador.");
        }
    }

    void Update()
    {
        Debug.Log("Juego en progreso");
        Movement();
        CheckIfShoot();
        Shoot();
        if (shield)
        {
            timer2 += Time.deltaTime;
        }
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(x, y) * (speed + ExtraSpeed);
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            // Disparo
            BulletGalaxyShooter b = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            if (critic)
            {
                int prob = Random.Range(1, 10);
                if (prob < 4)
                {
                    b.damage = (damage + extraDamage) * 2;
                }
                else
                {
                    b.damage = damage + extraDamage;
                }
            }
            else
            {
                b.damage = damage + extraDamage;
            }
            b.speed = bulletSpeed + extraBulletSpeed;
            canShoot = false;

            // Reproducir sonido de disparo
            PlayShootSound();
        }
    }

    public void TakeDamage(float dmg)
    {
        GameManager.ReduceHealth((int)dmg);

        // Reproducir sonido al recibir daño
        PlayTakeDamageSound();
    }

    void CheckIfShoot()
    {
        if (!canShoot)
        {
            if (timer < timeBtwShoot)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                canShoot = true;
            }
        }
    }

    // Método para reproducir el sonido de disparo
    void PlayShootSound()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
        else
        {
            Debug.LogWarning("No se ha asignado un AudioClip para el disparo.");
        }
    }

    // Método para reproducir el sonido al recibir daño
    void PlayTakeDamageSound()
    {
        if (audioSource != null && takeDamageSound != null)
        {
            audioSource.PlayOneShot(takeDamageSound);
        }
        else
        {
            Debug.LogWarning("No se ha asignado un AudioClip para recibir daño.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            SceneManager.LoadScene("MiniMarket");
        }
    }
}
