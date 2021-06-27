using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningBehavior : StateMachineBehaviour
{ 
    public GameObject _enemy;
    private Transform _bossPos;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bossPos = GameObject.FindGameObjectWithTag("Boss").GetComponent<Transform>();
        Instantiate(_enemy, new Vector3(_bossPos.position.x, _bossPos.position.y + 3, 0), Quaternion.identity);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Spawning");
    }
    
}
