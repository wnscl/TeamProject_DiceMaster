using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum  ItemName
{
    
}

public enum ItemType
{
 Equipment,
 Consumable,
 Dice
 
   
}

public class ItemData : ScriptableObject
{
   public string itemName;
   public ItemName dataItemName;
   public ItemType itemType;
   public string description;
    
    
    
    
    
}
