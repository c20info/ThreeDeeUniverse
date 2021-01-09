using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class mainmenuScript : MonoBehaviour
{
    public GameObject cam;
    public int dimX, dimY;
    public int difficultyPercentage;
    public int randomPercentage;



    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        options_save_btn();
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    void Update()
    {
        cam.transform.Rotate(Vector3.up*Time.deltaTime);
    }

    public void main_start_btn()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/map.barucci";
        Debug.Log(path);
        FileStream stream = new FileStream(path, FileMode.Create);
        MapData data = new MapData(dimX, dimY, difficultyPercentage, randomPercentage);
        formatter.Serialize(stream, data);
        stream.Close();
        SceneManager.LoadScene(1);
        
    }

    public void main_options_btn()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        
    }

    public void options_save_btn()
    {
        dimX = System.Convert.ToInt32(GameObject.Find("x_if").GetComponent<TMP_InputField>().text);
        dimY = System.Convert.ToInt32(GameObject.Find("y_if").GetComponent<TMP_InputField>().text); ;
        difficultyPercentage = (int)GameObject.Find("dif_sld").GetComponent<Slider>().value;
        randomPercentage = (int)GameObject.Find("rnd_sld").GetComponent<Slider>().value;

    }

    public void options_back_btn()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
