using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItemInstance : IItem
{
    public ItemData data { get; private set; }
    public int ID { get;private set; }

    public ConsumableItemInstance(ItemData data, int ID)
    {
      
    }
}
