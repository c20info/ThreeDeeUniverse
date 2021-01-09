using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakedObject : ScriptableObject
{
    protected static bool haveObject = false;
    protected GameObject selectionManager;
    protected SlectionHammerManager selectionManagerScript;
    protected GameObject objectPickedPrefab;
    protected static GameObject objectPicked, objectTaked;
    protected static int slot;

    private static int lastID = -1;
    private int ID;


    public TakedObject(GameObject selectedObject, GameObject objectPicked,string slotImage)
    {
        haveObject = true;
        this.ID = lastID++;
        slot = SelectionSlotManager.addAtSlot(slotImage);
        objectTaked = Instantiate(objectPicked, GameObject.Find("hand").transform.position, GameObject.Find("hand").transform.rotation);
        objectTaked.transform.parent = GameObject.Find("hand").transform;
        Destroy(selectedObject);
    }



    public int getSlot()
    {
        return slot;
    }

    public GameObject getObjectTaked()
    {
        return objectTaked;
    }
}
