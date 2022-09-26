using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using Unity;

//download the .dll from https://github.com/JamesNK/Newtonsoft.Json/releases and add it to the references.
//better to use JsonConvert.SerializeObject instead of JsonUtility when nested jsons are being made 
//you can also just use Jsonutility that comes with unity if you have a simple json
using Newtonsoft.Json;

public class CollectData : MonoBehaviour
{
    public Data obj = new Data();
    public List<string> csvObj = new List<string>();
    public List<Data> jsonObj = new List<Data>();
    public int count_obj = 0;
    public void Awake()
    {
        BeginLogging();
    }

    //track number of runs in a session counter up
    public void Run()
    {
        PlayerData.instance.number_of_runs += 1;
    }

    public void BeginLogging()
    {
        //add headers for csv
        csvObj.Add("date,time,score,runs,fish,consumables,obstacleAtDeath,speedAtDeath");
    }
    
    /*
    * Json collector
    */
    public void collect()
    {
        obj.date = System.DateTime.Now.ToString("yyyy/MM/dd");
        obj.time = System.DateTime.Now.ToString("HH:mm:ss");
        obj.score = TrackManager.instance.score;
        obj.runs++;
        obj.fish = TrackManager.instance.characterController.coins;
        obj.consumabled_consumbed = TrackManager.instance.characterController.characterCollider.consumed;
        obj.obstacle_impacted = TrackManager.instance.characterController.characterCollider.deathData.obstacleType;
        obj.speed_atDeath = TrackManager.instance.speed;

        CollectCSV();
        CollectJson();
    }

    void CollectJson()
    {
        jsonObj.Add(obj);
        string json = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
        WriteOut(json);
    }

    void CollectCSV()
    {
        //get the consumables consumbed object in its own variable to write down easily for csvObj_
        string consumed = "";
        foreach(var consume in obj.consumabled_consumbed)
        {
            consumed += consume + "_";
        }

        csvObj.Add(obj.date  + "," 
                    + obj.time 
                    + "," + obj.score + "," 
                    + obj.runs + "," 
                    + obj.fish + ","
                    + consumed + ","
                    + obj.obstacle_impacted + ","
                    + obj.speed_atDeath);
        
        WriteOutCSV(csvObj);
    }

    void WriteOut(string json_)
    {
        if(!Directory.Exists(Application.streamingAssetsPath + "/Data/JsonOutput/"))
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Data/JsonOutput/");
        string output_file = Application.streamingAssetsPath + "/Data/JsonOutput/" + System.DateTime.Now.ToString("yyyy-MM-dd") + ".json";
        File.WriteAllText(output_file, json_);
    }

    void WriteOutCSV(List<string> values)
    {                
        Debug.Log("log values : " + values);
        string output = "";
        for (int i = 0; i < values.Count; ++i)
        {
            //breakline for everyother line after first
            //this will not trigger for the final line
            if(i > 0)
                output += "\n";

            values[i] = values[i].Replace("\r", "").Replace("\n", ""); 
            output += values[i];
            Debug.Log("line to write is : " + output);
        }

        if(!Directory.Exists(Application.streamingAssetsPath + "/Data/CSVOutput/"))
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Data/CSVOutput/");
        string output_file = Application.streamingAssetsPath + "/Data/CSVOutput/" + System.DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
        File.WriteAllText(output_file,output);
    }
}
