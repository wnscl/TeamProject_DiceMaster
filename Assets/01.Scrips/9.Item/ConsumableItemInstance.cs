using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItemInstance : IItem
{
    public ItemData data { get; private set; }
    public int ID { get;private set; }

    

    public ConsumableItemInstance(ConsumableItemData data, int id)
    {
        this.data = data;
        this.ID = id;
       
    }
}
