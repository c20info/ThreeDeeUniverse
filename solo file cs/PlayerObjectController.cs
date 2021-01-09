using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerObjectController
{
    public static List<TakedObject> takedObjects = new List<TakedObject>(); 
    public static void takeObject(GameObject selectedObject, GameObject objectPicked, string slotImage)
    {
        if (!SelectionSlotManager.isFull())
        {
           
            
            if (checkFreePosition() >= 0)
            {
                int appPos = checkFreePosition();
                takedObjects[appPos] = new TakedObject(selectedObject, objectPicked, slotImage);
                ((ControllerObject)takedObjects[appPos].getObjectTaked().GetComponent<ControllerObject>()).setPositionInObjectList(appPos);
            }
            else
            {
               
                takedObjects.Add(new TakedObject(selectedObject, objectPicked, slotImage));
                ((ControllerObject)takedObjects[takedObjects.Count - 1].getObjectTaked().GetComponent<ControllerObject>()).setPositionInObjectList(takedObjects.Count - 1);
            }


                



        }
    }

    private static int checkFreePosition()
    {
        for (int i = 0; i < takedObjects.Count; i++)
            if (takedObjects[i] == null)
                return i;
        return -1;
    }
}
