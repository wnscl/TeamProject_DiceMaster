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
[Button]
  public void TestItem()
  {
    ItemData item = ItemDataManager.Instance.ItemDatas["0_TestEquipment"];
   IItem newItem = ItemManager.Instance.CreateItem(item);
   AddItem(newItem);
    

  }

  public void GetItem(String code)
  {
    ItemData item = ItemDataManager.Instance.ItemDatas[code];
    IItem newItem = ItemManager.Instance.CreateItem(item);
    AddItem(newItem);
  }
}
