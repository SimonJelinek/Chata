using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    public GameObject _shotgunAmmo;
    public GameObject _pistolAmmo;
    public GameObject _rifleAmmo;
    public int _ammoChance;

    void Start()
    {
        _ammoChance = Random.Range(0, 6);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if(_ammoChance > 0 && _ammoChance <= 3)
        {
            Instantiate(_pistolAmmo, transform.position, Quaternion.identity);
        }
        else if(_ammoChance > 3 && _ammoChance <= 5)
        {
            Instantiate(_rifleAmmo, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_shotgunAmmo, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }
}
