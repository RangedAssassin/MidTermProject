using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected EnemyController _enemy;

    //constructor

    public EnemyState(EnemyController enemy)
    {
        this._enemy = enemy;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();


}
