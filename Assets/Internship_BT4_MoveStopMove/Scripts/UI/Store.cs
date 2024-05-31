using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField] Button next;
    [SerializeField] Button previous;
    [SerializeField] Button back;
    [SerializeField] Transform parent;
    [SerializeField] GameObject itemlock;

    private bool islocked;
    private int total_weapon = 0;
    private int weapon_index = 0;
    private GameObject weapon;


    // Start is called before the first frame update
    void Start()
    {
        weapon_index = PlayerPrefs.GetInt("Weapons");
        total_weapon = GameplayManager.Instance.total_weapon;
        InitWeapon(weapon_index);
        next.onClick.AddListener(() => NextWeapon());
        previous.onClick.AddListener(() => PreviousWeapon());
        back.onClick.AddListener(() => GameplayManager.Instance.GameMenu());
    }

    private void InitWeapon(int index)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
        weapon = Instantiate(GameplayManager.Instance.GetCurrentWeapon(weapon_index), parent.position, Quaternion.identity, parent);
        weapon.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
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
}
