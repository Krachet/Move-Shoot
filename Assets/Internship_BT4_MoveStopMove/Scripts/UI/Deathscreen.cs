using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Deathscreen : MonoBehaviour
{
    [SerializeField] Button retry;
    [SerializeField] Button returnToMenu;
    // Start is called before the first frame update
    void Start()
    {
        retry.onClick.AddListener(() => Retry());
        returnToMenu.onClick.AddListener(() => ReturnToMenu());
    }

    private void Retry()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Scene"));
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
