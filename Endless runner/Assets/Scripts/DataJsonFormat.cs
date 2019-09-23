using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class DataJsonFormat 
{
    public string date;
    public string time;

    public int score;
    public int runs;
    public int fish;
    public List<Consumable.ConsumableType> collected;
    public string obstacle_impacted;

    public float speed_atDeath;
}
