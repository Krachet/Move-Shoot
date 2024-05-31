using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winState : iState<Player>
{
    public void OnEnter(Player player)
    {
        player.ChangeAnimState(Character.AnimState.Dance_win);
        GameController.Instance.nextScene++;
    }
    public void OnExecute(Player player)
    {
        player.StartCoroutine(NextLevel(player));
    }
    public void OnExit(Player player)
    {

    }

    private IEnumerator NextLevel(Player player)
    {
        yield return new WaitForSeconds(3);
        PlayerPrefs.SetInt("Scene", PlayerPrefs.GetInt("Scene") + 1);
        GameController.Instance.ChangeScene(PlayerPrefs.GetInt("Scene"));
    }
}

