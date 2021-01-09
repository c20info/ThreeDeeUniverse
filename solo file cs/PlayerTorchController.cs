using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerTorchController : ControllerObject
{
    private TurnONOFF tof;
    //gestisce lo stato della torcia

    private int slotUsed;
    //specifica lo slot che occupa nell'inventario

    private const float LIFE_TIME = 30, SEC_NEXT_CHANGE = 1, CHANGE_FOR_ACTION = 66 / LIFE_TIME * SEC_NEXT_CHANGE;
    //sono le costanti che servono a gestire la durata della torcia

    private float time = 0, nextChange = SEC_NEXT_CHANGE;
    //sono le variabili che servono a gestire la durata della torcia


    void Start()
    {
        tof = this.gameObject.GetComponent<TurnONOFF>();
        slotUsed = getPositionInObjectList();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("torcia:::::::::::" + slotUsed);
        if (tof != null && (SelectionSlotManager.currentSlotSelected == slotUsed))
        {
            if (Input.GetButtonDown("Fire3"))
            {
                tof.turnOn();
                Debug.Log("accesa");
            }
            else if (Input.GetButtonDown("Fire2"))
                tof.turnOff();
        }

        if (SelectionSlotManager.currentSlotSelected == slotUsed)
            ToggleVisibility(this.gameObject.transform.GetChild(0).transform, true);
        else
            ToggleVisibility(this.gameObject.transform.GetChild(0).transform, false);

        if (tof.active)
        {
            time+= Time.deltaTime;
            Debug.Log(time);
            if (time >= LIFE_TIME)
            {
                Debug.Log("removing in::::" + slotUsed);
                SelectionSlotManager.removeAtSlot(slotUsed);
                Destroy(this.gameObject);
                PlayerObjectController.takedObjects[getPositionInObjectList()] = null;
            }
            else if (time > nextChange)
            {
                ((RectTransform)SelectionSlotManager.getAtSlot(slotUsed).transform.GetChild(0).GetComponent<RectTransform>()).sizeDelta = 
                    new Vector2(70,66 - CHANGE_FOR_ACTION * nextChange / SEC_NEXT_CHANGE);
                nextChange += SEC_NEXT_CHANGE;
            }

        }
    }


}
