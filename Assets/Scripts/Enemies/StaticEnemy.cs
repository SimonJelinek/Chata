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

    public float xBulletSpawnPos;
    public float yBulletSpawnPos;

    float xPos;
    bool shooting = false;

    void Update()
    {
        if (shooting)
        {
            time += Time.deltaTime;

            if (time >= bulletSpawnTime)
            {
                xPos = xBulletSpawnPos;

                for (int x = 0; x < numberOfBullets; x++)
                {
                    Instantiate(staticEnemyBullet, new Vector2(xPos, yBulletSpawnPos), Quaternion.identity);
                    xPos -= bulletSpace;
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
