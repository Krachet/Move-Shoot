using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runState : iState<Player> 
{
    public void OnEnter(Player player)
    {

    }
    public void OnExecute(Player player)
    {
        if (Input.GetMouseButton(0) && JoystickControl.direct != Vector3.zero)
        {
            Vector3 nextpos = player.transform.position + JoystickControl.direct * player.speed * Time.deltaTime;
            if (player.CanMove(nextpos))
            {
                player.transform.position += JoystickControl.direct * Time.deltaTime * 5f;
            }
            player.ChangeAnimState(Character.AnimState.Running);
            player.body.forward = JoystickControl.direct;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            if (player.enemiesInRange.Count > 0)
            {
                player.ChangeState(new attackState());
            }
            else
            {
                player.ChangeState(new idleState());
            }
        }
    }
    public void OnExit(Player player)
    {

    }
}
