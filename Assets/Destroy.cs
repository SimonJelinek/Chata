using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private float _timer;
    public GameObject _box;

    void Update()
    {
        if (_timer >= 1 && _timer < 2)
        {
            _timer = 3;
            Instantiate(_box, transform.position, Quaternion.identity);
            Invoke("DestroyObject", 0.5f);
        }
        else if(_timer < 1)
        {
            _timer += Time.deltaTime;
        }
    }
    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
