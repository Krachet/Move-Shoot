using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieState : iState<Player>
{
    public void OnEnter(Player player)
    {
        player.attackRange.enabled = false;
        GameplayManager.Instance.Lose();
    }
    public void OnExecute(Player player)
    {

    }
    public void OnExit(Player player)
    {

    }
}
