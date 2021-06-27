using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairBehavior : MonoBehaviour
{
    public GameObject crosshair;
    Vector2 mousePos;
    Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
        crosshair.SetActive(true);
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        mousePos = camera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        crosshair.transform.position = mousePos;
    }
}
