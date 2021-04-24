using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    public GameObject bullet;
    public float hitDistance;

    void FixedUpdate()
    {
        if (Physics2D.Raycast(transform.position, Vector2.left, hitDistance))
        {
            Debug.Log("hit");
        }

        Debug.DrawRay(transform.position, Vector2.left * hitDistance, Color.red);
    }
}
