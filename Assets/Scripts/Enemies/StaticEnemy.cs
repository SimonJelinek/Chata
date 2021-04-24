using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    public GameObject bullet;
    public float hitDistance;
    public float spawnTime;

    float time;

    float xBulletPos;
    float yBulletPos = 0.19f;

    bool shooting = false;

    void Update()
    {
        if (shooting)
        {
            time += Time.deltaTime;

            if (time >= spawnTime)
            {
                xBulletPos = 3.19f;

                for (int x = 0; x < 3; x++)
                {
                    Instantiate(bullet, new Vector2(xBulletPos, yBulletPos), Quaternion.identity);
                    xBulletPos -= 2;
                }
                time = 0;
            }
        }        
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, hitDistance);

        if (hit.collider!=null && hit.collider.CompareTag("Player"))
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
