using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Components")]

    public Transform _firePoint;
    public GameObject _bulletPrefab;

    [Header("Bullet Variables")]
    [SerializeField] private float _bulletForce = 20f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }


    private void Shoot()
    {
        GameObject _bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);

        Rigidbody2D _rb = _bullet.GetComponent<Rigidbody2D>();
        _rb.AddForce(_firePoint.right * _bulletForce, ForceMode2D.Impulse);
    }
}
