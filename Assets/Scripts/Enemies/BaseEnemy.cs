using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public int health;
    public GameObject rifleAmmoBox;
    public GameObject shotgunAmmoBox;

    private int randomFactor;
    private int randomFactor2;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            health--;

            Debug.Log("hit bullet");

            // flash

            if(health <= 0)
            {
                Die();
            }
        }
    }

    public virtual void Die()
    {
        randomFactor = Random.Range(1, 100);
        Debug.Log(randomFactor.ToString());
        if(randomFactor > 30)
        {
            randomFactor2 = Random.Range(0, 2);
            Debug.Log(randomFactor2.ToString());
            if(randomFactor2 == 0)
            {
                Instantiate(rifleAmmoBox, this.gameObject.transform.position, Quaternion.identity);
            }
            if(randomFactor2 == 1)
            {
                Instantiate(shotgunAmmoBox, this.gameObject.transform.position, Quaternion.identity);
            }
        }
        Destroy(this.gameObject);
    }
}
