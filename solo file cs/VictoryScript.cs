using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class VictoryScript : MonoBehaviour
{

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gameData.maschile";
        FileStream stram = new FileStream(path, FileMode.Open);
        GameData data = formatter.Deserialize(stram) as GameData;

        GameObject.Find("timeData_lbl").GetComponent<TextMeshProUGUI>().text = Convert.ToString(data.time/60);
        GameObject.Find("brokenData_lbl").GetComponent<TextMeshProUGUI>().text = Convert.ToString(data.broken);
        GameObject.Find("hammersData_lbl").GetComponent<TextMeshProUGUI>().text = Convert.ToString(data.hammers);
        GameObject.Find("torchesData_lbl").GetComponent<TextMeshProUGUI>().text = Convert.ToString(data.torches);
    }

    void Update()
    {
        GameObject dataPanel = GameObject.Find("dataPanel");

        if (dataPanel.transform.position.y > 150)
        {
            //dataPanel.transform.position = new Vector3(dataPanel.transform.position.x, (float)dataPanel.transform.position.y - 0.1f, dataPanel.transform.position.z);
            dataPanel.transform.Translate(Vector3.up * (-Time.deltaTime) * 400f);
            //dataPanel.GetComponent<CharacterController>().Move(Vector3.up * (-Time.deltaTime) * 100);
        }

    }

    public void mainmenu_btn()
    {

        SceneManager.LoadScene(0);
    }
}
