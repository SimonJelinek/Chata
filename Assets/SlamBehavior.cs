using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamBehavior : StateMachineBehaviour
{
    public GameObject _particles;
    private Transform _playerPos;

    private float _timer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_timer >= 0.4f)
        {
            SpawnBox();
            _timer = 0f;
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Slam");
    }

    private void SpawnBox()
    {
        Instantiate(_particles, new Vector3(_playerPos.position.x, _playerPos.position.y + 5, 0), Quaternion.identity);
    }

}
