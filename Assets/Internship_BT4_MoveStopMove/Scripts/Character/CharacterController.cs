using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public abstract void OnInit();
    public abstract void OnDespawn();
    public abstract void OnAttack();
    public abstract void OnStopMove();
    public abstract void IncreaseSize();
    public abstract void OnDeath();

    // Update is called once per frame
    void Update()
    {
        
    }
}
