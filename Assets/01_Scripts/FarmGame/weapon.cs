using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public float speed = 5f;
    public float timeToDestroy = 4f;
    public float damage = 1f;
    public float rotationSpeed = 5f;  // Velocidad de rotación
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        Destroy(gameObject, timeToDestroy);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Mueve la bala hacia adelante
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // Calcula el ángulo deseado basándose en la dirección de movimiento
        float targetAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;

        // Crea una rotación basada en el ángulo objetivo
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, targetAngle));

        // Interpola suavemente la rotación actual hacia la rotación objetivo
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }
    }
}
