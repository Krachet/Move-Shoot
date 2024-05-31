using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {
        t.ChangeAnimState(Character.AnimState.Idle);
    }

    public void OnExecute(Enemy t)
    {
        if (t.enemiesInRange.Count < 1)
        {
            t.ChangeState(new PatrolState());
        }
        if (t.enemiesInRange.Count > 0)
        {
            t.ChangeState(new AttackState());
        }
    }

    public void OnExit(Enemy t)
    {

    }

}
