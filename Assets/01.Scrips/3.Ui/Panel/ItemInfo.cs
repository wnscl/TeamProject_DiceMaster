using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfo : MonoBehaviour
{
    public ItemSlot itemSlot;

    public Image itemIcon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemValue1;
    public TextMeshProUGUI itemValue2;
    public TextMeshProUGUI itemValue3;
    public TextMeshProUGUI itemValue4;
    public Image equipBtn;
    public TextMeshProUGUI equipBtnText;
    public Image useBtn;
    public Image throwBtn;
    public Image equipMark;

    public void InfoWIndowOnAndOff()
    {
        if (this.gameObject.activeInHierarchy)
        {
            Debug.Log("끈다");
            this.gameObject.SetActive(false);
            ResetInfo();

            itemSlot = null;
        }
        else if (!this.gameObject.activeInHierarchy)
        {
            InitSetInfo();
            this.gameObject.SetActive(true);
        }
    }


    public void ResetInfo()
    {
        Debug.Log("슬롯 리셋");
        itemIcon.sprite = null;
        itemName.text = string.Empty;
        itemDescription.text = string.Empty;
    }

    public void InitSetInfo()
    {
        Debug.Log($"슬롯 정보 셋팅 :{itemSlot.item.ID}");
        var iData = itemSlot.item.itemData;
        itemIcon.sprite = iData.itemIcon;
        itemName.text = iData.itemName;
        itemDescription.text = iData.description;

        // 장비 아이템인 경우
        if (itemSlot.item is EquipItemInstance equipInst)
        {
            equipBtn.gameObject.SetActive(true);

            if (equipInst.isEquipped)
            {
                equipMark.gameObject.SetActive(true);
                equipBtnText.text = "장착 해제";
            }
            else
            {
                equipMark.gameObject.SetActive(false);
                equipBtnText.text = "장착";
            }

            useBtn.gameObject.SetActive(false);
        }

        // 주사위 아이템인 경우
        else if (itemSlot.item is DiceItemInstance diceInst)
        {
            equipBtn.gameObject.SetActive(true);

            if (diceInst.isEquipped)
            {
                equipMark.gameObject.SetActive(true);
                equipBtnText.text = "장착 해제";
            }
            else
            {
                equipMark.gameObject.SetActive(false);
                equipBtnText.text = "장착";
            }

            useBtn.gameObject.SetActive(false);
        }

        // 소비 아이템
        else if (itemSlot.item is ConsumableItemInstance)
        {
            equipBtn.gameObject.SetActive(false);
            useBtn.gameObject.SetActive(true);
        }

        // 퀘스트 아이템
        else if (itemSlot.item.itemData is QuestItemData)
        {
            equipBtn.gameObject.SetActive(false);
            useBtn.gameObject.SetActive(false);
        }
    }



  public void OnEquip()
{
    IItem iitem = itemSlot.item;

    // 장비 아이템 처리
    if (iitem is EquipItemInstance equipInst)
    {
        var equipSlot = UIManager.Instance.equipSlot;

        // 이미 다른 장비가 장착돼 있고, 현재 장착 중인 것과 다른 경우
        if (equipSlot.equippedItem != null && equipSlot.equippedItem.ID != iitem.ID)
        {
            if (equipSlot.equippedItem is EquipItemInstance prevEquip)
                prevEquip.isEquipped = false;

            equipSlot.SetEquipSlot(equipSlot.equippedItem).sprite = null;
            Color offColor = equipSlot.SetEquipSlot(equipSlot.equippedItem).color;
            offColor.a = 0f;
            equipSlot.SetEquipSlot(equipSlot.equippedItem).color = offColor;

            equipSlot.equippedItem = null;
        }

        if (!equipInst.isEquipped)
        {
            equipInst.isEquipped = true;
            equipSlot.equippedItem = iitem;

            Image icon = equipSlot.SetEquipSlot(iitem);
            icon.sprite = iitem.itemData.itemIcon;
            Color color = icon.color;
            color.a = 1f;
            icon.color = color;
        }
        else
        {
            equipInst.isEquipped = false;
            equipSlot.equippedItem = null;

            Image icon = equipSlot.SetEquipSlot(iitem);
            icon.sprite = null;
            Color color = icon.color;
            color.a = 0f;
            icon.color = color;
        }
    }

    // 주사위 아이템 처리
    else if (iitem is DiceItemInstance diceInst)
    {
        diceInst.isEquipped = !diceInst.isEquipped;

        Image icon = UIManager.Instance.equipSlot.SetEquipSlot(iitem);
        if (diceInst.isEquipped)
        {
            icon.sprite = iitem.itemData.itemIcon;
            Color color = icon.color;
            color.a = 1f;
            icon.color = color;
        }
        else
        {
            icon.sprite = null;
            Color color = icon.color;
            color.a = 0f;
            icon.color = color;
        }
    }

    InitSetInfo(); // 버튼 텍스트 갱신 등
    UIManager.Instance.inventory.FindSameItem(iitem); // 인스턴스 정보 저장
}

    public void OnUse()
    {
        
        
    }










}