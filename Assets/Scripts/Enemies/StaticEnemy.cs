using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    public GameObject bullet;
    public float hitDistance;
    public float spawnTime;
    float time;

    bool shooting = false;

    void Update()
    {
        if (shooting)
        {
            time += Time.deltaTime;

            if (time >= spawnTime)
            {
                Instantiate(bullet, new Vector2(3.9f, 0.19f), Quaternion.identity);
                time = 0;
            }
        }
    }

    void FixedUpdate()
    {
        if (Physics2D.Raycast(transform.position, Vector2.left, hitDistance))
        {
            shooting = true;
        }
        else
        {
            shooting = false;
        }

        Debug.DrawRay(transform.position, Vector2.left * hitDistance, Color.red);
    }
}
