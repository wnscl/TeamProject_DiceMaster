using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    Collection, // ���� ����Ʈ
    Battle,     // óġ ����Ʈ
    Exploration // Ž�� ����Ʈ
}

[CreateAssetMenu(fileName = "QuestData", menuName = "SoDatas/Quest Data", order = 1)]

public class QuestData : ScriptableObject
{
    /*
     * [ to do list ]
     * ������ ����Ʈ Ʈ���Ű� �ߵ����� ��, �ٸ� ����Ʈ�� Ȱ��ȭ ��Ű�� ������ ��ü ����Ʈ ����Ʈ���� questId�� ���� �ش� ����Ʈ�� ã�� Ȱ��ȭ
     * �÷��̾� ������ ���� ����Ʈ�� ���, npc���� �÷��̾� ������ �´� ����Ʈ�� Ž���� ����
     * ��ȣ�ۿ��� ����Ʈ �Ϸ� ������ ���, ��������Ʈ�� ���� ��ȣ�ۿ��� ������Ʈ �ʿ��� ����Ʈ ���� ��������Ʈ�� Ȱ��ȭ ���Ѽ� ����Ʈ Ŭ����
     * ex ) Action QuestFlags �Լ� ����, ������ ������Ʈ�� ��ȣ�ۿ��ؾ���, ������Ʈ�� Ȱ��ȭ ��ų �� ���� QuestFlags ��������Ʈ ȣ��, Count++ ó��, ����Ʈ �ʿ����� Count�� ���� �� ����Ʈ �Ϸ� ó���� �� �� ������ ����
     */

    [Header("Quest Info")]
    [SerializeField] private string questId; // ����Ʈ ���� ID * memo : ���� ���ó ����, ����Ʈ Ž�� ���� �߰� ����
    [SerializeField] private QuestType questType;
    [SerializeField] private string questName;   // ǥ�õ� ����Ʈ �̸�
    [SerializeField] private string questDescription;    // �����ϰ� ǥ���� ����Ʈ ����
    [SerializeField] private List<ItemData> questObjectives; // ���� ����Ʈ�� ���, ����Ʈ ��ǥ ������ �÷���
    public string QuestId { get { return questId; } }
    public string QuestName {get { return questName; } }
    public string QuestDescription { get { return questDescription; } }
    public List<ItemData> QuestObjectives { get { return questObjectives; } }

    [Header("Quest Rewards")]
    [SerializeField] private int questRewardExp;
    [SerializeField] private int questRewardGold;
    [SerializeField] private List<ItemData> questRewards;    // ����Ʈ �Ϸ� ���� ������ �÷���
    public int QuestRewardExp { get { return questRewardExp; } }
    public int QuestRewardGold { get { return questRewardGold; } }
    public List<ItemData> QuestRewards { get { return questRewards; } }

    [Header("Quest Status")]
    [SerializeField] private bool isAvailable; // ����Ʈ ���� ���� ����
    [SerializeField] private bool isAccepted;  // ����Ʈ ���� ����
    [SerializeField] private bool isCompleted;  // ����Ʈ �Ϸ� ����
    public bool IsAvailable
    {
        get { return isAvailable; }
        set { isAvailable = value; }
    }
    public bool IsAccepted
    {
        get { return isAccepted; }
        set { isAccepted = value; }
    }
    public bool IsCompleted
    {
        get { return isCompleted; }
        set { isCompleted = value; }
    }

#if UNITY_EDITOR
    // �����Ϳ��� ���� �ٲ� �� �ڵ� ȣ���
    private void OnValidate()
    {
        // questId�� ��������� ���� ID�� ���� ����
        if (string.IsNullOrEmpty(questId))
        {
            questId = Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this); // ������� ���� ǥ��
        }
    }
#endif
}
