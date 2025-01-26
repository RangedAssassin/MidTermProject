using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : EnemyState
{
    float _distanceToPlayer;

    public EnemyFollowState(EnemyController enemy) : base(enemy)
    {

    }
    public override void OnStateEnter()
    {
        Debug.Log("Enemy is following Player");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy stopped following player");
    }

    public override void OnStateUpdate()
    {
        if (_enemy._player != null)
        {

            _distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy._player.position);

            if (_distanceToPlayer > 10)
            {
                // Go back to idle
                _enemy.ChangeState(new EnemyIdleState(_enemy));
            }

            //Go to Attack mode
            if (_distanceToPlayer < 2)
            {
                _enemy.ChangeState(new EnemyAttackState(_enemy));
            }


            _enemy._agent.destination = _enemy._player.position;
        }
        else
        {
            // Go back to idle
            _enemy.ChangeState(new EnemyIdleState(_enemy));
        }
    }
}

