using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public Vector2 checkPoint;

    void Awake()
    {
        App.checkpoints = this;
        checkPoint = transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            checkPoint = transform.position;
        }
    }
}
