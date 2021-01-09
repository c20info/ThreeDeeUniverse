using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHammerController : ControllerObject
{
    private SlectionHammerManager slectionManager;
    private int slotUsed;
    void Start()
    {
        slectionManager = this.gameObject.GetComponent<SlectionHammerManager>();
        slotUsed = PlayerObjectController.takedObjects[getPositionInObjectList()].getSlot();
    }
    void Update()
    {
        Debug.Log("martello:::::::::::" + slotUsed);
        if (SelectionSlotManager.currentSlotSelected == slotUsed)
            ToggleVisibility(this.gameObject.transform, true);
        else
            ToggleVisibility(this.gameObject.transform, false);
        if(slotUsed == SelectionSlotManager.currentSlotSelected && Input.GetButtonDown("Fire2") && SearchTag.search("dst", slectionManager.getSelection()))
        {
            Destroy(slectionManager.getSelection());
            Destroy(this.gameObject);
            PlayerObjectController.takedObjects[getPositionInObjectList()] = null;
            SelectionSlotManager.removeAtSlot();
            victoryCheck.blockDestroied++;

        }
       
    }

    

}
