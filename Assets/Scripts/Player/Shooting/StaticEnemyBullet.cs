using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyBullet : MonoBehaviour
{
    private Rigidbody2D rb;

    public float bulletSpeed;

    float time;
    float destroyTime = 3;
    bool timer = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(bulletSpeed, rb.velocity.y);
        timer = true;
    }

    void Update()
    {
        if (timer)
        {
            time += Time.deltaTime;

            if (time > destroyTime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ammo") || collision.CompareTag("Enemy"))
        {

        }
        else
        {
            Destroy(gameObject);
        }

    }

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }*/
}
