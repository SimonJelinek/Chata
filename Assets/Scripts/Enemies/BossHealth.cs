using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public float _healthCount = 200;
    private Material _defMat;
    private SpriteRenderer _sr;
    public Material _flashMat;
    public Slider _healthBar;
    private Animator _anim;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _defMat = _sr.material;
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        _healthBar.value = _healthCount;  

        if(_healthCount <= 0)
        {
            _anim.SetTrigger("Death");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            _sr.material = _flashMat;

            _healthCount--;

            if (_healthCount > 0)
            {
                Invoke("ResetMaterial", 0.1f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            _sr.material = _flashMat;

            _healthCount -= 5;

            if (_healthCount > 0)
            {
                Invoke("ResetMaterial", 0.1f);
            }
        }
    }
    private void ResetMaterial()
    {
        _sr.material = _defMat;
    }

}
