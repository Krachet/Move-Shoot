using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public Character ThrowChar;
    public Player player;
    public Transform target;
    public Transform child;
    public float speed = 100f;


    CounterTime counter = new CounterTime();
    private void Start()
    {
        
    }

    public void OnInit(Character character, Transform target)
    {
        this.ThrowChar = character;
        this.target = target;
        Physics.IgnoreCollision(this.ThrowChar.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        transform.forward = (target.position - transform.position).normalized;
        transform.position += Vector3.up;
        counter.Start(DeActiveBullet, ThrowChar.ThrowTime);
    }
    private void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        child.Rotate(Vector3.up * -6, Space.Self);
        counter.Execute();
    }

    

    private void DeActiveBullet()
    {
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.gameObject != this.ThrowChar.gameObject)
            {
                SoundManager.Instance.PlayOneShot(0);
                other.GetComponent<Character>().OnDeath();
                ThrowChar.enemiesInRange.Remove(other.gameObject.GetComponent<Character>());
                this.ThrowChar.IncreaseSize();
                this.ThrowChar.ThrowTime += 1;
                this.ThrowChar.size += 0.2f;
                GameController.Instance.SpawnEnemy();
            }
        }
    }

    IEnumerator RespawnEnemy()
    {
        yield return new WaitForSeconds(1f);
        GameController.Instance.SpawnEnemy();
    }
}
