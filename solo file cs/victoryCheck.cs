using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class victoryCheck : MonoBehaviour
{
    public LayerMask enterPointLayer;
    public static int torchPicked = 0, hammerPicked = 0, blockDestroied = 0;
    private float time = 0;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //contiene il cempo della partita

        if (Physics.CheckSphere(this.gameObject.transform.position, 0.5f, enterPointLayer))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            string path = Application.persistentDataPath + "/gameData.maschile";

            Debug.Log(path);
            FileStream stream = new FileStream(path, FileMode.Create);
            GameData data = new GameData(time, blockDestroied, hammerPicked, torchPicked);
            formatter.Serialize(stream, data);
            stream.Close();
            SceneManager.LoadScene(2);
        }
    }
}
