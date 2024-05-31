using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollows : Singleton<CameraFollows>
{
    public Transform TF;
    public Transform playerTF;

    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 min;
    [SerializeField] Vector3 max;

    // Start is called before the first frame update

    private void Awake()
    {
        playerTF = FindAnyObjectByType<Player>().transform;
        gameObject.transform.position = new Vector3(0, 5, 20f);
        offset = new Vector3(0f, 4f, 5f);
    }

    public void OnInit()
    {
        offset = new Vector3(0, 18, -12);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        TF.position = Vector3.Lerp(TF.position, playerTF.position + offset, Time.deltaTime * 10f);
    }

    public void SetOffset(float rate)
    {
        offset = Vector3.Lerp(min, max, rate);
    }
}
