using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] int Health = 100;
    [SerializeField] float ShotCounter;
    [SerializeField] float MinTimeShot = 0.2f;
    [SerializeField] float MaxTimeShot = 3f;
    [SerializeField] int ScoreValue = 150;
    [Header("Projectile")]
    [SerializeField] GameObject LaserPrefab;
    [SerializeField] float ProjectileSpeed = 10f;
    [Header("VFX")]
    [SerializeField] GameObject Explosion;
    [Header("Audio")]
    [SerializeField] AudioClip DeathSFX;
    [SerializeField] [Range(0,1)] float DeathVolume = 0.75f;
    [SerializeField] AudioClip ShootSFX;
    [SerializeField] [Range(0, 1)] float ShootVolume = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        ShotCounter = Random.Range(MinTimeShot, MaxTimeShot);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        ShotCounter -= Time.deltaTime;
        if(ShotCounter <= 0f)
        {
            Fire();
            ShotCounter = Random.Range(MinTimeShot, MaxTimeShot);
        }
    }

    private void Fire()
    {
        GameObject Laser = Instantiate(LaserPrefab, transform.position, Quaternion.identity) as GameObject;
        Laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -ProjectileSpeed);
        AudioSource.PlayClipAtPoint(ShootSFX, Camera.main.transform.position, ShootVolume);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        Health -= damageDealer.GetDamage();
        if (Health <= 0)
        {
            Die();
        }
        damageDealer.Hit();
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(ScoreValue);
        Destroy(gameObject);
        GameObject explosionVFX = Instantiate(Explosion, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(DeathSFX, Camera.main.transform.position, DeathVolume);
        Destroy(explosionVFX, 1f);
    }
}
