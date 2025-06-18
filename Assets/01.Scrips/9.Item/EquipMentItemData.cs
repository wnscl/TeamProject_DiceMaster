using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum  EquipType
{
    Cloak,//물방
    Clothet,//체력
    Ring,//마방
    Shoes, //회피 
    
}

[CreateAssetMenu(fileName = "EquipmentItemData", menuName = "ItemData/EquipmentItem")]
public class EquipmentItemData : ItemData
{
    
   
    public EquipType  equipType;
    public int valueAmount;
}
