using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionManager : MonoBehaviour
{
    private Transform selection;
    private static Text keyIndicator;
    private GameObject prefabParent;
    // Start is called before the first frame update
    void Start()
    {
        keyIndicator = GameObject.Find("KeyIndicator").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1) && !SelectionSlotManager.isFull())
        { 
            selection = hit.transform;
            prefabParent = getPrefabsParent(selection.gameObject);
            if (prefabParent != null && SearchTag.search("spo", prefabParent))
            {
                keyIndicator.text = "PREMERE [E] PER RACCOGLIERE  2";

                if (Input.GetButtonDown("Fire1"))
                {
                    Debug.Log(((Tag)prefabParent.GetComponent<Tag>()).getTag());
                        Debug.Log("ha il tag");
                        ((SpawnableObject)prefabParent.GetComponent<SpawnableObject>()).setEnable(true);
                        keyIndicator.text = " ";
                }
            }
        }
    }

    private GameObject getPrefabsParent(GameObject object1)
    {
        GameObject app1, app2 = null;
        int indexMax = 0;
        app2 = object1.transform.parent.gameObject;
        try
        {
            do
            {
                if (app2.GetComponent<Tag>() != null)
                    return app2;
                app1 = app2.transform.parent.gameObject;
                if (app1 != null)
                    app2 = app1;

                if (app1 == null)
                    return null;

                indexMax++;
            } while (indexMax < 100);
            Debug.Log("non trovato");
        }catch(System.Exception e)
        {
            return null;
        }
        return null;
    }
}
