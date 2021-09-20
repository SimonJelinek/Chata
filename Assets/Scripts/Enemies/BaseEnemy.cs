using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public int health;
    public GameObject rifleAmmoBox;
    public GameObject shotgunAmmoBox;
    public Material flashMat;

    private int randomFactor;
    private int randomFactor2;
    [HideInInspector] public Material dissolveMat;
    private SpriteRenderer sr;
    private float timer;
    private float deathAnimTime;

    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        dissolveMat = sr.material;
        deathAnimTime = 0.75f;
        timer = 0;
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

            if(health <= 0)
            {
                if (timer <= 0)
                {
                    Die();
                }
                
            }
            else
            {
                sr.material = flashMat;
                Invoke("ResetMaterial", 0.1f);
            }
        }
    }

    public virtual void ResetMaterial()
    {
        sr.material = dissolveMat;
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
        if (randomFactor > 20)
        {
            randomFactor2 = Random.Range(1, 100);
            if (randomFactor2 < 65)
            {
                rifleAmmoBox.transform.parent = null;
                rifleAmmoBox.SetActive(true);
            }
            else
            {
                shotgunAmmoBox.transform.parent = null;
                shotgunAmmoBox.SetActive(true);
            }
        }
        this.gameObject.SetActive(false);
    }
}
