using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemInstance : IItem
{
    public ItemData data { get; private set; }
    public int ID { get; private set; }

    public EquipItemInstance(ItemData data,int id)
    {
        this.data = data;
        this.ID = id;
    }
}
