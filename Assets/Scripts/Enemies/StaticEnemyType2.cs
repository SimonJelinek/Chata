using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyType2 : MonoBehaviour
{
    public GameObject staticEnemyBullet2;
    public float hitDistance;
    public float bulletSpawnTime;

    float time;
    public float xBulletSpawnPos;
    public float yBulletSpawnPos;

    bool shooting = false;

    void Update()
    {
        if (shooting)
        {
            time += Time.deltaTime;

            if (time >= bulletSpawnTime)
            {
                Instantiate(staticEnemyBullet2, new Vector2(xBulletSpawnPos, yBulletSpawnPos), Quaternion.identity);               
                time = 0;
            }
        }
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, hitDistance);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
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
