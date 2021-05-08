using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public Vector2 checkPoint;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint1")
        {
            checkPoint = new Vector2(3.63f, -9.86f);
        }

        if (collision.gameObject.tag == "Checkpoint2")
        {
            checkPoint = new Vector2(-10, -2.46f);
        }

        if (collision.gameObject.tag == "Checkpoint3")
        {
            checkPoint = new Vector2(-3, 8.9f);
        }
    }
}
