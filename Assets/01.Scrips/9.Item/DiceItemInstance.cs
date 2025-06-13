using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceItemInstance : IItem
{
    public ItemData data { get; private set; }
    public int ID { get; private set; }

    public DiceItemInstance(ItemData data, int id)
    {    this.data = data;
          this.ID = id; 
    }

}
