using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningBehavior : StateMachineBehaviour
{
    public float _timer;
    private int _rand;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rand = Random.Range(0, 2);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_timer <= 0)
        {
            if (_rand == 0)
            {
                animator.SetTrigger("Slam");
            }
            else
            {
                animator.SetTrigger("Idle");
            }

        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
