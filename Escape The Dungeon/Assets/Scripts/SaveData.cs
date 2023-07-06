using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int currentLevel;

    public SaveData(PlayerScript player)       
    {
        currentLevel = player.currentLevel;     
    }
    //Datele care vor fi salvate si incarcate, pot fi usor modificate
}
