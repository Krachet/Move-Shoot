using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] Button next;
    [SerializeField] Button previous;
    [SerializeField] Button Headnext;
    [SerializeField] Button Headprevious;
    [SerializeField] Button Pantnext;
    [SerializeField] Button Pantprevious;
    [SerializeField] Button equipped;
    [SerializeField] Button equippedHead;
    [SerializeField] Button equippedPant;
    [SerializeField] Button pick;
    [SerializeField] Button pickHead;
    [SerializeField] Button pickPant;
    [SerializeField] Button back;
    [SerializeField] Transform WeaponParent;
    [SerializeField] Transform HeadParent;
    [SerializeField] Transform PantParent;
    [SerializeField] GameObject player;

    [SerializeField] GameObject PanelSkins;
    [SerializeField] GameObject PanelWeapons;

    [SerializeField] Button Skins;
    [SerializeField] Button Weapons;

    private int total_weapon = 0;
    private int weapon_index=0;
    private GameObject weapon;

    private int total_head = 0;
    private int head_index = 0;
    private GameObject head;

    private int total_pant = 0;
    private int pant_index = 0;
    private GameObject pant;

    // Start is called before the first frame update
    void Start()
    {
        weapon_index = PlayerPrefs.GetInt("Weapons");
        total_weapon = GameplayManager.Instance.total_weapon;
        head_index = PlayerPrefs.GetInt("Heads");
        total_head = GameplayManager.Instance.total_head;
        PanelWeaponsActive();
        InitWeapon(weapon_index);
        next.onClick.AddListener(() => NextWeapon());
        previous.onClick.AddListener(() => PreviousWeapon());
        pick.onClick.AddListener(() => ChangeWeapon());  
        pickHead.onClick.AddListener(() => ChangeHead());
        pickPant.onClick.AddListener(() => ChangePant());
        back.onClick.AddListener(() => GameplayManager.Instance.GameMenu());
        Skins.onClick.AddListener(() => PanelSkinActive());
        Weapons.onClick.AddListener(() => PanelWeaponsActive());
        Headnext.onClick.AddListener(() => NextHead());
        Headprevious.onClick.AddListener(() => PreviousHead());
        Pantnext.onClick.AddListener(() => NextPant());
        Pantprevious.onClick.AddListener(() => PreviousPant());
    }

    private void ChangeWeapon()
    {
        PlayerPrefs.SetInt("Weapons", weapon_index);
        player.GetComponent<Character>().ChangeWeapon(weapon_index);
        InitWeapon(weapon_index);
    }

    private void ChangeHead()
    {
        PlayerPrefs.SetInt("Heads", head_index);
        player.GetComponent<Character>().ChangeHead(head_index);
        InitHead(head_index);
    }

    private void ChangePant()
    {
        PlayerPrefs.SetInt("Pants", pant_index);
        player.GetComponent<Character>().ChangePant(pant_index);
        InitPant(pant_index);
    }

    private void InitWeapon(int index)
    {
        weapon_index = index;
        foreach (Transform child in WeaponParent)
        {
            Destroy(child.gameObject);
        }
        if (index == PlayerPrefs.GetInt("Weapons"))
        {
            equipped.gameObject.SetActive(true);
            pick.gameObject.SetActive(false);
        }
        else
        {
            equipped.gameObject.SetActive(false);
            pick.gameObject.SetActive(true);
        }
        weapon = Instantiate(GameplayManager.Instance.GetCurrentWeapon(weapon_index), WeaponParent.position, Quaternion.identity, WeaponParent);
        weapon.transform.localPosition = new Vector3(0, -0, 0);
        weapon.transform.localRotation = Quaternion.Euler(0, -0, 0);
        weapon.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void InitHead(int index)
    {
        head_index = index;
        foreach (Transform child in HeadParent)
        {
            Destroy(child.gameObject);
        }
        if (index == PlayerPrefs.GetInt("Heads"))
        {
            equippedHead.gameObject.SetActive(true);
            pick.gameObject.SetActive(false);
        }
        else
        {
            equippedHead.gameObject.SetActive(false);
            pick.gameObject.SetActive(true);
        }
        head = Instantiate(GameplayManager.Instance.GetCurrentHead(head_index), HeadParent.position, Quaternion.identity, HeadParent);
        head.transform.localPosition = new Vector3(0, -0, 0);
        head.transform.localRotation = Quaternion.Euler(0, -0, 0);
        head.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    private void InitPant(int index)
    {
        pant_index = index;
        foreach (Transform child in PantParent)
        {
            Destroy(child.gameObject);
        }
        if (index == PlayerPrefs.GetInt("Pants"))
        {
            equippedPant.gameObject.SetActive(true);
            pick.gameObject.SetActive(false);
        }
        else
        {
            equippedPant.gameObject.SetActive(false);
            pick.gameObject.SetActive(true);
        }
        pant = Instantiate(GameplayManager.Instance.GetCurrentPantUI(pant_index), PantParent.position, Quaternion.identity, PantParent);
        pant.transform.localPosition = new Vector3(0, -0, 0);
        pant.transform.localRotation = Quaternion.Euler(0, -0, 0);
        pant.transform.localScale = new Vector3(1, 1, 1);
    }

    private void NextWeapon()
    {
        weapon_index++;
        if (weapon_index == total_weapon)
        {
            weapon_index = 0;
        }
        InitWeapon(weapon_index);
    }

    private void PreviousWeapon()
    {
        weapon_index--;
        if (weapon_index < 0)
        {
            weapon_index = total_weapon - 1;
        }
        InitWeapon(weapon_index);
    }

    private void NextHead()
    {
        head_index++;
        if (head_index == total_head)
        {
            head_index = 0;
        }
        InitHead(head_index);
    }

    private void PreviousHead()
    {
        head_index--;
        if (head_index < 0)
        {
            head_index = total_head - 1;
        }
        InitHead(head_index);
    }

    private void NextPant()
    {
        pant_index++;
        if (pant_index == total_pant)
        {
            pant_index = 0;
        }
        InitPant(pant_index);
    }

    private void PreviousPant()
    {
        pant_index--;
        if (pant_index < 0)
        {
            pant_index = total_pant - 1;
        }
        InitPant(pant_index);
    }

    private void PanelSkinActive()
    {
        PanelSkins.SetActive(true);
        PanelWeapons.SetActive(false);
        pickHead.gameObject.SetActive(true);
        pickPant.gameObject.SetActive(true);
        pick.gameObject.SetActive(false);
        equipped.gameObject.SetActive(false);
    }

    private void PanelWeaponsActive()
    {
        PanelWeapons.SetActive(true);
        PanelSkins.SetActive(false);
        pick.gameObject.SetActive(true);
        pickHead.gameObject.SetActive(false);
        pickPant.gameObject.SetActive(false);
        equippedHead.gameObject.SetActive(false);
        equippedPant.gameObject.SetActive(false);
    }
}
