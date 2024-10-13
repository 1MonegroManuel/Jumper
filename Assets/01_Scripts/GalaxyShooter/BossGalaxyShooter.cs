using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossGalaxyShooter : MonoBehaviour
{
    public float MaxLife = 50;
    float life;
    public float damage = 2;
    public float speed = 1.5f;
    public float timeBtwShoot = 2f;
    public float BulletSpeed = 3f;
    public int ExtraDamage = 1;
    public int ExtraSpeed = 1;
    float timer = 0;
    public float range = 5;
    public float dropChance = 50f;
    bool targetInRange = false;
    Transform target;
    public Transform firePoint1;
    public Transform firePoint2;
    public Transform firePoint3;
    public BulletGalaxyShooter bulletPrefab;
    public GameObject explosionEffect;
    public List<GameObject> powerUpPrefabs;

    // Sonidos
    public AudioClip shootSound;     // Sonido para disparo
    public AudioClip phaseChangeSound;  // Sonido para cambio de fase
    private AudioSource audioSource;

    public enum BossPhase { Phase1, Phase2, Phase3, Phase4 };
    public BossPhase currentPhase = BossPhase.Phase1;

    float oscillationTimer = 0;
    public float oscillationFrequency = 2f;
    public float oscillationAmplitude = 3f;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        life = MaxLife;

        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No se encontró AudioSource en el jefe.");
        }
    }

    void Update()
    {
        HandlePhaseTransition();

        switch (currentPhase)
        {
            case BossPhase.Phase1:
                Phase1Behavior();
                break;
            case BossPhase.Phase2:
                Phase2Behavior();
                break;
            case BossPhase.Phase3:
                Phase3Behavior();
                break;
            case BossPhase.Phase4:
                Phase4Behavior();
                break;
        }
    }

    public void TakeDamage(float dmg)
    {
        life -= dmg;

        if (life <= 0)
        {
            Destroy(gameObject);
            Instantiate(powerUpPrefabs[0], transform.position, transform.rotation);
        }
    }

    void HandlePhaseTransition()
    {
        float lifePercentage = life / MaxLife;

        if (lifePercentage <= 0.75f && currentPhase == BossPhase.Phase1)
        {
            currentPhase = BossPhase.Phase2;
            ChangePhaseEffect();
        }
        else if (lifePercentage <= 0.5f && currentPhase == BossPhase.Phase2)
        {
            currentPhase = BossPhase.Phase3;
            ChangePhaseEffect();
        }
        else if (lifePercentage <= 0.25f && currentPhase == BossPhase.Phase3)
        {
            currentPhase = BossPhase.Phase4;
            ChangePhaseEffect();
        }
    }

    void Phase1Behavior()
    {
        MoveOscillating();
        ShootStraight();

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }

    void Phase2Behavior()
    {
        speed = 4f;
        MoveOscillating();
        ShootSpread();
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.yellow;
        }
    }

    void Phase3Behavior()
    {
        speed = 4f;
        ExtraDamage = 3;
        BulletSpeed = 10;
        MoveOscillating();
        ShootTargeted();
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.magenta;
        }
    }

    void Phase4Behavior()
    {
        Vector2 dir = target.position - transform.position;
        float angleZ = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, -angleZ);
        speed = 3f;
        ExtraDamage = 3;
        timeBtwShoot = 1f;
        MoveOscillating();
        ShootTargeted();
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
        }
    }

    void MoveOscillating()
    {
        oscillationTimer += Time.deltaTime * oscillationFrequency;
        float oscillation = Mathf.Sin(oscillationTimer) * oscillationAmplitude;
        Vector3 pos = transform.position;
        pos.x = oscillation;
        transform.position = pos;
    }

    void ShootStraight()
    {
        if (timer < timeBtwShoot)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            BulletGalaxyShooter b1 = Instantiate(bulletPrefab, firePoint1.position, transform.rotation);
            BulletGalaxyShooter b2 = Instantiate(bulletPrefab, firePoint2.position, transform.rotation);
            BulletGalaxyShooter b3 = Instantiate(bulletPrefab, firePoint3.position, transform.rotation);
            b1.speed = BulletSpeed * ExtraSpeed;
            b2.speed = BulletSpeed * ExtraSpeed;
            b3.speed = BulletSpeed * ExtraSpeed;
            b1.damage = damage * ExtraDamage;
            b2.damage = damage * ExtraDamage;
            b3.damage = damage * ExtraDamage;

            // Reproducir sonido de disparo
            PlayShootSound();
        }
    }

    void ShootSpread()
    {
        if (timer < timeBtwShoot)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            for (int i = -2; i <= 2; i++)
            {
                Quaternion spreadRotation = Quaternion.Euler(0, 0, i * 10);
                BulletGalaxyShooter b1 = Instantiate(bulletPrefab, firePoint1.position, transform.rotation * spreadRotation);
                BulletGalaxyShooter b2 = Instantiate(bulletPrefab, firePoint2.position, transform.rotation * spreadRotation);
                BulletGalaxyShooter b3 = Instantiate(bulletPrefab, firePoint3.position, transform.rotation * spreadRotation);
                b1.speed = BulletSpeed * ExtraSpeed;
                b2.speed = BulletSpeed * ExtraSpeed;
                b3.speed = BulletSpeed * ExtraSpeed;
                b1.damage = damage * ExtraDamage;
                b2.damage = damage * ExtraDamage;
                b3.damage = damage * ExtraDamage;
            }

            // Reproducir sonido de disparo
            PlayShootSound();
        }
    }

    void ShootTargeted()
    {
        if (timer < timeBtwShoot)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            Vector2 dir = (target.position - transform.position).normalized;
            BulletGalaxyShooter b1 = Instantiate(bulletPrefab, firePoint1.position, Quaternion.LookRotation(Vector3.forward, dir));
            BulletGalaxyShooter b2 = Instantiate(bulletPrefab, firePoint2.position, Quaternion.LookRotation(Vector3.forward, dir));
            BulletGalaxyShooter b3 = Instantiate(bulletPrefab, firePoint3.position, Quaternion.LookRotation(Vector3.forward, dir));
            b1.speed = BulletSpeed * ExtraSpeed;
            b2.speed = BulletSpeed * ExtraSpeed;
            b3.speed = BulletSpeed * ExtraSpeed;
            b1.damage = damage * ExtraDamage;
            b2.damage = damage * ExtraDamage;
            b3.damage = damage * ExtraDamage;

            // Reproducir sonido de disparo
            PlayShootSound();
        }
    }

    void ChangePhaseEffect()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // Reproducir sonido de cambio de fase
        PlayPhaseChangeSound();
    }

    // Reproducir sonido de disparo
    void PlayShootSound()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    // Reproducir sonido de cambio de fase
    void PlayPhaseChangeSound()
    {
        if (audioSource != null && phaseChangeSound != null)
        {
            audioSource.PlayOneShot(phaseChangeSound);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerGalaxyShooter p = collision.gameObject.GetComponent<PlayerGalaxyShooter>();
            p.TakeDamage(damage);
        }
    }
}
