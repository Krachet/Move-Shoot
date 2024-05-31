using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startGame;
    public Button changeWeapon;
    public Button shop;
    public Button level;

    private int weapon_index;
    // Start is called before the first frame update
    void Start()
    {
        startGame.onClick.AddListener(() => StartGame());
        changeWeapon.onClick.AddListener(() => ChangeWeapon());
        shop.onClick.AddListener(() => OpenShop());
        level.onClick.AddListener(() => LevelSelector());
    }


    private void StartGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Scene"));
        //GameController.Instance.OnInit();
    }

    private void ChangeWeapon()
    {
        GameplayManager.Instance.ChangeWeapon();
    }

    private void OpenShop()
    {
        GameplayManager.Instance.OpenShop();
    }

    private void LevelSelector()
    {
        GameplayManager.Instance.LevelSelect();
    }
}
