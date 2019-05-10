using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int Health = 100;
    [SerializeField] float ShotCounter;
    [SerializeField] float MinTimeShot = 0.2f;
    [SerializeField] float MaxTimeShot = 3f;
    [SerializeField] GameObject LaserPrefab;
    [SerializeField] float ProjectileSpeed = 10f;
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
            Destroy(gameObject);
        }
        damageDealer.Hit();
    }
}
