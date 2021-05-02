using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowAI : MonoBehaviour
{
    [Header("Components")]

    public Transform player;
    public Transform eyes;
    public Transform lookBackEyes;
    public Transform lavaCheck;


    [Header("Parameters")]

    public float rayLength;
    public float lookbackRayLength;
    [Range(0,10)]
    public float speed;
    [Range(0,10)]
    public float jumpForce;

    private Rigidbody2D rb;
    private bool isFollowing;
    private bool isSearching;
    private int direction;
    private float offset;
    private bool isGrounded;


    private void Start()
    {
        speed = 1.75f;
        jumpForce = 8f;
        isFollowing = false;
        isSearching = false;
        isGrounded = true;
        direction = 1;
        offset = 0.2f;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(CanSeePlayer())
        {
            isFollowing = true;
        }
        else
        {
            if(isFollowing)
            {
                if(!isSearching)
                {
                    if(isGrounded)
                    {
                        isSearching = true;
                        Invoke("StopFollowingPlayer", 3);
                    }
                    
                }
            }      
        }

        if(isFollowing)
        {
            FollowPlayer();
            if(IsNearLava())
            {
                Jump();
            }
        }

        // Ak prave preskakuje lavu a dojdu mu 3 sekundy co nevidi playera, tak dokonci skok (nezastane vo vzduchu a nespadne do lavy)
        if (!isGrounded && !isFollowing)
        {
            FollowPlayer();
        }

        LookBack();
    }

  

    public bool CanSeePlayer()
    {
        Vector2 endPos = eyes.position + Vector3.right * rayLength * direction;
        RaycastHit2D hit = Physics2D.Linecast(eyes.position, endPos);

        Debug.DrawLine(eyes.position, endPos, Color.red);

        if(hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LookBack()
    {
        Vector2 endPos = lookBackEyes.position + Vector3.left * lookbackRayLength * direction;
        RaycastHit2D lookBack = Physics2D.Linecast(lookBackEyes.position, endPos);

        Debug.DrawLine(lookBackEyes.position, endPos, Color.blue);

        if(lookBack.collider != null && lookBack.collider.gameObject.CompareTag("Player"))
        {
            // Ak je nalavo od hraca
            if (transform.position.x < player.position.x)
            {
                Flip(1);
            }

            // Ak je napravo od hraca
            if (transform.position.x > player.position.x)
            {
                Flip(-1);
            }
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
        else if(transform.position.x < player.position.x)
        {
            Flip(1);
        }
        // Ak je napravo od hraca
        else if(transform.position.x > player.position.x)
        {
            Flip(-1);
        }
    }

    public bool IsNearLava()
    {
        Vector2 endPos = lavaCheck.position + Vector3.down * 0.4f;
        RaycastHit2D hit = Physics2D.Linecast(lavaCheck.position, endPos, LayerMask.GetMask("Lava"));

        Debug.DrawLine(lavaCheck.position, endPos, Color.yellow);

        if(hit.collider != null)
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
        if(isGrounded)
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
        isFollowing = false;
        isSearching = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void Flip(int dir)
    {
        direction = dir;
        transform.localScale = new Vector2(dir, 1);
    }
    
}
