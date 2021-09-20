using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyAI : BaseEnemy
{
    [Header("Components")]
    public GameObject _bullet;
    public GameObject _firePoint;
    public GameObject ruka;
    //public Transform[] _moveSpots;
    private Animator _anim;
    private AudioSource _source;
    public AudioClip _shoot;

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
    private Vector3 dir;
    private float angle;
    private bool lookingLeft;
    
    
    [Header("Health")]
    // [SerializeField] private float _maxHealth; - v BaseEnemy
    private float _health;
    //private Material _defMat;
    //public Material _flashMat;
    private SpriteRenderer _sr;

    public override void Start()
    {
        _source = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        base.Start();

        //_randomSpot = Random.Range(0, _moveSpots.Length);
        _waitTime = _startWaitTime;

        _sr = GetComponent<SpriteRenderer>();
        //_flashMat = Resources.Load("Flash", typeof(Material)) as Material;
        //_defMat = _sr.material;

        // _health = _maxHealth; - v BaseEnemy
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        lookingLeft = true;
    }

    public override void Update()
    {
        base.Update();
        RaycastHit2D hit = Physics2D.Linecast(gameObject.transform.position, _player.transform.position, 1 << LayerMask.NameToLayer("Ground"));

        if (following == false)
        {
            if (hit.collider == null)
            {
                _inSight = true;
               // Debug.Log("Player in sight");
            }
            else
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    _inSight = false;
                    //Debug.Log("Hit wall");
                }
            }
            //Patroling();
        }

        if (_inSight == true)
        {
            



            if (_player.position.x > transform.position.x && lookingLeft)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                transform.rotation = Quaternion.AngleAxis(-10f, Vector3.forward);
                lookingLeft = !lookingLeft;
            }
            if(_player.position.x < transform.position.x && !lookingLeft)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                transform.rotation = Quaternion.AngleAxis(10f, Vector3.forward);
                lookingLeft = !lookingLeft;
                
            }


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

        ArmPointing();

        if (_distanceFromPlayer < _lineOfSight && _distanceFromPlayer > _shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, _player.position, _enemySpeed * Time.deltaTime);
            _lineOfSight = 14f;
        }
        else if (_distanceFromPlayer <= _shootingRange && _nextFireTime < Time.time)
        {
            _source.clip = _shoot;
            _source.Play();
            _anim.SetTrigger("Shoot");
            Instantiate(_bullet, _firePoint.transform.position, Quaternion.identity);
            _nextFireTime = Time.time + _fireRate;
        }
        else
        {
            following = false;
            _lineOfSight = 14f;
        }
    }

    public void ArmPointing()
    {
        dir = _player.position - ruka.transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        ruka.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

   /* private void Patroling()
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
    }*/

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
}
