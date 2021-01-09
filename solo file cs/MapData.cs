using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class MapData
{
    public int dimX, dimY, difficulty, random;

    public MapData(int dimX, int dimY, int difficulty, int random)
    {
        this.dimX = dimX;
        this.dimY = dimY;
        this.difficulty = difficulty;
        this.random = random;
    }
}
