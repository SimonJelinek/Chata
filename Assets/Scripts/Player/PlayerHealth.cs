using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Components")]
    public TextMeshProUGUI _healthUI;
    public Material _flashMat;
    public Movement playerMovementScript;
    public float knockbackPowerX;
    public float knockbackPowerY;
    public float knockbackFreezeTime;

    [Header("Health Variables")]
    [SerializeField] private float _maxHealth;
    public float deathAnimTime;
    private float _healthCount;
    private Material dissolveMat;
    private SpriteRenderer _sr;
    private Rigidbody2D _rb;
    private int direction;
    private float timer;

    public GameObject gameOverScreen;
    public GameObject ingameScreen;
    public GameObject crossHair;

    void Awake()
    {
        App.playerHealth = this;
    }

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        dissolveMat = _sr.material;
        _healthCount = _maxHealth;
        UpdateUI();
        deathAnimTime = 1;
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            dissolveMat.SetFloat("Fade", timer / deathAnimTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            _healthCount -= 2;
            UpdateUI();

            HealthCheck();
        }

        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            _healthCount--;
            UpdateUI();

            HealthCheck();
        }

        if (collision.gameObject.CompareTag("Lava"))
        {
            LavaTrigger();
        }

        if (collision.CompareTag("Offset"))
        {
            App.cameraFollow.ChangeCamera(1.6f, 2.15f);
        }

        if (collision.gameObject.CompareTag("Health"))
        {
            _healthCount += 2;
            UpdateUI();

            HealthCheck();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _healthCount--;
            UpdateUI();

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

            HealthCheck();

            if (_healthCount > 0)
            {
                Knockback();
            }
        }
    }

    void LavaTrigger()
    {
        _healthCount -= 2;
        UpdateUI();

        HealthCheck();

        if(_healthCount > 0)
        {
            gameObject.SetActive(false);

            transform.position = App.checkpoints.checkPoint;

            gameObject.SetActive(true);
        }
        
    }

    void Knockback()
    {
        _rb.AddForce(new Vector2(knockbackPowerX * direction, knockbackPowerY), ForceMode2D.Impulse);

        playerMovementScript.knockbackFreezeTimeCounter = knockbackFreezeTime;
    }

    private void ResetMaterial()
    {
        _sr.material = dissolveMat;
    }

    void UpdateUI()
    {
        if (_healthCount > 0)
        {
            _healthUI.SetText(_healthCount.ToString());
        }
        else
        {
            _healthUI.SetText("0");
        }

    }

    void HealthCheck()
    {
        if (_healthCount > 0)
        {
            _sr.material = _flashMat;
            Invoke("ResetMaterial", 0.1f);
        }
        else
        {
            _sr.material = _flashMat;
            Invoke("ResetMaterial", 0.1f);
            playerMovementScript.isAlive = false;
            _rb.velocity = new Vector2(0, 0);
            dissolveMat.SetFloat("Fade", 0);
            timer = deathAnimTime;
            Invoke("GameOver", deathAnimTime + 0.2f);
            Invoke("DestroyHand", deathAnimTime / 3);
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

    void DestroyHand()
    {
        var childObj = transform.Find("WeaponHolder");
        childObj.gameObject.SetActive(false);
    }
}