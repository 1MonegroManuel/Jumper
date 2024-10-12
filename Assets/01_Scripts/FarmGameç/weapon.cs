using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public float speed = 5f;
    public float timeToDestroy = 4f;
    public float damage = 1f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        Destroy(gameObject, timeToDestroy);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // Ajusta la rotación de la bala para que apunte en la dirección de movimiento
        float angle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
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
