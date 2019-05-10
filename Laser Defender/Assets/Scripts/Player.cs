using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Config parameters
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float offSet = 0.5f;
    [SerializeField] int Health = 200;
    [Header("Projectile")]
    [SerializeField] float ProjectileSpeed = 10f;
    [SerializeField] float ProjectileFiringPeriod = 0.1f;
    [SerializeField] GameObject LaserPrefab;

    Coroutine FiringCoroutine;

    float xMin;
    float yMin;
    float xMax;
    float yMax;

    void Start()
    {
        SetUpMoveBoundaries();
    }

    void Update()
    {
        Move();
        Fire();
        
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
           FiringCoroutine = StartCoroutine(FireContinuous());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(FiringCoroutine);
        }
    }

    IEnumerator FireContinuous()
    {
        while (true)
        {
            GameObject Laser = Instantiate(LaserPrefab, transform.position, Quaternion.identity) as GameObject;
            Laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, ProjectileSpeed);
            yield return new WaitForSeconds(ProjectileFiringPeriod);
        }
    }

    private void Move()
    {
        var MoveX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var MoveY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + MoveX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + MoveY, yMin, yMax);
        transform.position = new Vector2(newXPos , newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera camera = Camera.main;
        xMin = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + offSet;
        yMin = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + offSet;
        xMax = camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - offSet;
        yMax = camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - offSet;
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
