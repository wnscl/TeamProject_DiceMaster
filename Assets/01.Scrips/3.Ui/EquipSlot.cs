using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//배열 안 만들고 간단히 만들어보려고 했는데 착각이었습니다. 어우
public class EquipSlot : MonoBehaviour
{

    public Image cloakIcon;
    public Image clothetIcon;
    public Image ringIcon;
    public Image shoesIcon;
    public Image diceIcon;
    public IItem cloakItem;
    public IItem clothetItem;
    public IItem ringItem;
    public IItem shoesItem;
    public IItem diceItem;
    public Image ReturnImage(IItem data)
    {
      
        if (data is EquipItemInstance equipInst && equipInst.itemData is EquipmentItemData equipData)
        {
            switch (equipData.equipType)
            {
                case EquipType.Cloak:
                    return cloakIcon;
                case EquipType.Clothet:
                    return clothetIcon;
                case EquipType.Ring:
                    return ringIcon;
                case EquipType.Shoes:
                    return shoesIcon;
            }
        }

        if (data is DiceItemInstance diceInst&& diceInst.itemData is DiceItemData diceData)
        {
          return   diceIcon;
        }

        return null;
    }
    
    public IItem ReturnIItem(IItem data)
    {
      
        if (data is EquipItemInstance equipInst && equipInst.itemData is EquipmentItemData equipData)
        {
            switch (equipData.equipType)
            {
                case EquipType.Cloak:
                    return cloakItem;
                case EquipType.Clothet:
                    return clothetItem;
                case EquipType.Ring:
                    return ringItem;
                case EquipType.Shoes:
                    return shoesItem;
            }
        }

        if (data is DiceItemInstance diceInst&& diceInst.itemData is DiceItemData diceData)
        {
            return   diceItem;
        }

        return null;
    }
    public void SetIItem(IItem item)
    {
        if (item is EquipItemInstance equipInst && equipInst.itemData is EquipmentItemData equipData)
        {
            switch (equipData.equipType)
            {
                case EquipType.Cloak:
                    cloakItem = item;
                    break;
                case EquipType.Clothet:
                    clothetItem = item;
                    break;
                case EquipType.Ring:
                    ringItem = item;
                    break;
                case EquipType.Shoes:
                    shoesItem = item;
                    break;
            }
        }
        else if (item is DiceItemInstance)
        {
            diceItem = item;
        }
    }

    public void ClearIItem(IItem item)
    {
        if (item is EquipItemInstance equipInst && equipInst.itemData is EquipmentItemData equipData)
        {
            switch (equipData.equipType)
            {
                case EquipType.Cloak:
                    cloakItem = null;
                    break;
                case EquipType.Clothet:
                    clothetItem = null;
                    break;
                case EquipType.Ring:
                    ringItem = null;
                    break;
                case EquipType.Shoes:
                    shoesItem = null;
                    break;
            }
        }
        else if (item is DiceItemInstance)
        {
            diceItem = null;
        }
    }
}