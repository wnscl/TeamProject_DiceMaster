using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[Serializable]
public class DataForSaveLoad
{
    public Dictionary<string, string> PlayerStats { get; private set; }
    //public List<IItem> InventoryData { get; private set; }
    public ItemSaveData[] ItemSaveData { get; private set; }
    public List<QuestData>[] QuestData { get; private set; }
    public SaveData SaveData { get; private set; }

    public DataForSaveLoad(Dictionary<string, string> playerStats, ItemSaveData[] itemSaveData, List<QuestData>[] questData, SaveData saveData)//
    {
        PlayerStats = playerStats;
        ItemSaveData = itemSaveData;
        QuestData = questData;
        SaveData = saveData;
    }
     
    public DataForSaveLoad GetSaveData()
    {
        PlayerStats = GameManager.Instance.player.statHandler.ToStatDict();
        //InventoryData = UIManager.Instance.inventory.items.ToList();
        int itemCount = UIManager.Instance.inventory.items.Count;
        ItemSaveData = new ItemSaveData[itemCount];
        for(int i = 0; i < itemCount; i++)
        {
            IItem item = UIManager.Instance.inventory.items[i];
            ItemSaveData[i] = new ItemSaveData(item.itemData.itemCode, item.ID);
        }        

        QuestData = new List<QuestData>[2];
        QuestData[0] = QuestManager.Instance.PlayerQuests.ToList();
        QuestData[1] = QuestManager.Instance.CompletedQuests.ToList();

        SaveData = new SaveData(StageManager.Instance.currentStage, GameManager.Instance.player.transform.position);

        return new DataForSaveLoad(PlayerStats, ItemSaveData, QuestData, SaveData);//
    }

    public void GetLoadData(string jsonString)
    {
        JObject root = JObject.Parse(jsonString); //방법 1

        JToken player = root["PlayerStats"];
        LoadPlayer(player);

        JToken inven = root["ItemSaveData"];
        //LoadInventory(inven);

        JToken quest = root["QuestData"];
        LoadQuest(quest);

        JToken saveData = root["SaveData"];
        LoadStage(saveData);
    }

    private void LoadPlayer(JToken Data)
    {
        //GameManager.Instance.player.statHandler.serializeStats = JsonConvert.DeserializeObject<Dictionary<string,string>>(Data.ToString()); //방법 2
        foreach (JProperty jj in Data)
        {
            GameManager.Instance.player.statHandler.serializeStats[jj.Name] = jj.Value.ToString();
        }

        GameManager.Instance.player.statHandler.LoadStatsToCurrent();
    }

    private void LoadInventory(JToken Data)
    {
        JArray itemData = (JArray)Data;
        List<IItem> inventory = new List<IItem>();

        for(int i=0; i<itemData.Count;i++)
        {
            
        }
        




        UIManager.Instance.inventory.items = JsonConvert.DeserializeObject<List<IItem>>(Data.ToString()).ToList();        
    }

    private void LoadQuest(JToken Data)
    {
        QuestManager.Instance.PlayerQuests = JsonConvert.DeserializeObject<List<QuestData>>(Data[0].ToString()).ToList();
        QuestManager.Instance.CompletedQuests = JsonConvert.DeserializeObject<List<QuestData>>(Data[1].ToString()).ToList();
    }

    private void LoadStage(JToken Data)
    {
        //JValue currentStage = (JValue)Data["currentStage"];
        //JToken position = Data["position"];
        float x = float.Parse(Data["X"].ToString());
        float y = float.Parse(Data["Y"].ToString());
        
        StageManager.Instance.currentStage = int.Parse(Data["CurrentStage"].ToString())-1;        
        GameManager.Instance.player.transform.position = new Vector2(x, y);
    }
}

[Serializable]
public class SaveData
{
    public int CurrentStage { get; private set; }
    //public Vector2 Position { get; private set; }

    public float X { get; private set; }
    public float Y { get; private set; }

    public SaveData(int currentStage, Vector2 position)
    {
        CurrentStage = currentStage; X = position.x; Y = position.y ;
    }
}

[Serializable]
public class ItemSaveData
{
    public string ItemCode;
    public int ID;
    //public bool IsDcEquipped;

    public ItemSaveData(string itemCode, int iD )//,bool isDcEquipped
    {
        ItemCode = itemCode;
        ID = iD;
        //IsDcEquipped = isDcEquipped;
    }
}
