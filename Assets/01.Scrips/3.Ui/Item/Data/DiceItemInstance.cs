using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceItemInstance : IItem
{
    public ItemData data { get; }
    public int ID { get; }

    public DiceItemInstance(ItemData data, int ID)
    {
    }

}
