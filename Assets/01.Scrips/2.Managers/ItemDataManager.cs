using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager : MonoBehaviour
{
    public static ItemDataManager Instance { get; private set; }
   
    public List<ItemData> ItemDataSO = new List<ItemData>();
    public Dictionary<string, ItemData> ItemDatas = new Dictionary<string, ItemData>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        InitData();
    }

    void InitData()
    {
        foreach (ItemData data in ItemDataSO)
        {
            if (!ItemDatas.ContainsKey((data.itemCode)))
            {
                ItemDatas.Add(data.itemCode, data);
            }
            else
            {
                Debug.LogError("중복 아이템 ID: " + data.itemCode);
            }
        }
    }
    
    public  bool TryDataCast<T>(ItemData data, out T result ) where T : class
    {
        if (data is T matched)
        {
            result = matched;
            Debug.Log("성공" + data.itemCode);
            return true;
        }
        result = null;
        Debug.Log("실패" + data.itemCode);
        return false;
    }

    
}