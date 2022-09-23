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
    public List<DataJsonFormat> jsonObj_ = new List<DataJsonFormat>();
    public int count_obj = 0;

    public void runs()
    {
        PlayerData.instance.number_of_runs += 1;
    }

    public void collect()
    {
        jsonObj_.Add(new DataJsonFormat());
        jsonObj_[count_obj].date = System.DateTime.Now.ToString("yyyy/MM/dd");
        jsonObj_[count_obj].time = System.DateTime.Now.ToString("HH:mm:ss");

        jsonObj_[count_obj].score = TrackManager.instance.score;
        jsonObj_[count_obj].runs = PlayerData.instance.number_of_runs;

        jsonObj_[count_obj].fish = TrackManager.instance.characterController.coins;
        jsonObj_[count_obj].consumabled_consumbed = TrackManager.instance.characterController.characterCollider.consumed;

        jsonObj_[count_obj].obstacle_impacted = TrackManager.instance.characterController.characterCollider.deathData.obstacleType;
        jsonObj_[count_obj].speed_atDeath = TrackManager.instance.speed;

        count_obj++;
        //string json = JsonUtility.ToJson(jsonObj_);

        string json = JsonConvert.SerializeObject(jsonObj_, Formatting.Indented);

        writeout(json);
    }

    void writeout(string json_)
    {
        string output_file = Application.streamingAssetsPath + "/Data/JsonOutput" + System.DateTime.Now.ToString("yyyy-MM-dd") + ".json";
        File.WriteAllText(output_file, json_);
    }
}
