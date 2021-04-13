using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHit : MonoBehaviour
{
    public Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            anim.SetTrigger("Hit");
        }
    }
}
