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
    private Material dissolveMat;
    private SpriteRenderer sr;
    private float timer;
    private float deathAnimTime;

    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        dissolveMat = sr.material;
        deathAnimTime = 0.75f;
    }

    public virtual void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            dissolveMat.SetFloat("Fade", timer / deathAnimTime);
        }

    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            health--;

            // flash

            if(health <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        dissolveMat.SetFloat("Fade", 1);
        timer = deathAnimTime;
        Invoke("DestroyEnemy", deathAnimTime);
    }

    public void DestroyEnemy()
    {
        randomFactor = Random.Range(1, 100);
        if (randomFactor > 30)
        {
            randomFactor2 = Random.Range(0, 2);
            if (randomFactor2 == 0)
            {
                Instantiate(rifleAmmoBox, this.gameObject.transform.position, Quaternion.identity);
            }
            if (randomFactor2 == 1)
            {
                Instantiate(shotgunAmmoBox, this.gameObject.transform.position, Quaternion.identity);
            }
        }
        Destroy(this.gameObject);
    }
}
