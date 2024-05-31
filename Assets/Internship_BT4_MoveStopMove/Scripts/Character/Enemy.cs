using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public NavMeshAgent agent;
    public Vector3 destination;

    public bool isAtDestination => (Mathf.Abs(destination.x - transform.position.x) + Mathf.Abs(destination.z - transform.position.z)) < 0.3f;
    public CounterTime Counter => counter;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        destination = transform.position;
        ChangeState(new PatrolState());
    }

    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destination = position;
        agent.SetDestination(destination);
    }

    IState<Enemy> currentState;
    private void Update()
    {
        agent.speed = speed;
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        int total_weapon = GameplayManager.Instance.total_weapon;
        ChangeWeapon(Random.Range(0, total_weapon));
        targetIndicators = GameController.Instance.createIndicators(nameplates.transform);
    }

    public void ChangeState(IState<Enemy> newState)
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

    public override void IncreaseSize()
    {
        base.IncreaseSize(); 
    }

    public override void OnDeath()
    {
        base.OnDeath();
        ChangeState(null);
        agent.enabled = false;
        Invoke(nameof(OnDisable), 2f);
    }

    private void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
