using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicators : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject parent;

    [SerializeField] RectTransform rect;

    Transform target;
    Vector3 viewpoint;
    Vector3 halfScreen = new Vector2(Screen.width, Screen.height) / 2;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(Nameplates.Instance.getCanvas().transform);  
    }

    public void OnInit(Transform target)
    {
        gameObject.SetActive(true);
        this.target = target;
        Color color = new Color(Random.value, Random.value, Random.value, 1);
        levelText.color = color;
        nameText.color = color;
    }
    // Update is called once per frame
    void Update()
    {
        viewpoint = Camera.main.WorldToScreenPoint(target.position) - halfScreen;
        rect.anchoredPosition = viewpoint / (Screen.width / 1080f);
    }

    public void SetScore(int score)
    {
        levelText.text = score.ToString();
    }

    public void SetText(string text)
    {
        nameText.text = text;
    }
}
