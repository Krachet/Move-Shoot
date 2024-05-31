using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    [SerializeField] Button[] levels;
    [SerializeField] Button home;
    // Start is called before the first frame update
    void Start()
    {
        levels[0].onClick.AddListener(() => ChooseLevel(1));
        levels[1].onClick.AddListener(() => ChooseLevel(2));
        levels[2].onClick.AddListener(() => ChooseLevel(3));
        levels[3].onClick.AddListener(() => ChooseLevel(4));
        levels[4].onClick.AddListener(() => ChooseLevel(5));
        levels[5].onClick.AddListener(() => ChooseLevel(6));
        home.onClick.AddListener(() => GameplayManager.Instance.GameMenu());
    }
    
    private void ChooseLevel(int index)
    {
        PlayerPrefs.SetInt("Scene", index);
        SceneManager.LoadScene(index);
    }

}
