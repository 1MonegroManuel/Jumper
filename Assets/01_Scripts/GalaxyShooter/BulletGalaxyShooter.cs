using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGalaxyShooter : MonoBehaviour
{
    public float speed = 5;
    public float damage = 1;
    public float timeToDestroy = 4;
    public bool playerBullet = false;

    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && playerBullet)
        {
            EnemyGalaxyShooter e = collision.gameObject.GetComponent<EnemyGalaxyShooter>();
            e.TakeDamage(damage);
            Destroy(gameObject);
        }else if (collision.gameObject.CompareTag("Player") && !playerBullet)
        {
            PlayerGalaxyShooter p = collision.gameObject.GetComponent<PlayerGalaxyShooter>();
            p.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Boss") && playerBullet)
        {
            BossGalaxyShooter b = collision.gameObject.GetComponent<BossGalaxyShooter>();
            b.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}