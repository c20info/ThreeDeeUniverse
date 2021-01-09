using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * è la superclasse che gestisce gli oggetti
 * saprsi nella mappa
 */
public class SpawnableObject : MonoBehaviour
{
    protected bool enable = false;
    //controlla se l'oggetto è attivo

    public LayerMask mazeMask;
    //contiene il layer da controllare per vedere se collide con la mappa

    void Start()
    {
        if (Physics.CheckSphere(this.transform.position, 0.1f, mazeMask))
            Destroy(this.gameObject);
        //elimina gli oggetti che collidono con la mappa
    }

    //setta l'attività dell'oggetto
    public void setEnable(bool eanble)
    {
        this.enable = eanble;
    }


}
