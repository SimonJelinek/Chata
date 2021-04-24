using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowAI : MonoBehaviour
{
    [Header("Components")]
    public Transform player;
    public Transform startPos;
    public Transform backPos;

    [Header("Parameters")]
    public float rayLength;
    public float lookbackRayLength;
    [Range(0,10)]
    public float speed;

    private Rigidbody2D rb;
    private bool isFollowing;
    private bool isSearching;
    private int direction;


    private void Start()
    {
        isFollowing = false;
        isSearching = false;
        rb = GetComponent<Rigidbody2D>();
        direction = 1;
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
                    isSearching = true;
                    Invoke("StopFollowingPlayer", 3);
                }
            }      
        }

        if(isFollowing)
        {
            FollowPlayer();
        }

        LookBack();
    }

  

    public bool CanSeePlayer()
    {
        Vector2 endPos = startPos.position + Vector3.right * rayLength * direction;
        RaycastHit2D hit = Physics2D.Linecast(startPos.position, endPos);

        Debug.DrawLine(startPos.position, endPos, Color.red);

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
        Vector2 endPos = backPos.position + Vector3.left * lookbackRayLength * direction;
        RaycastHit2D lookBack = Physics2D.Linecast(backPos.position, endPos);

        Debug.DrawLine(backPos.position, backPos.position + Vector3.left * lookbackRayLength * direction, Color.blue);

        if(lookBack.collider != null && lookBack.collider.gameObject.CompareTag("Player"))
        {
            // Ak je nalavo od hraca
            if (transform.position.x < player.position.x)
            {
                FlipPlayer(1);
            }

            // Ak je napravo od hraca
            if (transform.position.x > player.position.x)
            {
                FlipPlayer(-1);
            }
        }

    }

    public void FollowPlayer()
    {
        // Ak je nalavo od hraca
        if(transform.position.x < player.position.x)
        {
            FlipPlayer(1);
            rb.velocity = new Vector2(speed, 0);
        }

        // Ak je napravo od hraca
        if(transform.position.x > player.position.x)
        {
            FlipPlayer(-1);
            rb.velocity = new Vector2(-speed, 0);
        }
    }

    public void StopFollowingPlayer()
    {
        isFollowing = false;
        isSearching = false;
        rb.velocity = Vector2.zero;
    }

    public void FlipPlayer(int dir)
    {
        direction = dir;
        transform.localScale = new Vector2(dir, 1);
    }
    
}
