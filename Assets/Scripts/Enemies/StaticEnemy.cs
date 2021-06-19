using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : BaseEnemy
{
    public GameObject staticEnemyBullet;
    public int numberOfBullets;
    public float hitDistance; 
    public float bulletSpawnTime;
    public float bulletSpace; // medzera medzi nábojmi
    public GameObject firePoint;
    public Transform player;
    public float attackRange;

    float time;

    public float xBulletSpawnPos;
    public float yBulletSpawnPos;

    float xPos;
    bool shooting = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
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
                    Instantiate(staticEnemyBullet, firePoint.transform.position, Quaternion.identity);
                    xPos -= bulletSpace;
                }
                time = 0;
            }
        }        
    }

    void FixedUpdate()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceFromPlayer <= attackRange)
        {
            shooting = true;
        }else
        {
            shooting = false;
        }




      /*  RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, hitDistance);

        if (hit.collider!=null && hit.collider.CompareTag("Player"))
        {
            shooting = true;
        }
        else
        {
            shooting = false;
        }

        Debug.DrawRay(transform.position, Vector2.left * hitDistance, Color.red);*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
