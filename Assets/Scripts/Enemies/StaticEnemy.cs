using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    public GameObject staticEnemyBullet;
    public int numberOfBullets;
    public float hitDistance; 
    public float bulletSpawnTime;
    public float bulletSpace; // medzera medzi nábojmi

    float time;
    float xBulletSpawnPos;
    float yBulletSpawnPos = 0.19f;

    bool shooting = false;

    void Update()
    {
        if (shooting)
        {
            time += Time.deltaTime;

            if (time >= bulletSpawnTime)
            {
                xBulletSpawnPos = 4f;

                for (int x = 0; x < numberOfBullets; x++)
                {
                    Instantiate(staticEnemyBullet, new Vector2(xBulletSpawnPos, yBulletSpawnPos), Quaternion.identity);
                    xBulletSpawnPos -= bulletSpace;
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
