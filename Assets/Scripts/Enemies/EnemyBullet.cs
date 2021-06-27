using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Components")]
    GameObject _target;
    Rigidbody2D _rb;

    [Header("EnemyBulletVariables")]
    [SerializeField] float _speed;
    float time;
    float destroyTime = 2;
    bool timer = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _target = GameObject.FindGameObjectWithTag("Player");
        Vector2 _moveDir = (_target.transform.position - transform.position).normalized * _speed;
        _rb.velocity = new Vector2(_moveDir.x, _moveDir.y);

        timer = true;
    }

    void Update()
    {
        if (timer)
        {
            time += Time.deltaTime;

            if (time > destroyTime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ammo") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Checkpoint"))
        {

        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Enemy"))
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
