using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Vector3 lookAheadAmount;
    [Range(0, 10f)]
    public float speed;

    private int direction;
    private Vector3 mousePos;


    private void Update()
    {   
        mousePos = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

        if(mousePos.x > target.position.x)
        {
            direction = 1;
        }
        if(mousePos.x < target.position.x)
        {
            direction = -1;
        }
        
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z) + offset + (lookAheadAmount * direction), speed * Time.fixedDeltaTime);

        /*
         
        if(Input.GetAxis("Horizontal") > 0)
        {
            direction = 1;
        }
        if(Input.GetAxis("Horizontal") < 0)
        {
            direction = -1;
        }

        */
    }


}
