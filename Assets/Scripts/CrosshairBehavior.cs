using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairBehavior : MonoBehaviour
{
    public GameObject crosshair;
    Vector2 mousePos;

    private void Start()
    {
        crosshair.SetActive(true);
        Cursor.visible = false;
    }

    void Update()
    {
        mousePos = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        crosshair.transform.position = mousePos;
    }
}
