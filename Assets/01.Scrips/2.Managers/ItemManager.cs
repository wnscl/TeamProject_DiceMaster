using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
   public List<IItem> allItems = new List<IItem>();
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

    public IItem CreateItem(ItemData data)
    {
        IItem item = null;
// 종류가 많아지면 수정 힘들어짐
//제네릭을 이용하면 좋음

        if (data is EquipmentItemData ED)
        {
            item = new EquipItemInstance(ED, nextID);
        }
        else if (data is ConsumableItemData CD)
        {
            item = new ConsumableItemInstance(CD, nextID);
        }
        else if (data is DiceItemData DD)
        {
            item = new DiceItemInstance(DD, nextID);
        }
        else if (data is QuestItemData QD)
        {
            item = new QuestItemInstance(QD, nextID);
        }
        else
        {
            Debug.LogWarning($"잘못 된 Data가 연결된 듯? {data.itemName}, {data.name}");
            return null;
        }

        Debug.Log($"아이템 : {item.itemData.itemCode} 아이템 코드 {item.itemData.itemName}생성");
        allItems.Add(item);
        GetNextID();
        return item;
    }

  

    /*void AddItem(IItem item)
    {
        // 인벤토리에 들어갈 함수
    }

    void InstanceItem(string code, ItemData data)
    {
        //사용할 곳에서 아이템 데이터매니져 캐싱해서 데이터 딕셔너리에서 꺼내기? 
        IItem a = CreateItem(data);
        AddItem(a);
    }*/
}

//코드는 int 선호 
//1000 번대는 아이템 2000~ 번대는 몬스터 이런식으로 구분함
//단위 수에 좀 더 타입구분처럼 지정하여 사용  천의자리 아이템 백의 자리 무기아이템 10의 자리~~~ 이런식으로 나누는 약속을 함
//