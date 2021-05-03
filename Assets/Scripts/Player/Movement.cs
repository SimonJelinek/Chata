using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Components")]

    public Transform _feetPos;
    public ParticleSystem dustParticles;
    private ParticleSystem.EmissionModule dustParticlesEmission;
    public SpriteRenderer sr;
    private Rigidbody2D _rb;
    public GameObject _onDropDustEffect;
    private AudioSource _source;
    public AudioClip _landingSound;
    public AudioClip _footsteps;
    public AudioClip _jumpingSound;

    [Header("Movement Variables")]

    [SerializeField] private float _movementAcceleration;
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _linearDrag;
    private float _horizontalDirection;
    private bool _changingDirections => (_rb.velocity.x > 0f && _horizontalDirection < 0f) || (_rb.velocity.x < 0f && _horizontalDirection > 0f);

    [Header("Jumping Variables")]

    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private float _checkRadius;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _fallMultiplier;
    [SerializeField] private float _lowJumpFallMultiplier;
    [SerializeField] private float _airLinearDrag;
    private bool _isGrounded;
    private bool _isJumping;
    private bool _spawnDustOnLand;

    [Header("Timers")]

    [SerializeField] private float _jumpTime;
    private float _jumpTimeCounter;
    [SerializeField] private float _hangTime;
    private float _hangTimeCounter;
    [SerializeField] private float _bufferTime;
    private float _bufferTimeCounter;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();
        dustParticlesEmission = dustParticles.emission;
    }

    private void Update()
    {
        if(_horizontalDirection != 0 && _isGrounded && _source.isPlaying == false)
        {
            _source.clip = _footsteps;
            _source.volume = Random.Range(0.8f, 0.3f);
            _source.pitch = Random.Range(1f, 0.9f);
            _source.Play();
        }

        _horizontalDirection = GetInput().x;
        _isGrounded = Physics2D.OverlapCircle(_feetPos.position, _checkRadius, _whatIsGround);

        FallMultiplier();

        if (_isGrounded)
        {
            if(_spawnDustOnLand == true)
            {
                _source.clip = _landingSound;
                _source.volume = Random.Range(1, 0.6f);
                _source.Play();
                Instantiate(_onDropDustEffect, _feetPos.position, Quaternion.identity);
                _spawnDustOnLand = false;
            }

            _hangTimeCounter = _hangTime;
        }
        else
        {
            _spawnDustOnLand = true;

            _hangTimeCounter -= Time.deltaTime;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            _bufferTimeCounter = _bufferTime;
        }
        else
        {
            _bufferTimeCounter -= Time.deltaTime;
        }


        if (_hangTimeCounter > 0f && _bufferTimeCounter > 0)
        {
            EffectsOnJump();

            _isJumping = true;
            _jumpTimeCounter = _jumpTime;
            _bufferTimeCounter = 0;
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.Space) && _isJumping == true)
        {
            if (_jumpTimeCounter > 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                _jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                _isJumping = false;
            }

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter();

        if (_horizontalDirection > 0)
        {
            sr.flipX = false;
        }
        else if (_horizontalDirection < 0)
        {
            sr.flipX = true;
        }

        if (_isGrounded)
        {
            ApplyLinearDrag();
        }
        else
        {
            ApplyAirLinearDrag();
            FallMultiplier();
        }

    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MoveCharacter()
    {
        _rb.AddForce(new Vector2(_horizontalDirection, 0f) * _movementAcceleration);

        // Adding dust particles
        if(GetInput().x != 0 && _isGrounded)
        {
            dustParticlesEmission.rateOverTime = 35f;
        }
        else
        {
            dustParticlesEmission.rateOverTime = 0f;
        }

        if (Mathf.Abs(_rb.velocity.x) > _maxMoveSpeed)
        {
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * _maxMoveSpeed, _rb.velocity.y);
        }
    }

    private void ApplyLinearDrag()
    {
        if (Mathf.Abs(_horizontalDirection) < 0.4f || _changingDirections)
        {
            _rb.drag = _linearDrag;
        }
        else
        {
            _rb.drag = 0f;
        }
    }

    private void ApplyAirLinearDrag()
    {
        _rb.drag = _airLinearDrag;
    }

    private void FallMultiplier()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.gravityScale = _fallMultiplier;
        }
        else if (_rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        {
            _rb.gravityScale = _lowJumpFallMultiplier;
        }
        else
        {
            _rb.gravityScale = 1f;
        }
    }

    private void EffectsOnJump()
    {
        _source.clip = _jumpingSound;
        _source.pitch = Random.Range(1f, 0.9f);
        _source.volume = Random.Range(1, 0.6f);
        _source.Play();
        Instantiate(_onDropDustEffect, _feetPos.position, Quaternion.identity);
    }

}