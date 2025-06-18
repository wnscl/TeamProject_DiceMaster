using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  [SerializeField] private int defaultSlotCount = 9;
  public List<IItem> items = new List<IItem>(); //받은 아이템 인스턴스 데이터 저장 리스트
  public List<GameObject> slots = new List<GameObject>(); //슬롯 리스트
  public GameObject itemSlotPrefab; //슬롯 프리팹
  public GameObject content; //슬롯 생성되는 스크롤 뷰의 콘텐트 오브젝트
  public GameObject slot;

  private void Awake()
  {
    SetDefaultSlotCount();
  }

  private void Start()
  {
    GetItem("301");
    GetItem("101");
    GetItem("102");
    GetItem("103");
    GetItem("104");
    GetItem("201");
  }

  public void AddItem(IItem item)
  {
    items.Add(item);

    for (int i = 0; i < slots.Count; i++)
    {
      ItemSlot slotComponent = slots[i].GetComponent<ItemSlot>();


      if (slotComponent.item == null)
      {
        slotComponent.item = item;
        slotComponent.SetSlot(item);
        return;
      }
    }

    GameObject newSlot = Instantiate(itemSlotPrefab, content.transform);
    slots.Add(newSlot);
    newSlot.GetComponent<ItemSlot>().item = item;
    newSlot.GetComponent<ItemSlot>().SetSlot(item);
  }

  public void FindOpenSlot()
  {
    for (int i = 0; i < slots.Count; i++)
    {
      if (slots[i].GetComponent<ItemSlot>().item.ID == UIManager.Instance.itemInfo.itemSlot.item.ID)
      {
        slot = slots[i];
        return;
      }
    }

    Debug.Log(slot.GetComponent<ItemSlot>().item.ID);
  }

  public void RemoveItem()
  {
    FindOpenSlot();
    var itemComponent = slot.GetComponent<ItemSlot>();
    items.Remove(itemComponent.item);    
    ItemManager.Instance.allItems.Remove(itemComponent.item);
    
    slot.GetComponent<ItemSlot>().ONDestroySlot();

    SetDefaultSlotCount();
   
  }

  public void SetDefaultSlotCount()
  {
    int needed = defaultSlotCount - slots.Count;
    for (int i = 0; i < needed; i++)
    {
      GameObject newSlot = Instantiate(itemSlotPrefab, content.transform);
      slots.Add(newSlot);
    }
  }
  
  
  


  public void GetItem(String code)
  {
    ItemData item = ItemDataManager.Instance.ItemDatas[code];
    IItem newItem = ItemManager.Instance.CreateItem(item);
    AddItem(newItem);
  }


  public void FindSameItem(IItem iitem)//인스턴스가 들고있는 bool값등이 바뀌는 경우 그 시점에 같이 호줄해서 바꾼 상태를 리스트에 넣는 매서드 Save위해서 필요
  {
    for (int i = 0; i < items.Count; i++)
    {
      if (items[i].ID ==iitem.ID)
      {
        items[i] = iitem;
      }
    }

  }
  
  

  

  //테스트 아이템 생성용
  [Button]
  public void TestItem()
  {
    ItemData item = ItemDataManager.Instance.ItemDatas["0_TestEquipment"];
    IItem newItem = ItemManager.Instance.CreateItem(item);
    AddItem(newItem);
    

  }
}
