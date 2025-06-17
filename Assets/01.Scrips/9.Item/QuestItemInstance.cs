using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItemInstance :IItem
{
    

    public ItemData itemData { get; }
    public int ID { get; }
    public QuestItemInstance(QuestItemData data, int id)
    {
        this.itemData = data;
        this.ID = id;
       
    }
}
