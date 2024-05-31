using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ColorType
{
    Default,
    Black,
    Red,
    Blue,
    Yellow,
    Orange,
    Brown,
    Green,
    Violet
}

public class GameController : Singleton<GameController>
{
    readonly List<ColorType> colorTypes = new List<ColorType>() { ColorType.Black, ColorType.Blue, ColorType.Brown, ColorType.Green, ColorType.Orange, ColorType.Red, ColorType.Violet, ColorType.Yellow };

    public List<Enemy> enemyList = new List<Enemy>();
    [SerializeField] private GameObject enemyPrefab;
    public int CharacterAmount => enemyAmount + 1;
    public int enemyAmount;
    public int totalEnemySpawn;

    public Transform[] startPoint;
    public List<Vector3> emptyPos = new List<Vector3>();
    public Player player;

    [SerializeField] GameObject indicator;
    [SerializeField] GameObject nameplatePos;
    [SerializeField] TextMeshProUGUI aliveNum;

    [SerializeField] GameObject[] powerups;

    public int nextScene;

    public int totalCharAlive;

    private static string[] game_name = {
    "ShadowBlade", "SonicStorm", "LunarPhoenix", "IronHawk", "MysticWraith",
    "ThunderFury", "RapidViper", "AstralJester", "SilverWisp", "EclipseRogue",
    "InfernoSpecter", "NeonSorcerer", "VortexStriker", "VenomPulse", "BlazeWyrm",
    "SpectralRaven", "FrostQuasar", "NovaCipher", "CyberPulse", "PhoenixRider"
};

    private void Start()
    {
        totalCharAlive = CharacterAmount + 10;
        nextScene = 0;
        OnInit();
    }   

    public void OnInit()
    {
        player = FindAnyObjectByType<Player>();
        CameraFollows.Instance.OnInit();

        totalEnemySpawn = 0;

        aliveNum.text = "Alive: " + totalCharAlive.ToString();
        for (int i = 0; i < startPoint.Length; i++)
        {
            emptyPos.Add(startPoint[i].position);
        }

        if (emptyPos.Count == 0)
        {
            return;
        }
        Debug.Log(emptyPos.Count);
        int ran = Random.Range(0, emptyPos.Count);
        player.transform.position = emptyPos[ran];
        player.OnInit();
        emptyPos.RemoveAt(ran);

        //bot
        for (int i = 0; i < CharacterAmount - 1; i++)
        {
            ran = Random.Range(0, emptyPos.Count);
            //Enemy enemy = Instantiate(enemyPrefab, emptyPos[ran], Quaternion.identity).GetComponent<Enemy>();
            Enemy enemy = EasyObjectPool.instance.GetObjectFromPool("Enemy", emptyPos[ran], Quaternion.identity).GetComponent<Enemy>();
            enemyList.Add(enemy);
            enemy.ChangeState(new PatrolState());
            emptyPos.RemoveAt(ran);
        }

        for (int j = 0; j < 3; j++)
        {
            ran = Random.Range(0, emptyPos.Count);
            GameObject powerup = Instantiate(powerups[Random.Range(0, 3)], emptyPos[ran], Quaternion.identity);
            emptyPos.RemoveAt(ran);
            powerup.transform.position += new Vector3(0, 2, 0);
        }
    }

    private void OnDespawn()
    {

    }

    public void ChangeScene(int index)
    {
        nextScene++;
        PlayerPrefs.SetInt("Scene", nextScene);
        SceneManager.LoadScene(index);
    }

    public void AdjustCharAlive()
    {
        totalCharAlive--;
        aliveNum.text = "Alive: " + totalCharAlive.ToString();
    }

    public TargetIndicators createIndicators(Transform target)
    {
        TargetIndicators targetIndicators = Instantiate(indicator, target.transform.position, Quaternion.identity).GetComponent<TargetIndicators>();
        targetIndicators.OnInit(nameplatePos.transform);
        targetIndicators.OnInit(target.transform);
        targetIndicators.SetText(game_name[Random.Range(0, game_name.Length)]);
        return targetIndicators;
    }

    public void SpawnEnemy()
    {
        if (totalEnemySpawn == 5)
        {
            Debug.Log("sudden death");
        }

        else
        {
            int ran = Random.Range(0, emptyPos.Count);
            //Enemy enemy = Instantiate(enemyPrefab, emptyPos[ran], Quaternion.identity).GetComponent<Enemy>();
            Enemy enemy = EasyObjectPool.instance.GetObjectFromPool("Enemy", emptyPos[ran], Quaternion.identity).GetComponent<Enemy>();
            enemy.gameObject.SetActive(true);
            enemy.ChangeState(new PatrolState());
            enemyList.Add(enemy);
            emptyPos.RemoveAt(ran);
            totalEnemySpawn += 1;
            totalCharAlive -= 1;
            Debug.Log(totalCharAlive);
        }
    }

    public void RemoveEnemy()
    {
        foreach (Enemy enemy in enemyList)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    public void SpawnOrbs()
    {
        int ran = Random.Range(0, emptyPos.Count);
        GameObject powerup = Instantiate(powerups[Random.Range(0, 2)], emptyPos[ran], Quaternion.identity);
        emptyPos.RemoveAt(ran);
        powerup.transform.position += new Vector3(0, 2, 0);
    }
}
