using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hammer : SpawnableObject
{
    // Start is called before the first frame update
    private bool up = false;

    private GameObject player;
   
    
    void Start()
    {
        player = GameObject.Find("Player");
        

    }

    // Update is called once per frame
    void Update()
    {

        this.transform.Rotate(0, 0.2f, 0);
            if (this.transform.position.y < 2 && up)
            this.transform.position = new Vector3(this.transform.position.x,
                                                  this.transform.position.y + 0.5f * Time.deltaTime,
                                                  this.transform.position.z);
            else if (this.transform.position.y > 1 && !up)
            this.transform.position = new Vector3(this.transform.position.x,
                                                  this.transform.position.y - 0.5f * Time.deltaTime,
                                                  this.transform.position.z);
            if (this.transform.position.y <= 1 && !up)
                up = true;
            else if (this.transform.position.y >= 2 && up)
                up = false;
        if (enable)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("rilevo collisione con :" + this.gameObject.name);
                PlayerObjectController.takeObject(this.gameObject,
                    (GameObject)Resources.Load("HammerPicked", typeof(GameObject)),
                    "HammerImage");
                victoryCheck.hammerPicked++;
                enable = false;
            }
        }

    }

}
