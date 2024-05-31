using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    private float t;
    
    iState<Player> currentState;

    public void Start()
    {
        attackRange.gameObject.SetActive(false);
        ChangeWeapon(PlayerPrefs.GetInt("Weapons"));
        ChangeHead(PlayerPrefs.GetInt("Heads"));
        ChangePant(PlayerPrefs.GetInt("Pants"));
    }
    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        if (GameController.Instance.totalCharAlive == 1)
        {
            GameController.Instance.RemoveEnemy();
            ChangeState(new winState());
            isDead = true;
            return;
        }
    }

    public void ChangeState(iState<Player> newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new idleState());
        ChangeWeapon(PlayerPrefs.GetInt("Weapons"));
        ChangeHead(PlayerPrefs.GetInt("Heads"));
        ChangePant(PlayerPrefs.GetInt("Pants"));
        enemiesInRange.Clear();
        targetIndicators = GameController.Instance.createIndicators(nameplates.transform);
    }

    public override void IncreaseSize()
    {
        base.IncreaseSize();
        t += 0.1f;
        CameraFollows.Instance.SetOffset(t);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        attackRange.gameObject.SetActive(false);
        ChangeState(new dieState());
    }
}


