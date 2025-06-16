using System;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class DataForSaveLoad : MonoBehaviour
{
    [SerializeField] PlayerSaveData player;


    public DataForSaveLoad GetSaveData()
    {
        player = new PlayerSaveData();

        return new DataForSaveLoad();
    }

    public void GetLoadData(string jsonString)
    {
        JObject root = JObject.Parse(jsonString);

        JToken player = root["player"];
        JToken inven = root["inventory"]; //아마 이런식?
    }
}

[Serializable]
public class PlayerSaveData
{
    string name;
    float level;
    float currentExp;
    float money;
    float maxHP;
    float currentHP;
    float def;
    float mDef;
    float dodge;
    float moveSpeed;

    public PlayerSaveData()
    {
        name = GameManager.Instance.player.name;
        level = GameManager.Instance.player.statHandler.GetStat(StatType.Level);
        maxHP = GameManager.Instance.player.statHandler.GetStat(StatType.MaxHp);
        currentHP = GameManager.Instance.player.statHandler.GetStat(StatType.Hp);
        def = GameManager.Instance.player.statHandler.GetStat(StatType.PhysicalDefense);
        mDef = GameManager.Instance.player.statHandler.GetStat(StatType.MagicalDefense); ;
        dodge = GameManager.Instance.player.statHandler.GetStat(StatType.Evasion); ;
        currentExp = GameManager.Instance.player.statHandler.GetStat(StatType.Exp);
        money = GameManager.Instance.player.statHandler.GetStat(StatType.Money);
        moveSpeed = GameManager.Instance.player.statHandler.GetStat(StatType.MoveSpeed);
    }
}
