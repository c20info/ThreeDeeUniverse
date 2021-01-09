using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * gestisce le torce presenti nella mappa
 * fino alla propria distruzione
 */
public class Torch : SpawnableObject
{

    private bool up = false;
    //serve per contollare i limiti del movimento sull'asse y

    void Update()
    {
        //contollo del movimento su e giù e della rotazione
        this.transform.Rotate(0, 0.2f, 0);
        if (this.transform.position.y < 3 && up)
            this.transform.position = new Vector3(this.transform.position.x,
                                                  this.transform.position.y + 0.5f * Time.deltaTime,
                                                  this.transform.position.z);
        else if (this.transform.position.y > 2 && !up)
            this.transform.position = new Vector3(this.transform.position.x,
                                                  this.transform.position.y - 0.5f * Time.deltaTime,
                                                  this.transform.position.z);
        if (this.transform.position.y <= 2 && !up)
            up = true;

        else if (this.transform.position.y >= 3 && up) 
            up = false;


        //se viene abilitata, istanzia l'oggetto nella mano del player e si distrugge
        if (enable)
        {
            PlayerObjectController.takeObject(this.gameObject,
                (GameObject)Resources.Load("TorchPiked(Clone)", typeof(GameObject)),
                "TorchImage");
            victoryCheck.torchPicked++;
            enable = false;
        }
    }


   
}
