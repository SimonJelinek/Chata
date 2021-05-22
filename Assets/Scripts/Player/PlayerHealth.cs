﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Components")]
    public TextMeshProUGUI _healthUI;
    public Material _flashMat;

    [Header("Health Variables")]
    [SerializeField] private float _maxHealth;
    private float _healthCount;
    private Material _defMat;
    private SpriteRenderer _sr;

    void Awake()
    {
        App.playerHealth = this;
    }

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _defMat = _sr.material;
        _healthCount = _maxHealth;
    }

    void Update()
    {
        _healthUI.SetText(_healthCount.ToString());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            _sr.material = _flashMat;

            _healthCount -= 2;

            if (_healthCount > 0)
            {
                Invoke("ResetMaterial", 0.1f);
            }
        }

        if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("Enemy"))
        {
            _sr.material = _flashMat;

            _healthCount--;

            if (_healthCount > 0)
            {
                Invoke("ResetMaterial", 0.1f);
            }
        }

        if (collision.gameObject.tag == "Lava")
        {
            Debug.Log("Au");
        }
    }

    private void ResetMaterial()
    {
        _sr.material = _defMat;
    }
}
