using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DataForSaveLoad : MonoBehaviour
{
    [SerializeField] Dictionary<string, string> playerStats;

    public DataForSaveLoad GetSaveData()
    {
        playerStats = GameManager.Instance.player.statHandler.ToStatDict();
        return this;
    }

    public void GetLoadData(string jsonString)
    {
        //GameManager.Instance.player.statHandler.serializeStats = JsonConvert.DeserializeObject<Dictionary<StatType,int>>(jsonString); ////방법 2

        JObject root = JObject.Parse(jsonString); //방법 1

        JToken player = root["playerStats"];
        LoadPlayer(player);

        JToken inven = root["inventory"]; //아마 이런식?
    }

    public void LoadPlayer(JToken Data)
    {
        int i = 0;
        foreach(JProperty jj in Data)
        {
            GameManager.Instance.player.statHandler.serializeStats[jj.Name] = jj.Value.ToString();
            i++;
        }

        GameManager.Instance.player.statHandler.LoadStatsToCurrent();
    }
}
