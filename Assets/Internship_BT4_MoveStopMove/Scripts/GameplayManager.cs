using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : Singleton<GameplayManager>
{
    [SerializeField] GameObject JoyStick;
    [SerializeField] GameObject aliveNumber;
    [SerializeField] GameObject nameplates;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject shop;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject level;
    [SerializeField] GameObject deathScreen;
    [SerializeField] Player player;

    public GameObject[] weapons;
    public GameObject[] weaponsThrow;
    public GameObject[] heads;
    public Material[] pants;
    public GameObject[] pantsUI;
    public int total_weapon => weapons.Length;
    public int total_head => heads.Length;
    public int total_pant => pantsUI.Length;
    // Start is called before the first frame update
    void Awake()
    {
        if (!PlayerPrefs.HasKey("Weapons"))
        {
            PlayerPrefs.SetInt("Weapons", 0);
        }
        else
        {
            PlayerPrefs.GetInt("Weapons");
        }

        if (!PlayerPrefs.HasKey("Scene"))
        {
            PlayerPrefs.SetInt("Scene", 0);
        }
        else
        {
            PlayerPrefs.GetInt("Scene");
        }

        if (!PlayerPrefs.HasKey("Heads"))
        {
            PlayerPrefs.SetInt("Heads", 0);
        }
        else
        {
            PlayerPrefs.GetInt("Heads");
        }

        if (!PlayerPrefs.HasKey("Pants"))
        {
            PlayerPrefs.SetInt("Pants", 0);
        }
        else
        {
            PlayerPrefs.GetInt("Pants");
        }

        if (!PlayerPrefs.HasKey("Scene"))
        {
            PlayerPrefs.SetInt("Scene", 0);
        }
        else
        {
            PlayerPrefs.GetInt("Scene");
        }
    }

    public GameObject GetCurrentWeapon(int index)
    {
        return weapons[index];
    }

    public Bullet GetCurrentThrowWeapon(int index) {return weaponsThrow[index].GetComponent<Bullet>();}

    public GameObject GetCurrentHead(int index) { return heads[index]; }

    public Material GetCurrentPant(int index) { return pants[index]; }

    public GameObject GetCurrentPantUI(int index) { return pantsUI[index]; }

    public void OnInit()
    {
        JoyStick.SetActive(false);
        aliveNumber.SetActive(false);
        mainMenu.SetActive(true);
        shop.SetActive(false);
        inventory.SetActive(false);
        deathScreen.SetActive(false);
    }

    public void PlayGame()
    {
        JoyStick.SetActive(true);
        aliveNumber.SetActive(true);
        nameplates.SetActive(true);
        mainMenu.SetActive(false);
        deathScreen.SetActive(false);
    }

    public void GameMenu()
    {
        player.ChangeState(new idleState());
        JoyStick.gameObject.SetActive(false);
        aliveNumber.SetActive(false);
        mainMenu.SetActive(true);
        nameplates.SetActive(false);
        inventory.SetActive(false);
        shop.SetActive(false);
        deathScreen.SetActive(false);
        level.SetActive(false);
    }

    public void ChangeWeapon()
    {
        JoyStick.gameObject.SetActive(false);
        aliveNumber.SetActive(false);
        mainMenu.SetActive(false);
        nameplates.SetActive(false);
        inventory.SetActive(true);
        shop.SetActive(false);
    }

    public void OpenShop()
    {
        JoyStick.gameObject.SetActive(false);
        aliveNumber.SetActive(false);
        mainMenu.SetActive(false);
        nameplates.SetActive(false);
        inventory.SetActive(false);
        shop.SetActive(true);
    }

    public void Lose()
    {
        deathScreen.SetActive(true);
    }

    public void LevelSelect()
    {
        level.SetActive(true);
        JoyStick.gameObject.SetActive(false);
        aliveNumber.SetActive(false);
        mainMenu.SetActive(false);
        nameplates.SetActive(false);
        inventory.SetActive(false);
        shop.SetActive(false);
    }
}
