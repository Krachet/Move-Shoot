using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : CharacterController
{
    public LayerMask ground;
    public Transform body;

    public Animator Animator;

    public bool isInRange = false;

    public List<Character> enemiesInRange = new List<Character>();
    public Character enemy;

    public Transform throwPos;
    public GameObject bulletPrefabs;
    public GameObject currentWeapon;

    public int ThrowTime = 1;

    public CounterTime counter = new CounterTime();
    public CounterTime counterOther = new CounterTime();
    private CounterTime attackCounter = new CounterTime();

    public float size = 1;
    public int score = 0;
    protected TargetIndicators targetIndicators;
    public GameObject nameplates;

    public Skins skin;

    public bool isDead;
    public bool isIdle;

    public BoxCollider box;
    public Bullet currentBullet;
    public GameObject currentHead;
    public GameObject currentPant;

    public Transform head;
    public float speed;
    public AttackRange attackRange;

    public GameObject shield;

    public string[] weaponPool = { "Arrow", "Axe", "CandyCane", "Hammer", "IceCream", "Knife", "Lolipop" };
    private string bulletname;

    public enum AnimState
    {
        Idle,
        Dead,
        Running,
        Win,
        Attack,
        Dance,
        Dance_win,
        Ultimate
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void ChangeAnimState(AnimState state)
    {
        switch (state)
        {
            case AnimState.Idle:
                Animator.SetBool("Idle", true);
                Animator.SetBool("Dead", false);
                Animator.SetBool("Running", false);
                Animator.SetBool("Win", false);
                Animator.SetBool("canAttack", false);
                Animator.SetBool("Dance", false);
                Animator.SetBool("Ultimate", false);
                break;

            case AnimState.Running:
                Animator.SetBool("Running", true);
                Animator.SetBool("Idle", false);
                Animator.SetBool("Dead", false);
                Animator.SetBool("Win", false);
                Animator.SetBool("canAttack", false);
                Animator.SetBool("Ultimate", false);  
                break;

            case AnimState.Attack:
                Animator.SetBool("canAttack", true);
                Animator.SetBool("Idle", true);
                Animator.SetBool("Dead", false);
                Animator.SetBool("Win", false);
                Animator.SetBool("Running", false);
                Animator.SetBool("Ultimate", false);
                Animator.SetTrigger("Attack");
                break;

            case AnimState.Dance:
                Animator.SetBool("Dance", true);
                Animator.SetBool("Idle", false);
                Animator.SetBool("Dead", false);
                Animator.SetBool("Win", false);
                Animator.SetBool("Running", false);
                Animator.SetBool("canAttack", false);
                Animator.SetBool("Ultimate", false);
                break;

            case AnimState.Ultimate:
                Animator.SetBool("Ultimate", true);
                Animator.SetBool("Idle", true);
                Animator.SetBool("canAttack", true);
                Animator.SetBool("Dead", false);
                Animator.SetBool("Running", false);
                Animator.SetBool("Win", false);
                break;

            case AnimState.Dance_win:
                Animator.SetBool("Win", true);
                Animator.SetBool("Dance", true);
                Animator.SetBool("Dead", false);
                Animator.SetBool("Running", false);
                break;

            case AnimState.Win:
                Animator.SetBool("Win", true);
                Animator.SetBool("Dance", false);
                break;

            case AnimState.Dead:
                Animator.SetBool("Dead", true);
                break;
        }   
    }

    public void ChangeWeapon(int index)
    {
        currentWeapon = skin.ChangeWeapon(index); 
        currentBullet = GameplayManager.Instance.GetCurrentThrowWeapon(index);
        bulletname = weaponPool[index];
    }

    public void ChangeHead(int index)
    {
        currentHead = skin.ChangeHead(index);
        head.transform.position = Vector3.zero;
        currentHead.transform.SetParent(head);
        currentHead.transform.position = new Vector3(0, 0f, 0);
    }

    public void ChangePant(int index)
    {
        currentPant = skin.ChangePant(index);
    }

    public bool CanMove(Vector3 point)
    {
        bool canmove = false;
        if (Physics.Raycast(point + Vector3.up, Vector3.down, 5f, ground))
        {
            canmove = true;
        }
        return canmove;
    }

    public void AddEnemiesInRange(Character enemy)
    {
        enemiesInRange.Add(enemy);
        if (enemiesInRange.Count != 0)
        {
            isInRange = true;
        }
    }

    public Character GetCharInRange()
    {
        if (enemiesInRange.Count > 0f)
        {
            return enemiesInRange[Random.Range(0, enemiesInRange.Count)];
        }
        return null;
    }
    public void ThrowWeapons()
    {
        if (enemiesInRange.Count < 1)
        {
            return;
        }
        else
        {
            attackCounter.Start(() => ChangeAnimState(AnimState.Idle), 1.4f);
            body.LookAt(GetCharInRange().transform);
            ChangeAnimState(AnimState.Attack);
            Debug.Log(bulletname);
            //Bullet bullet = Instantiate(currentBullet, transform.position, body.rotation).GetComponent<Bullet>();
            GameObject bulletObj = EasyObjectPool.instance.GetObjectFromPool(bulletname, transform.position, body.rotation);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            bullet.gameObject.SetActive(true);
            bullet.transform.localScale = new Vector3(size, size, size);
            bullet.OnInit(this, GetCharInRange().transform);
            attackCounter.Execute();          
        }
    }

    private void IncreaseRange(Character character)
    {
        character.attackRange.transform.localScale += Vector3.one;
    }

    public override void IncreaseSize()
    {
        gameObject.transform.localScale += Vector3.one * 0.2f;
        score++;
        targetIndicators.SetScore(score);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Speed"))
        {
            Destroy(other.gameObject);
            speed += speed * 0.3f;
            Invoke(nameof(ResetStats), 3);
        }
        if (other.CompareTag("Sight"))
        {
            Destroy(other.gameObject);
            IncreaseRange(gameObject.GetComponent<Character>());
            StartCoroutine(ResetStats(gameObject.GetComponent<Character>()));
        }
        if (other.CompareTag("Shield"))
        {
            Destroy(other.gameObject);
            GameObject barrier = Instantiate(shield, gameObject.transform.position, Quaternion.identity);
            barrier.transform.SetParent(gameObject.transform);
            barrier.transform.localPosition = new Vector3(0.04f, 0.75f, 0.1f);
        }
    }

    public IEnumerator ResetStats(Character character)
    {
        yield return new WaitForSeconds(6);
        character.speed = 5;
        character.attackRange.transform.localScale = Vector3.one * 3;
    }

    private void SpawnOrbs()
    {
        GameController.Instance.SpawnOrbs();
    }

    public override void OnAttack()
    {

    }

    public override void OnDespawn()
    {

    }

    public override void OnInit()
    {
        speed = 5;
        isDead = false;
        size = 1;
        box.gameObject.SetActive(true);
    }

    public override void OnStopMove()
    {

    }

    public override void OnDeath()
    {
        ChangeAnimState(AnimState.Dead);
        box.enabled = false;
        targetIndicators.gameObject.SetActive(false);
        GameController.Instance.AdjustCharAlive();
        isDead = true;
    }
}
