using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 아이템 인스턴가 공통으로 가질 프로퍼티를 위한 인터페이스
/// </summary>
public interface IItem
{ 
   ItemData itemData { get; }
   int ID{get;  }// 각 아이템이 종류 상관없이 생성 순서에 따라 ++하여 가져갈  고유ID값
    
}

public enum ItemGrade
{
   Common,
   Uncommon,
   Rare,
   Unique,
   Legendary,
   Quest
   
}


/// <summary>
/// 아이템 초기생성 기본 필드 중 공통필드 들고 있는 아이템 SO데이터 부모 클래스 
/// </summary>
public abstract class ItemData : ScriptableObject
{
   public string itemCode; //ItemDataManager에 키값으로 들어갈 아이템 종류 분류할 ID
   public string itemName;
   public string description;
   public int requiredLevel;
   public ItemGrade grade;
   public Sprite itemIcon;
   public GameObject itemPrefab; 
}
