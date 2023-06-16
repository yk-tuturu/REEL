using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public List<int> totalCaught;
    public List<int> totalSold;

    public List<Upgrade> upgradeLevels;
    
    public float money;

    public List<int> Rod0Capacity;
    public List<int> Rod1Capacity;
    public List<int> Trap0Capacity;
    public List<int> Trap1Capacity;
}
