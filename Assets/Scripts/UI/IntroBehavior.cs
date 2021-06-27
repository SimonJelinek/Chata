using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBehavior : StateMachineBehaviour
{
    private int _rand;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rand = Random.Range(0, 3);

        if (_rand == 0)
        {
            animator.SetTrigger("Spawning");
        }
        else if(_rand == 1)
        {
            animator.SetTrigger("Idle");
        }
        else
        {
            animator.SetTrigger("Slam");
        }
    } 
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }


}
