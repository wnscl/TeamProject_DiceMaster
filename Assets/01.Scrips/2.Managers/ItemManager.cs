using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    List<IItem> allItems = new List<IItem>();
    private int nextID = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetNextID()
    {
        return nextID++;
    }

    /*public IItem CreateItem( ItemData data)
    { 
        
        IItem item;
        
        switch (data)
        {
            case EquipMentItemData:

                item = new EquipItemInstance( data,nextID);
                allItems.Add(item);
                break;

            case ConsumableItemData:

                break;

            case DiceItemData:

                break;

            default:

                Debug.LogWarning($"잘못 된 Data가 연결된 듯?{data.itemName},{data.itemCode},{data.name}");
                break;
        }
        
        
        GetNextID();
        return item;
    }*/
}