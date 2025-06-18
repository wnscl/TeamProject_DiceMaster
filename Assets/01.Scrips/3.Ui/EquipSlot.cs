using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{

    public Image cloakIcon;
    public Image clothetIcon;
    public Image ringIcon;
    public Image shoesIcon;
    public Image diceIcon;
    public IItem equippedItem;

    public Image SetEquipSlot(IItem data)
    {
      
        if (data is EquipmentItemData equipData)
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

        if (data is DiceItemData diceData)
        {
          return   cloakIcon;
        }

        return null;
    }
}