using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum  EquipType
{
    Cloak,//물방
    Body,//체력
    Ring,//마방
    Feet, //회피 
}

[CreateAssetMenu(fileName = "EquipmentItemData", menuName = "ItemData/EquipmentItem")]
public class EquipmentItemData : ItemData
{
    
    public bool isEquipped;
    public EquipType  equipType;
    public float valueAmount;
}
