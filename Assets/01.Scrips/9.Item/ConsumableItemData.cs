using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ConsumableItemData", menuName = "ItemData/ConsumableItem")]
public class ConsumableItemData : ItemData
{
    public int valueAmount; // 회복이나 버프에 사용할 값 
    public int maxStackAmount;// 스택 최대값
    public bool isStackable; // 스택 가능한 아이템인지 
}
