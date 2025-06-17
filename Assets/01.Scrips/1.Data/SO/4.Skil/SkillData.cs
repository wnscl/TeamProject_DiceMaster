using UnityEngine;

[CreateAssetMenu(fileName = "DataSo", menuName = "SoDatas/Skill Data", order = 4)]
//���Ͱ� ���� �� �ʱ�ȭ�� ���� ����ȭ�� ���� ��ũ���ͺ� ������Ʈ
public class SkillDataSo : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private string discription;
    [SerializeField] private Sprite icon;

    public string Name          => name;
    public string Discription   => discription;
    public Sprite Icon          => icon;

}