using UnityEngine;

[CreateAssetMenu(fileName = "DataSo", menuName = "SoDatas/Skill Data", order = 4)]
//몬스터가 가진 값 초기화와 관리 간편화를 위한 스크립터블 오브젝트
public class SkillDataSo : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private string discription;
    [SerializeField] private Sprite icon;
    [SerializeField] private int skillNumber;

    public string   Name            => name;
    public string   Discription     => discription;
    public Sprite   Icon            => icon;
    public int      SkillNumber     => skillNumber;

}