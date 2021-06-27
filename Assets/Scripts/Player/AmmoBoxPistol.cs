using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxPistol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.Find("Pistol").GetComponent<Shooting>()._allBullets += 10;
            Destroy(gameObject);
        }
    }
}
