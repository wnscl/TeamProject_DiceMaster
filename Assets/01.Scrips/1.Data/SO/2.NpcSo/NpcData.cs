using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NpcData", menuName = "SoDatas/Npc Data", order = 1)]
public class NpcData : ScriptableObject
{
    [SerializeField] private string npcId; // NPC ID
    [SerializeField] private string npcName; // NPC �̸�
    [SerializeField] private string npcDescription; // NPC ����
    [SerializeField] private List<QuestData> npcQuests; // �ش� NPC�� �����ϴ� ����Ʈ �÷���
    [SerializeField] private TextAsset npcScripts; // NPC�� ��ȭ ��ũ��Ʈ ������
    public List<QuestData> NpcQuests { get { return npcQuests; } }

#if UNITY_EDITOR
    // �����Ϳ��� ���� �ٲ� �� �ڵ� ȣ���
    private void OnValidate()
    {
        // npcId�� ��������� ���� ID�� ���� ����
        if (string.IsNullOrEmpty(npcId))
        {
            npcId = Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this); // ������� ���� ǥ��
        }
    }
#endif
}
