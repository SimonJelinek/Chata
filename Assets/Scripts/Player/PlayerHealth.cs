using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Components")]
    public TextMeshProUGUI _healthUI;
    public Material _flashMat;
    public float knockbackPowerX;
    public float knockbackPowerY;
    public float knockbackFreezeTime;

    [Header("Health Variables")]
    [SerializeField] private float _maxHealth;
    private float _healthCount;
    private Material _defMat;
    private SpriteRenderer _sr;
    private Rigidbody2D _rb;
    private int direction;

    public GameObject gameOverScreen;
    public GameObject ingameScreen;
    public GameObject crossHair;

    public Movement playerMovementScript;

    void Awake()
    {
        App.playerHealth = this;
    }

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _defMat = _sr.material;
        _healthCount = _maxHealth;
        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            _sr.material = _flashMat;

            _healthCount -= 2;
            UpdateUI();

            HealthCheck();
        }

        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            _sr.material = _flashMat;

            _healthCount--;
            UpdateUI();

            HealthCheck();
        }

        if (collision.gameObject.CompareTag("Lava"))
        {
            LavaTrigger();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _sr.material = _flashMat;

            _healthCount--;
            UpdateUI();

            HealthCheck();

            if (collision.transform.position.x > transform.position.x)
            {
                // player je nalavo od enemaka
                direction = -1;
            }
            else
            {
                // player je napravo od enemaka
                direction = 1;
            }

            Knockback();

        }
    }

    void LavaTrigger()
    {
        _sr.material = _flashMat;

        _healthCount -= 2;
        UpdateUI();

        HealthCheck();

        gameObject.SetActive(false);

        transform.position = App.checkpoints.checkPoint;

        gameObject.SetActive(true);
    }

    void Knockback()
    {
        _rb.AddForce(new Vector2(knockbackPowerX * direction, knockbackPowerY), ForceMode2D.Impulse);

        playerMovementScript.knockbackFreezeTimeCounter = knockbackFreezeTime;
    }

    private void ResetMaterial()
    {
        _sr.material = _defMat;
    }

    void UpdateUI()
    {
        _healthUI.SetText(_healthCount.ToString());
    }

    void HealthCheck()
    {
        if (_healthCount > 0)
        {
            Invoke("ResetMaterial", 0.1f);
        }
        else
        {
            Invoke("GameOver", 0.75f); // animacia zomretia bude mat 0.75s
        }
    }

    void GameOver()
    { 
        gameOverScreen.SetActive(true);
        ingameScreen.SetActive(false);
        crossHair.SetActive(false);
        Destroy(gameObject);
        Cursor.visible = true;
        Time.timeScale = 0;   
    }
}