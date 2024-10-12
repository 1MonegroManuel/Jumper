using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 1.0f;
    public float bulletSpeed = 5f;
    public GameObject bulletPrefab;  // Prefab de la bala
    public Transform shootPoint;      // Punto de disparo
    public float timeBtwShoot = 1.5f;
    public float health = 10f;         // Salud m�xima
    public Image lifeBar;
    public GameObject deathParticlesPrefab; // Prefab de part�culas para la muerte
    public float deathDelay = 1.3f;
    public SpriteRenderer mainSpriteRenderer;
    public SpriteRenderer deathSpriteRenderer;
    public Animator animator; // Referencia al Animator

    private float maxHealth;
    private float shootTimer = 0.5f;
    private bool isFacingRight = true; // Para controlar la direcci�n del sprite

    void Start()
    {
        maxHealth = health;
        UpdateLifeBar();
    }

    void Update()
    {
        Movement();
        HandleShooting();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        UpdateLifeBar();

        if (health <= 0)
        {
            Die();
        }
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(x, y);
        rb.velocity = movement * speed;

        // Voltear el sprite seg�n la direcci�n del movimiento horizontal
        if (x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (x < 0 && isFacingRight)
        {
            Flip();
        }

        // Activar o desactivar la animaci�n de "Walking"
        if (animator != null)
        {
            bool isMoving = movement.magnitude > 0.1f;
            animator.SetBool("IsWalking", isMoving);
        }
    }

    void HandleShooting()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= timeBtwShoot && Input.GetMouseButtonDown(0))
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && shootPoint != null)
        {
            // Obt�n la posici�n del rat�n en el mundo
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)shootPoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            if (bulletRb != null)
            {
                bulletRb.velocity = direction * bulletSpeed;
            }
        }
    }
    void UpdateLifeBar()
    {
        if (lifeBar != null)
        {
            lifeBar.fillAmount = health / maxHealth;
        }
    }



    void Die()
    {
        Debug.Log("�El jugador ha muerto!");
        StartCoroutine(HandleDeath());
    }

    IEnumerator HandleDeath()
    {
        if (mainSpriteRenderer != null)
        {
            mainSpriteRenderer.enabled = false;
        }
        if (deathSpriteRenderer != null)
        {
            deathSpriteRenderer.enabled = true;
        }
        if (deathParticlesPrefab != null)
        {
            Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
        }
        // Esperar por el tiempo especificado antes de reiniciar el nivel
        yield return new WaitForSeconds(deathDelay);

        // Reiniciar el nivel despu�s del retraso
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Destruir el jugador despu�s de reiniciar la escena
        Destroy(gameObject);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        if (mainSpriteRenderer != null)
        {
            Vector3 theScale = mainSpriteRenderer.transform.localScale;
            theScale.x *= -1;  // Voltea la escala en el eje X
            mainSpriteRenderer.transform.localScale = theScale;
        }
    }
}
