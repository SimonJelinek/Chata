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
    public TextMeshProUGUI _reloadingText;

    [Header("Weapon Types")]

     public int _allBullets;
    [SerializeField] private int _magazineSize;
    [SerializeField] private int _bulletsLeft;
    [SerializeField] private float _reloadTime;
    [SerializeField] private bool _allowButtonHold;
    [SerializeField] private float _timeBetweenShooting;
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private int _bulletsPerTap;
    [SerializeField] private float _spread;
    private bool _shooting;
    private bool _reloading = false;
    private bool _allowInvoke = true;
    private bool _readyToShoot = true;
    private int _bulletsShot;

    [Header("Bullet Variables")]

    [SerializeField] private float _bulletForce = 20f;


    private void Update()
    {
        if(_allowButtonHold == true)
        {
            _shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            _shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
       

        if (_readyToShoot && _shooting && _bulletsLeft > 0 && _reloading == false)
        {
            _bulletsShot = 0;

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
            _ammunitionDisplay.SetText(_bulletsLeft / _bulletsPerTap + " / " + _allBullets / _bulletsPerTap);
        }

        if(_reloading)
        {
            _reloadingText.gameObject.SetActive(true);
            Debug.Log("reloading is true");
        } 
        else
        {
            _reloadingText.gameObject.SetActive(false);
            Debug.Log("reloading is false");
        }


    }


    private void Shoot()
    {
            _readyToShoot = false;
        
            GameObject _bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);

            float x = Random.Range(-_spread, _spread);
            float y = Random.Range(-_spread, _spread);

            Rigidbody2D _rb = _bullet.GetComponent<Rigidbody2D>();
            _rb.AddForce(_firePoint.right * _bulletForce + new Vector3(x, y, 0), ForceMode2D.Impulse);

            _bulletsLeft--;
            _bulletsShot++;

            if (_allowInvoke)
            {
                Invoke("ResetShot", _timeBetweenShooting);
                _allowInvoke = false;
            }

            if(_bulletsShot < _bulletsPerTap && _bulletsLeft > 0)
            {
                Invoke("Shoot", _timeBetweenShots);
            }
         
    }

    private void ResetShot()
    {
        _readyToShoot = true;
        _allowInvoke = true;
    }


    private void Reload()
    {
        if(_bulletsLeft < _magazineSize)
        {
            if (_bulletsPerTap >= 2)
            {
                _reloading = true;
                Invoke("ReloadFinishedShotgun", _reloadTime);
            }
            else
            {
                _reloading = true;
                Invoke("ReloadFinished", _reloadTime);
            }
        }
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
                if(_bulletsLeft + _allBullets > _magazineSize)
                {
                    _allBullets -= _magazineSize - _bulletsLeft;
                    _bulletsLeft = _magazineSize;
                    _reloading = false;
                }
                else
                {                  
                    _bulletsLeft += _allBullets;
                    _allBullets = 0;
                    _reloading = false;
                }
            }
            if (_bulletsLeft == 0)
            {
                _bulletsLeft = _allBullets;
                _allBullets = 0;
                _reloading = false;
            }
        }       
    }

    private void ReloadFinishedShotgun()
    {
        if (_allBullets >= _magazineSize && _reloading == true)
        {
            if (_bulletsLeft > 0)
            {
                _bulletsLeft += _bulletsPerTap;
                _allBullets -= _bulletsPerTap;
                _reloading = false;
                Invoke("Reload", 0.15f);
            }
            if (_bulletsLeft == 0)
            {
                _bulletsLeft += _bulletsPerTap;
                _allBullets -= _bulletsPerTap;
                _reloading = false;
                Invoke("Reload", 0.15f);
            }
        }
        if (_allBullets < _magazineSize && _reloading == true)
        {
            if (_bulletsLeft > 0)
            {
                _bulletsLeft += _bulletsPerTap;
                _allBullets -= _bulletsPerTap;
                _reloading = false;
                if(_allBullets > 0)
                {
                    Invoke("Reload", 0.15f);
                }

            }
            if (_bulletsLeft == 0)
            {
                _bulletsLeft += _bulletsPerTap;
                _allBullets -= _bulletsPerTap;
                _reloading = false;
                if (_allBullets > 0)
                {
                    Invoke("Reload", 0.15f);
                }
            }
        }
    }

}

