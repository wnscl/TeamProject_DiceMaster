using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItemInstance : IItem
{
    public ItemData data { get; }
    public int ID { get; }

    public ConsumableItemInstance(ItemData data, int ID)
    {
      
    }
}
