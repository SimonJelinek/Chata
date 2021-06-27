using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxRifle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.Find("Rifle").GetComponent<Shooting>()._allBullets += 10;
            Destroy(gameObject);
        }
    }
}
