using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

//스탯 타입을 정의 
public enum StatType
{
    Hp,
    MaxHp,
    PhysicalDefense,
    MagicalDefense,
    Evasion,
    MoveSpeed,
    Money,
    Level,
    Exp,
    RequireExp,
    None
}
//메뉴에 스탯 테이터 ScriptableObject로 생성
[CreateAssetMenu(fileName = "New StatData", menuName = "Stats/Chracter Stats")]
public class StatData : ScriptableObject
{
    public string characterName;
    public List<StatEntry> stats;
}

[System.Serializable]
public class StatEntry
{
    public StatType statType;
    public int baseValue;
}

