using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<Enemy>
{
    void OnEnter(Enemy t);
    void OnExecute(Enemy t);
    void OnExit(Enemy t);
}
