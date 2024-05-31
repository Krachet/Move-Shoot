using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Enemy>
{
    public GameObject weaponPrefab;
    private int rand;
    //GO TO DESTINATION
    public void OnEnter(Enemy t)
    {
        t.StartCoroutine(delayAttack());
        t.agent.enabled = false;
        t.ThrowWeapons();
        t.currentWeapon.GetComponent<Weapons>().Throw();
        t.Counter.Start(() => t.ChangeState(new IdleState()), 1f);
    }

    //CHECK IF CAN GO TO DESTINATION
    public void OnExecute(Enemy t)
    {
        t.counter.Execute();
    }

    public void OnExit(Enemy t)
    {
            
    }

    IEnumerator delayAttack()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
