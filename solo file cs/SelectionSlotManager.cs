using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


//gestisce gli slots del player
public class SelectionSlotManager : MonoBehaviour
{
    public static GameObject slotManager, prefApp;
    /*
     * slot manager: è un'istanza si se stesso
     * prefApp: è loggetto che sto per istanziare
     */

    private static List<bool> full = new List<bool>();
    //contine lo stato delle posizioni degli solts

    public static List<GameObject> slots = new List<GameObject>();
    //contiene le istanze degli slots

    public static int currentSlotSelected;
    //coniene lo slot selezionato
    void Start()
    {
        //definisco e inizializzo gli slots:
        slots.Add(this.gameObject.transform.GetChild(0).gameObject);
        slots.Add(this.gameObject.transform.GetChild(1).gameObject);
        slots.Add(this.gameObject.transform.GetChild(2).gameObject);

        for (int i = 0; i < slots.Count; i++)
            full.Add(false);
        slotManager = this.gameObject;


    }

    void Update()
    {
        //contollo il movineto della rotella del mouse
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            this.gameObject.transform.GetChild(currentSlotSelected).GetComponent<RawImage>().texture = (Texture)AssetDatabase.LoadAssetAtPath("Assets/DefaultImage.png", typeof(Texture));
            currentSlotSelected++;
            if (currentSlotSelected >= slots.Count)
                currentSlotSelected = 0;
            this.gameObject.transform.GetChild(currentSlotSelected).GetComponent<RawImage>().texture = (Texture)AssetDatabase.LoadAssetAtPath("Assets/DefaultSelectedImage.png", typeof(Texture));
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            this.gameObject.transform.GetChild(currentSlotSelected).GetComponent<RawImage>().texture = (Texture)AssetDatabase.LoadAssetAtPath("Assets/DefaultImage.png", typeof(Texture));
            currentSlotSelected--;
            if (currentSlotSelected < 0)
                currentSlotSelected = slots.Count - 1;
            this.gameObject.transform.GetChild(currentSlotSelected).GetComponent<RawImage>().texture = (Texture)AssetDatabase.LoadAssetAtPath("Assets/DefaultSelectedImage.png", typeof(Texture));
        }
    }

    //aggiunge un oggetto allo slot
    public static int addAtSlot(string tipo)
    {
        
        prefApp = (GameObject)Resources.Load(tipo, typeof(GameObject));
        //cerco e salvo il nuovo slot

        //cerco la prima posizione libera e lo inserisco, restituendo la posizione:
        for (int i = 0; i < full.Count; i++)
            if (!full[i])
            {
                GameObject app;
                app = Instantiate(prefApp, slots[i].transform.position, Quaternion.Euler(0f, 0f, 0f));
                app.transform.parent = slotManager.transform;
                app.transform.localScale = new Vector3(0.713f, 0.7f, 1f);
                full[i] = true;
                return i;
            }
        return -1;

    }

    //rimuove allo slot corrente
    public static void removeAtSlot()
    {
        try
        {
            Destroy(slotManager.gameObject.transform.
                GetChild(getCountInFullForObject(currentSlotSelected) + slots.Count).gameObject);
            full[currentSlotSelected] = false; 
        }
        catch(Exception e) { }
    }

    //rimuove allo slot specificato
    public static void removeAtSlot(int slot)
    {
        try
        {
            Destroy(slotManager.gameObject.transform.
                GetChild(getCountInFullForObject(slot) + slots.Count).gameObject);
            full[slot] = false;
        }
        catch (Exception e) { }

    }

    //ritorna l'oggetto in quella posizion
    public static GameObject getAtSlot(int slot)
    {
        return slotManager.gameObject.transform.
            GetChild(getCountInFullForObject(slot) + slots.Count).gameObject;
    }

    //controlla se è pieno
    public static bool isFull()
    {
        int app = 0;
        for (int i = 0; i < full.Count; i++)
            if (full[i])
                app++;
        if (app == slots.Count)
            return true;
        return false;
    }

    //contolla quanto oggetti sono presnti nell'array prima della pisizione specificata, posizione compresa
    public static int getCountInFullForObject(int pos)
    {
        
        int app = 0;
        for (int i = 0; i < full.Count; i++)
        {
            if (i == pos)
                return app;
            if (full[i])
                app++;
            
        }

        return app;
    }
}
