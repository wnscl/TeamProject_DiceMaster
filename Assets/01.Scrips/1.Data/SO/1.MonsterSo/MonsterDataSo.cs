using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "DataSo", menuName = "SoDatas/Monster Data", order = 1)]
//몬스터가 가진 값 초기화와 관리 간편화를 위한 스크립터블 오브젝트
public class MonsterDataSo : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private int    maxHp;
    [SerializeField] private int    def;
    [SerializeField] private int    dodge;

    [SerializeField] private GameObject[] _skillPrefabs;

    public string Name  => name;
    public int MaxHp    =>  maxHp;
    public int Def      =>  def;
    public int Dodge    =>  dodge;
    public GameObject[] _SkillPrefabs => _skillPrefabs;


}
