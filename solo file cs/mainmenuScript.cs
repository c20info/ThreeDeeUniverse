using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//componente per la gestione del menu principale
public class mainmenuScript : MonoBehaviour
{
    public GameObject cam;
    //riferimento alla camera per poter gestire la rotazione
    public int dimX, dimY;
    public int difficultyPercentage;
    public int randomPercentage;
    //settaggi della mappa che si andrà a generare



    void Start()
    {
        options_save_btn();
        //salva i dati di default del menu opzioni nelle variabili

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        //setta la visibilità dei due menu
    }
    //inizializzazione dei menu

    void Update()
    {
        cam.transform.Rotate(Vector3.up*Time.deltaTime);
    }
    //gestisce la rotazione della camera

    public void main_start_btn()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/map.barucci";
        FileStream stream = new FileStream(path, FileMode.Create);
        MapData data = new MapData(dimX, dimY, difficultyPercentage, randomPercentage);
        formatter.Serialize(stream, data);
        stream.Close();
        //scrittura su file binario dall istanza data (contiene dati della mappa)

        SceneManager.LoadScene(1);
        //carica la scena 1 (game)


    }
    //si avvia alla pressione del tasto start nel menu main

    public void main_options_btn()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        //setta la visibilità dei due menu
    }
    //si avvia alla pressione del tasto options del menu main

    public void options_save_btn()
    {
        dimX = System.Convert.ToInt32(GameObject.Find("x_if").GetComponent<TMP_InputField>().text);
        dimY = System.Convert.ToInt32(GameObject.Find("y_if").GetComponent<TMP_InputField>().text); ;
        difficultyPercentage = (int)GameObject.Find("dif_sld").GetComponent<Slider>().value;
        randomPercentage = (int)GameObject.Find("rnd_sld").GetComponent<Slider>().value;
        //imposta le variabili dell oggetto ai valori degli input del menu opzioni
    }
    //si avvia alla pressione del tasto save del menu options

    public void options_back_btn()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        //setta la visibilità dei due menu
    }
    //si avvia alla pressione del tasto back nel menu options
}
