using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyAI : BaseEnemy
{
    [Header("Components")]
    public GameObject _bullet;
    public GameObject _firePoint;
    public Transform[] _moveSpots;

    [Header("Enemy Variables")]
    [SerializeField] private float _nextFireTime;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _enemySpeed;
    [SerializeField] private float _enemySpeedPatroling;
    [SerializeField] private float _lineOfSight;
    [SerializeField] private float _shootingRange;
    [SerializeField] private float _startWaitTime;
    private int _randomSpot;
    private Transform _player;
    private bool _inSight = false;
    private bool following = false;
    private float _waitTime;
    
    
    [Header("Health")]
    // [SerializeField] private float _maxHealth; - v BaseEnemy
    private float _health;
    private Material _defMat;
    public Material _flashMat;
    private SpriteRenderer _sr;

    void Start()
    {
        _randomSpot = Random.Range(0, _moveSpots.Length);
        _waitTime = _startWaitTime;

        _sr = GetComponent<SpriteRenderer>();
        //_flashMat = Resources.Load("Flash", typeof(Material)) as Material;
        _defMat = _sr.material;

        // _health = _maxHealth; - v BaseEnemy
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    { 

        RaycastHit2D hit = Physics2D.Linecast(gameObject.transform.position, _player.transform.position, 1 << LayerMask.NameToLayer("Ground"));

        if (following == false)
        {
            if (hit.collider == null)
            {
                _inSight = true;
                Debug.Log("Player in sight");
            }
            else
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    _inSight = false;
                    Debug.Log("Hit wall");
                }
            }
            Patroling();
        }

        if (_inSight == true)
        {
            following = true;
            Spotted();
        }

        /*if(_health <= 0)
        {
            OnDeath();
        } 
        - v BaseEnemy
        */
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _lineOfSight);
        Gizmos.DrawWireSphere(transform.position, _shootingRange);
    }

    private void Spotted()
    {
        float _distanceFromPlayer = Vector2.Distance(_player.position, transform.position);

        if (_distanceFromPlayer < _lineOfSight && _distanceFromPlayer > _shootingRange)
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
            following = false;
            _lineOfSight = 7;
        }
    }

    private void Patroling()
    {
        transform.position = Vector2.MoveTowards(transform.position, _moveSpots[_randomSpot].position, _enemySpeedPatroling * Time.deltaTime);

        if(Vector2.Distance(transform.position, _moveSpots[_randomSpot].position) < 0.2f)
        {
            if(_waitTime <= 0)
            {
                _randomSpot = Random.Range(0, _moveSpots.Length);
                _waitTime = _startWaitTime;
            }
            else
            {
                _waitTime -= Time.deltaTime;
            }
        }
    }

    /*public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag("Bullet"))
        {
            _sr.material = _flashMat;

            // _health--; - v BaseEnemy

            if(_health > 0)
            {
                Invoke("ResetMaterial", 0.1f);
            }
        }
    }
    // - flash sa bude riesit v BaseEnemy
    */

    /*private void ResetMaterial()
    {
        _sr.material = _defMat;
    }
    - pojde do BaseEnemy
     */

    /*private void OnDeath()
    {
        Destroy(gameObject);
    }
    - v BaseEnemy
     */

    public override void Die()
    {
        base.Die();
        // unique death animation
    }

}
