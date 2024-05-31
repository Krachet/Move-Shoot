using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        child.SetActive(true);
    }

    public void Throw()
    {
        child.SetActive (false);
        Invoke(nameof(OnEnable), 0.4f);
    }
}
