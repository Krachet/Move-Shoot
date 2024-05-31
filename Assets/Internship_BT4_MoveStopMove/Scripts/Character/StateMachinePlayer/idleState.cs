using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleState : iState<Player>
{
    public CounterTime counter = new CounterTime();
    public void OnEnter(Player player)
    {
        player.attackRange.gameObject.SetActive(true);
        player.ChangeAnimState(Character.AnimState.Idle);
        counter.Start(() => player.ChangeState(new attackState()), 0.4f);
    }
    public void OnExecute(Player player)
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.ChangeState(new runState());
        }
        if (player.enemiesInRange.Count > 0)
        {
            counter.Execute();
        }
    }
    public void OnExit(Player player)
    {

    }
}
