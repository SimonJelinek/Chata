using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyAI : MonoBehaviour
{
    [Header("Components")]
    public GameObject _bullet;
    public GameObject _firePoint;

    [Header("Enemy Variables")]
    [SerializeField] private float _nextFireTime;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _enemySpeed;
    [SerializeField] private float _lineOfSight;
    [SerializeField] private float _shootingRange;
    private Transform _player;
    
    [Header("Health")]
    [SerializeField] private float _maxHealth;
    private float _health;
    private Material _defMat;
    public Material _flashMat;
    private SpriteRenderer _sr;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        //_flashMat = Resources.Load("Flash", typeof(Material)) as Material;
        _defMat = _sr.material;

        _health = _maxHealth;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float _distanceFromPlayer = Vector2.Distance(_player.position, transform.position);

        if(_distanceFromPlayer < _lineOfSight && _distanceFromPlayer > _shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, _player.position, _enemySpeed * Time.deltaTime);
            _lineOfSight = 11;
        }
        else if (_distanceFromPlayer <= _shootingRange && _nextFireTime < Time.time)
        {
            Instantiate(_bullet, _firePoint.transform.position, Quaternion.identity);
            _nextFireTime = Time.time + _fireRate;
        }
        else
        {
            _lineOfSight = 7;
        }

        if(_health <= 0)
        {
            OnDeath();
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _lineOfSight);
        Gizmos.DrawWireSphere(transform.position, _shootingRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            _sr.material = _flashMat;
            _health--;

            if(_health > 0)
            {
                Invoke("ResetMaterial", 0.1f);
            }
        }
    }

    private void ResetMaterial()
    {
        _sr.material = _defMat;
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }

}
