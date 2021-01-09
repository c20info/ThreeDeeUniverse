using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerObject : MonoBehaviour
{
    public int positionInObjectList;


    protected void ToggleVisibility(Transform obj, bool state)
    {
        for (int i = 0; i < obj.childCount; i++)
        {
            obj.GetChild(i).GetComponent<MeshRenderer>().enabled = state;
        }
    }

    public void setPositionInObjectList(int pos)
    {
        this.positionInObjectList = pos;
    }

    public int getPositionInObjectList()
    {
        return this.positionInObjectList;
    }
}
