using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState<Enemy>
{
    public void OnEnter(Enemy enemy)
    {
        enemy.Counter.Exit();
        enemy.counterOther.Exit();
    }
    public void OnExecute(Enemy enemy)
    {
        EasyObjectPool.instance.ReturnObjectToPool(enemy.gameObject);
        enemy.gameObject.SetActive(false);
    }
    public void OnExit(Enemy enemy)
    {

    }
}
