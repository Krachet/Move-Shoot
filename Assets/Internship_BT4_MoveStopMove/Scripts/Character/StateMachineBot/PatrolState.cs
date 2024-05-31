using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Enemy>
{
    private int rand;    
    public void OnEnter(Enemy t)
    {
        t.agent.enabled = true;
        t.ChangeAnimState(Enemy.AnimState.Running);
        SeekTarget(t);
    }

    public void OnExecute(Enemy t)
    {
        if (t.enemiesInRange.Count > 0)
        {
            t.ChangeState(new IdleState());
        }
        else if (t.isAtDestination)
        {
            t.ChangeState(new IdleState());
        }
    }

    public void OnExit(Enemy t)
    {

    }

    private void SeekTarget(Enemy t)
    {
        t.destination = new Vector3(Random.Range(-24f, 24f), 1f, Random.Range(-24f, 24f));
        t.SetDestination(t.destination);
    }
}
