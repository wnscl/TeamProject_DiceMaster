using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemInstance : IItem
{
    public ItemData data { get; }
    public int ID { get; }

    public EquipItemInstance(ItemData data, int ID)
    {
    }
}
