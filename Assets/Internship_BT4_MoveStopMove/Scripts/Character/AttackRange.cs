using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public Character character;
    public Transform parent;

    private void Start()
    {
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), parent.GetComponent<Collider>());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            character.AddEnemiesInRange(other.GetComponent<Character>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            character.enemiesInRange.Remove(other.GetComponent<Character>());
        }
    }
}
