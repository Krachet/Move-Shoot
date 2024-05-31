using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skins : MonoBehaviour
{
    [SerializeField] Transform hand;
    [SerializeField] Transform head;
    [SerializeField] Transform pant;
    [SerializeField] GameObject currentPant;

    GameObject currentHead;
    GameObject currentWeapon;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject ChangeWeapon(int index)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        return currentWeapon = Instantiate(GameplayManager.Instance.GetCurrentWeapon(index), hand);
    }

    public GameObject ChangeHead(int index)
    {
        if (currentHead != null)
        {
            Destroy(currentHead.gameObject);
        }
        currentHead = Instantiate(GameplayManager.Instance.GetCurrentHead(index), head);
        return currentHead;
    }

    public GameObject ChangePant(int index)
    {
        if (currentPant != null)
        {
            Destroy(currentPant.gameObject);
        }
        currentPant = Instantiate(currentPant, pant);
        currentPant.GetComponent<Renderer>().material = GameplayManager.Instance.GetCurrentPant(index);
        return currentPant;
    }
}
