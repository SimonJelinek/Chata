using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyBullet : MonoBehaviour
{
    float time;
    float destroyTime = 3;
    bool timer = false;

    void Start()
    {
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
        if (collision.CompareTag("Ammo"))
        {

        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
