using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxShotgun : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.Find("Shotgun").GetComponent<Shooting>()._allBullets += 15;
            Destroy(gameObject);
        }
    }
}
