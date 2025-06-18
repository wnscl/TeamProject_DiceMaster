using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItemInstance : IItem
{
    public ItemData itemData { get; private set; }
    public int ID { get;private set; }
    
    public int stackAmount; //인스턴스 생성시 주어질 스택 값
    
    

    public ConsumableItemInstance(ConsumableItemData data, int id)
    {
        this.itemData = data;
        this.ID = id;
      
        this.stackAmount = 1;
    }
}
