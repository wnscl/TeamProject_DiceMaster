using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceItemInstance : IItem
{
    public ItemData itemData { get; private set; }
    public int ID { get; private set; }

    public DiceItemInstance(ItemData data, int id)
    {    this.itemData = data;
          this.ID = id; 
    }

}
