using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class victoryCheck : MonoBehaviour
{
    public LayerMask enterPointLayer;
    //layer mask del punto di vittoria sulla mappa
    public static int torchPicked = 0, hammerPicked = 0, blockDestroied = 0;
    private float time = 0;
    //dati della partita da scrivere nel file


    void Update()
    {
        time += Time.deltaTime;
        //aggiunta al tempo trascorso del tempo passato tra un frame e il successivo

        if (Physics.CheckSphere(this.gameObject.transform.position, 0.5f, enterPointLayer))
        {
            //controllo di collisione tra il questo oggetto(un empty object ai piedi del player) in un raggio di 0.5 con la layer mask del punto vittoria
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/gameData.maschile";
            FileStream stream = new FileStream(path, FileMode.Create);
            GameData data = new GameData(time, blockDestroied, hammerPicked, torchPicked);
            formatter.Serialize(stream, data);
            stream.Close();
            //scrittura su file binario di un istanza data (contiene dati della partita)

            SceneManager.LoadScene(2);
            //carica la scena 2 (outro)
        }
    }

}
