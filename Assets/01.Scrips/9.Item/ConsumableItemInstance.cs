using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItemInstance : IItem
{
    public ItemData itemData { get; private set; }
    public int ID { get;private set; }

    

    public ConsumableItemInstance(ConsumableItemData data, int id)
    {
        this.itemData = data;
        this.ID = id;
       
    }
}
