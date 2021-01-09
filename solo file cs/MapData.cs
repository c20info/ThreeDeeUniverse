using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//oggetto per la conversione in file binario, contiene i dati della mappa
public class MapData
{
    public int dimX, dimY, difficulty, random;
    //dati della mappa (dimensione, difficoltà, randomicità)

    public MapData(int dimX, int dimY, int difficulty, int random)
    {
        this.dimX = dimX;
        this.dimY = dimY;
        this.difficulty = difficulty;
        this.random = random;
    }
    //costruttore per assegnazione dei dati
}
