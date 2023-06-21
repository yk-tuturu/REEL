using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int level;
    public float minTime;
    public float maxTime;
    public int maxCapacity;
    public float uncommonProb;
    public float rareProb;
}

[System.Serializable]
public class Levels 
{
    public Level[] levels;
}
