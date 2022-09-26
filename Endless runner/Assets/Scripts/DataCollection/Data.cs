using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Data 
{
    public string date;
    public string time;

    public int score;
    public int runs;
    public int fish;
    public Dictionary<Consumable.ConsumableType, int> consumabled_consumbed;
    public string obstacle_impacted;

    public float speed_atDeath;

    public Data()
    {
        date    = "";
        time    = "";
        score   = 0;
        runs    = 0;
        fish    = 0;
        consumabled_consumbed = new Dictionary<Consumable.ConsumableType, int>();
        obstacle_impacted  = "";
        speed_atDeath = 0f; 
    }
}
