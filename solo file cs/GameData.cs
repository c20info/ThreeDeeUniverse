using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//oggetto per la conversione in file binario, contiene i dati della partita
public class GameData
{
    public int broken, hammers, torches;
    public float time;
    //dati della partita (blocchi rotti, martelli raccolti, torce raccolte, tempo trascorso)

    public GameData(float time, int broken, int hammers, int torches)
    {
        this.time = time;
        this.broken = broken;
        this.hammers = hammers;
        this.torches = torches;
    }
    //costruttore per assegnazione dei dati
}
