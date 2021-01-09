using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int broken, hammers, torches;
    public float time;


    public GameData(float time, int broken, int hammers, int torches)
    {
        this.time = time;
        this.broken = broken;
        this.hammers = hammers;
        this.torches = torches;
    }
}
