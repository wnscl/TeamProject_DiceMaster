using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemInstance : IItem
{
    public ItemData itemData { get; private set; }
    public int ID { get; private set; }

    public EquipItemInstance(ItemData data,int id)
    {
        this.itemData = data;
        this.ID = id;
    }
}
