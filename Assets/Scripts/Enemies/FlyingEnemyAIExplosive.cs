using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyAIExplosive : MonoBehaviour
{
    [Header("Components")]
    private CircleCollider2D _explosionCollider;
    public GameObject _explosion;
    public GameObject _explosionParticles;
    private Transform _player;
    //public Transform[] _moveSpots;
    private Animator _anim;

    [Header("Enemy Variables")]
    [SerializeField] private float _enemySpeed;
    [SerializeField] private float _enemySpeedPatroling;
    [SerializeField] private float _lineOfSight;
    [SerializeField] private float _explodeRange;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _chargeTime;
    [SerializeField] private float _startWaitTime;
    private bool _exploding = false;
    private bool _exploding2 = true;
    private bool _inSight = false;
    private bool following = false;
    private float _waitTime;
    private int _randomSpot;

    [Header("Health")]
    [SerializeField] private float _maxHealth;
    private float _health;
    private Material _defMat;
    public Material _flashMat;
    private SpriteRenderer _sr;

    void Start()
    {
        _anim = GetComponent<Animator>();

        //_randomSpot = Random.Range(0, _moveSpots.Length);
        _waitTime = _startWaitTime;

        /* _explosionCollider = GetComponentInChildren<CircleCollider2D>();
         _explosionCollider.radius = _explosionRadius;*/

        _sr = GetComponent<SpriteRenderer>();
        _defMat = _sr.material;

        _health = _maxHealth;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float _distanceFromPlayer = Vector2.Distance(_player.position, transform.position);

        RaycastHit2D hit = Physics2D.Linecast(gameObject.transform.position, _player.transform.position, 1 << LayerMask.NameToLayer("Ground"));

        if (following == false)
        {
            if (hit.collider == null)
            {
                _inSight = true;
                //Debug.Log("Player in sight");
            }
            else
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    _inSight = false;
                    //Debug.Log("Hit wall");
                }
            }
          // Patroling();
        }

        if (_inSight == true)
        {
            following = true;
            Spotted();
        }

        if (_health <= 0)
        {
            _exploding = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _lineOfSight);
        Gizmos.DrawWireSphere(transform.position, _explodeRange);
    }

    private void Spotted()
    {
        float _distanceFromPlayer = Vector2.Distance(_player.position, transform.position);

        if (_exploding == true)
        {
            if (_exploding2 == true)
            {
                _anim.SetTrigger("Boom");
                _exploding2 = false;
                Invoke("Explode", _chargeTime);
            }
        }
        else
        {

            if (_distanceFromPlayer < _lineOfSight && _distanceFromPlayer > _explodeRange)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, _player.position, _enemySpeed * Time.deltaTime);
                _lineOfSight = 100;
            }
            else if (_distanceFromPlayer <= _explodeRange)
            {
                _exploding = true;
            }
        }

    }

   /* private void Patroling()
    {
        transform.position = Vector2.MoveTowards(transform.position, _moveSpots[_randomSpot].position, _enemySpeedPatroling * Time.deltaTime);

        if (Vector2.Distance(transform.position, _moveSpots[_randomSpot].position) < 0.2f)
        {
            if (_waitTime <= 0)
            {
                _randomSpot = Random.Range(0, _moveSpots.Length);
                _waitTime = _startWaitTime;
            }
            else
            {
                _waitTime -= Time.deltaTime;
            }
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
           // _sr.material = _flashMat;
            _health--;

            if (_health > 0)
            {
                Invoke("ResetMaterial", 0.1f);
            }
        }
    }

    private void ResetMaterial()
    {
        _sr.material = _defMat;
    }

    private void Explode()
    {       
            _sr.material = _flashMat;
            //Instantiate(_explosionParticles, gameObject.transform.position, Quaternion.identity);
            _explosion.SetActive(true);
            Destroy(gameObject, 0.15f);       
    }

}
