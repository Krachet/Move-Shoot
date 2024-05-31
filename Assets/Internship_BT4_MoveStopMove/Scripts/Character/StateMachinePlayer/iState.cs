using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iState<Player>
{
    void OnEnter(Player player);
    void OnExecute(Player player);
    void OnExit(Player player);
}
