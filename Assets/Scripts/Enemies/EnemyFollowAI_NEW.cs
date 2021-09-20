using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowAI_NEW : BaseEnemy
{

    private Transform player;
    public Transform eyes;
    public Transform lavaCheck;
    public Animator _anim;
    private AudioSource _source;
    public AudioClip _run;

    public float seeDistance;
    [Range(0, 10)]
    public float speed;
    [Range(0, 10)]
    public float jumpForce;

    private int layerMask;
    private Vector2 dir;
    private float offset;
    private Rigidbody2D rb;
    private int direction;
    private bool isFollowing;
    private bool isGrounded;
    public SpriteRenderer[] bodyParts;
    private bool _runSound;

    public override void Start()
    {
        _source = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
        layerMask = ~(1 << 12);
        rb = GetComponent<Rigidbody2D>();
        offset = 0.2f;
        direction = 1;
        bodyParts = GetComponentsInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
       
        if (CanSeePlayer())
        {
            _runSound = true;
            _anim.SetBool("Run", true);
            isFollowing = true;
        }
        else
        {
            _runSound = false;
            _anim.SetBool("Run", false);
            Invoke("StopFollowingPlayer", 3);
        }

        if(isFollowing)
        {
            FollowPlayer();
            if(IsNearLava())
            {
                Jump();
            }
        }

        if (!isGrounded && !isFollowing)
        {
            FollowPlayer();
        }

        if (_runSound == true && _source.isPlaying == false)
        {
            _source.clip = _run;
            _source.Play();
        }
    }

    public bool CanSeePlayer()
    {
        dir = (player.position - eyes.position);
        // dir.Normalize();
        RaycastHit2D hit = Physics2D.Raycast(eyes.position, dir, seeDistance, layerMask);
        Debug.DrawRay(eyes.position, dir, Color.blue);

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void FollowPlayer()
    {
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);

        // Ak je presne pod/nad hracom, aby sa netriasol
        if (transform.position.x >= player.position.x - offset && transform.position.x <= player.position.x + offset)
        {
            rb.velocity = Vector2.zero;
        }
        // Ak je nalavo od hraca
        else if (transform.position.x < player.position.x)
        {
            Flip(1);
        }
        // Ak je napravo od hraca
        else if (transform.position.x > player.position.x)
        {
            Flip(-1);
        }
    }

    public bool IsNearLava()
    {
        Vector2 endPos = lavaCheck.position + Vector3.down;
        RaycastHit2D hit = Physics2D.Linecast(lavaCheck.position, endPos, layerMask);

        Debug.DrawLine(lavaCheck.position, endPos, Color.yellow);

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Lava"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    public void StopFollowingPlayer()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void Flip(int dir)
    {
        direction = dir;
        transform.localScale = new Vector2(dir, 1);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.gameObject.CompareTag("Bullet"))
        {
            if(health > 0)
            {
                foreach (SpriteRenderer part in bodyParts)
                {
                    part.material = flashMat;
                }
                Invoke("ResetMaterial", 0.03f);
            }
        }
    }

    public override void ResetMaterial()
    {
        base.ResetMaterial();

        foreach (SpriteRenderer part in bodyParts)
        {
            part.material = dissolveMat;
        }

    }
}
