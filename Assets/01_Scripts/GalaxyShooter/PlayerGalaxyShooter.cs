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
    public float damage= 5f;
    public bool critic = false;
    public float bulletSpeed= 10f;
    public float extraBulletSpeed = 0f;
    float timer = 0;
    float timer2 = 0;
    public bool shield;
    bool canShoot = true;
    public Rigidbody2D rb;
    public Transform firePoint;
    public BulletGalaxyShooter bulletPrefab;
    void Start()
    {
        Debug.Log("Inició el juego");
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
        rb.velocity = new Vector2(x, y) * (speed+ExtraSpeed);
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            BulletGalaxyShooter b = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            if (critic)
            {
                int prob = Random.Range(1, 10);
                if (prob < 4)
                {
                    b.damage = (damage + extraDamage)*2;
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
            b.speed= bulletSpeed + extraBulletSpeed;
            canShoot = false;

        }
    }


    public void TakeDamage(float dmg)
    {
        GameManager.ReduceHealth((int)dmg);
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            GameManager.Portal();
        }

    }

}