using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve_FollowingEnemy : MonoBehaviour
{
    public EnemyFollowAI_NEW followingEnemy;
    public SpriteRenderer[] childrenSr;

    private float timer;

    private void Start()
    {
        childrenSr = GetComponentsInChildren<SpriteRenderer>();
        timer = 0.75f;
    }
    private void Update()
    {
        if(followingEnemy.health <= 0)
        {

            foreach(SpriteRenderer bodyPart in childrenSr)
            {
                bodyPart.material.SetFloat("Fade", timer / 0.75f);
            }

            timer -= Time.deltaTime;
        }
    }
    
}
