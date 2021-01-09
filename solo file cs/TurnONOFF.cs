using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnONOFF : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject luce;
    public bool active;
    void Start()
    {
        luce = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject;
        luce.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnOn()
    {
        luce.SetActive(true);
        active = true;
    }

    public void turnOff()
    {
        luce.SetActive(false);
        active = false;
    }
}
