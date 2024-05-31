using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackState : iState<Player>
{
    private CounterTime counter = new CounterTime();
    public void OnEnter(Player player)
    {
        player.ThrowWeapons();
        counter.Start(() => player.ChangeState(new idleState()), 1f);
    }
    public void OnExecute(Player player)
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.ChangeState(new runState());
        }
        else
        {
            counter.Execute();
        }
    }
    public void OnExit(Player player)
    {

    }
}
