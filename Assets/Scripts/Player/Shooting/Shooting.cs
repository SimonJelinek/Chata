using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    [Header("Components")]

    public Transform _firePoint;
    public GameObject _bulletPrefab;
    public TextMeshProUGUI _ammunitionDisplay;

    [Header("Magazine")]

    [SerializeField] private int _allBullets;
    [SerializeField] private int _magazineSize = 2;
    [SerializeField] private int _bulletsLeft;
    [SerializeField] private float _reloadTime;
    private bool _reloading = false;

    [Header("Bullet Variables")]

    [SerializeField] private float _bulletForce = 20f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && _bulletsLeft > 0)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && _bulletsLeft < _magazineSize && !_reloading && _allBullets > 0)
        {
            Reload();
        }
        if (!_reloading && _bulletsLeft <= 0 && _allBullets > 0)
        {
            Reload();
        }

        if (_ammunitionDisplay != null)
        {
            _ammunitionDisplay.SetText(_bulletsLeft + " / " + _allBullets);
        }

    }


    private void Shoot()
    {
        GameObject _bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);

        Rigidbody2D _rb = _bullet.GetComponent<Rigidbody2D>();
        _rb.AddForce(_firePoint.right * _bulletForce, ForceMode2D.Impulse);

        _bulletsLeft --;
    }

    private void Reload()
    {
        _reloading = true;
        Invoke("ReloadFinished", _reloadTime); 
    }

    private void ReloadFinished()
    {
        
        if(_allBullets >= _magazineSize && _reloading == true)
        {
            if(_bulletsLeft > 0)
            {
                _allBullets -= _magazineSize - _bulletsLeft;
                _bulletsLeft = _magazineSize;
                _reloading = false;
            }
            if (_bulletsLeft == 0)
            {
                _bulletsLeft = _magazineSize;
                _allBullets -= _magazineSize;
                _reloading = false;
            }
        }
        if(_allBullets < _magazineSize && _reloading == true)
        {
            if (_bulletsLeft > 0)
            {
                _allBullets -= _magazineSize - _bulletsLeft;
                _bulletsLeft = _magazineSize;
                _reloading = false;
            }
            if (_bulletsLeft == 0)
            {
                _bulletsLeft = _allBullets;
                _allBullets = 0;
                _reloading = false;
            }
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ammo"))
        {
            _allBullets = 10;
            Destroy(collision.gameObject);
        }
    }

}

