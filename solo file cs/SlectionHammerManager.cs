using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class SlectionHammerManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Material selectionMaterial;
    public Material lastMaterial;
    private Transform lastSelection;
    private bool change = true;
    private static Material latestMateral;
    private Transform selection;
    private int positionInObjectList;
    private int slotUsed;

    void Start()
    {
        slotUsed = PlayerObjectController.takedObjects[positionInObjectList].getSlot();
    }


    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (slotUsed == SelectionSlotManager.currentSlotSelected && Physics.Raycast(ray, out hit))
        {
            //Thread.Sleep(10);

            if(lastSelection == null && latestMateral != null)
            {
                hit.transform.gameObject.GetComponent<Renderer>().material = latestMateral;
                Debug.Log("ho rilevato un vecchio cambiamento");
            }

            if (hit.transform != lastSelection && lastSelection != null)
            {
                lastSelection.gameObject.GetComponent<Renderer>().material = lastMaterial;
                change = true;
            }

            selection = hit.transform;
            var selectionRender = selection.GetComponent<Renderer>();

            if (change)
            {
                lastMaterial = selectionRender.material;
                change = false;
            }

            if (selectionRender != null && SearchTag.search("slz", selection.gameObject))
            {
                selectionRender.material = selectionMaterial;

            }

            lastSelection = selection;

        }
        else if (slotUsed != SelectionSlotManager.currentSlotSelected && lastSelection != null)
        { 
            
            lastSelection.gameObject.GetComponent<Renderer>().material = lastMaterial;
            latestMateral = lastMaterial;
            lastSelection = null;
            change = true;
        }

    }

    public GameObject getSelection()
    {
        return selection.gameObject;
    }
    

    
}
