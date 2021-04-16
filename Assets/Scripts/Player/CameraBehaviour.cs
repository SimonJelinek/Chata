using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform cam;
    public Transform crosshair;
    private float xOffset;
    private float yOffset;
    private float zOffset;
    private Vector3 offset;
    public int camDirection;

    [Range(0,10)]
    public float aheadAmount;
    public float aheadSpeed;

    private void Start()
    {
        xOffset = 0f;
        yOffset = 0.1f;
        zOffset = -5f;
        offset = new Vector3(xOffset, yOffset, zOffset);
    }
    private void Update()
    {
        float direction = Input.GetAxisRaw("Horizontal");

        Vector3 playerPosition = transform.position;
        Vector3 newCamPosition = playerPosition + offset;
        cam.position += newCamPosition;

        /*
         * if(direction != 0)
        {
            cam.position = new Vector3(Mathf.Lerp(cam.position.x, aheadAmount * direction, aheadSpeed * Time.deltaTime), cam.position.y, cam.position.z);
        }
        */
        


    }
}
