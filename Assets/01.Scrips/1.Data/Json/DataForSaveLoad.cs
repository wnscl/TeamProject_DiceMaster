using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataForSaveLoad
{
    [SerializeField] Dictionary<string, string> playerStats;
    [SerializeField] List<IItem> InventoryData;
    [SerializeField] List<QuestData>[] questData = new List<QuestData>[2];
    [SerializeField] SaveData saveData;
    public DataForSaveLoad GetSaveData()
    {
        playerStats = GameManager.Instance.player.statHandler.ToStatDict();
        InventoryData = UIManager.Instance.inventory.items.ToList();
        questData[0] = QuestManager.Instance.PlayerQuests.ToList();
        questData[1] = QuestManager.Instance.CompletedQuests.ToList();
        saveData = new SaveData(StageManager.Instance.currentStage, GameManager.Instance.player.transform.position);
        return this;
    }

    public void GetLoadData(string jsonString)
    {
        JObject root = JObject.Parse(jsonString); //방법 1

        JToken player = root["playerStats"];
        LoadPlayer(player);

        JToken inven = root["InventoryData"];
        LoadInventory(inven);

        JToken quest = root["questData"];
        LoadQuest(quest);

        JToken saveData = root["saveData"];
        LoadStage(saveData);
    }

    private void LoadPlayer(JToken Data)
    {
        //GameManager.Instance.player.statHandler.serializeStats = JsonConvert.DeserializeObject<Dictionary<string,string>>(Data.ToString()); ////방법 2
        foreach(JProperty jj in Data)
        {
            GameManager.Instance.player.statHandler.serializeStats[jj.Name] = jj.Value.ToString();
        }

        GameManager.Instance.player.statHandler.LoadStatsToCurrent();
    }

    private void LoadInventory(JToken Data)
    {
        UIManager.Instance.inventory.items = JsonConvert.DeserializeObject<List<IItem>>(Data.ToString()).ToList();        
    }

    private void LoadQuest(JToken Data)
    {
        QuestManager.Instance.PlayerQuests = JsonConvert.DeserializeObject<List<QuestData>>(Data[0].ToString()).ToList();
        QuestManager.Instance.CompletedQuests = JsonConvert.DeserializeObject<List<QuestData>>(Data[1].ToString()).ToList();
    }

    private void LoadStage(JToken Data)
    {
        JValue currentStage = (JValue)Data["currentStage"];
        JToken position = Data["position"];
        float x = float.Parse(position["X"].ToString());
        float y = float.Parse(position["Y"].ToString());

        StageManager.Instance.currentStage = int.Parse(currentStage.Value.ToString());
        GameManager.Instance.player.transform.position = new Vector2(x, y);
    }
}

[Serializable]
class SaveData
{
    public int CurrentStage { get; private set; }
    public Vector2 Position { get; private set; }

    public SaveData(int currentStage, Vector2 position)
    {
        CurrentStage = currentStage; Position = position;
    }
}
