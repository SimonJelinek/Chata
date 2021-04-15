using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Camera cam;
    private float xOffset;
    private float yOffset;
    private float zOffset;
    private Vector3 offset;

    private void Start()
    {
        xOffset = 0f;
        yOffset = 0.1f;
        zOffset = -5f;
        offset = new Vector3(xOffset, yOffset, zOffset);
    }
    private void Update()
    {
        Vector3 playerPosition = transform.position;
        Vector3 newCamPosition = playerPosition + offset;
        cam.transform.position = newCamPosition;
    }
}
