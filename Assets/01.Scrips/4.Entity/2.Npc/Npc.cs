using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;

/// <summary>
/// �÷��̾���� ��ȣ�ۿ��� �����ϴ� �������̽�
/// </summary>
public interface IInteractable
{
    void OnInteract();  // �÷��̾�� ��ȣ�ۿ��ϴ� �޼ҵ�
}

/// <summary>
/// NPC ��ȭ ������ ����
/// </summary>
public enum NpcTalkType
{
    Greeting,   // �λ縻
    Conversation,   // ȸȭ
    Farewell,   // �ۺ� �λ�
    Quest,   // ����Ʈ ���� ��ȭ
    AskQuest,   // ����Ʈ ���� ��û
    AcceptQuest,    // ����Ʈ ���� ��
    CompleteQuest,  // ����Ʈ �Ϸ� ��
    IncompleteQuest // ����Ʈ �̿Ϸ� �� �ش� ����Ʈ ��ȭ ��û
}

public class Npc : MonoBehaviour, IInteractable
{
    [SerializeField] private NpcData npcData; // NPC ������
    private JToken parseNpcScript; // �Ľ̵� NPC ��ũ��Ʈ ������
    private NpcScriptUI npcScriptUI; // NPC ��ũ��Ʈ UI

    void Start()
    {

    }

    void Update()
    {
        
    }

    /// <summary>
    /// ����Ʈ ���� �޼ҵ�
    /// </summary>
    /// <param name="data"></param>
    public void AcceptQuest(QuestData data)
    {
        if (npcData.ValidQuests != null)
        {
            foreach (QuestData quest in npcData.ValidQuests)
            {
                // �Ű����� data�� validQuests�� �ִ� ����Ʈ����, ���� ������ ����Ʈ���� Ȯ��
                if (quest.QuestId == data.QuestId && quest.IsAvailable)
                {
                    // �ش� ����Ʈ�� ��ȿ�� ���, �÷��̾� ����Ʈ ����Ʈ�� ����Ʈ �߰�
                    QuestManager.Instance.PlayerQuests.Add(quest);

                    // memo : IsAvailable�� false�� ���� �ʴ� ���� => ������ ����Ʈ�� �޴��� ǥ���ϰ�, ������ ����Ʈ�� �ٽ� ������ �� ����Ʈ �Ϸ��Ű�� ����
                    quest.IsAccepted = true;

                    Debug.Log($"{quest.QuestName} ����Ʈ ����");

                    return;
                }
            }
        }
        else
        {
            Debug.LogWarning("������ �� �ִ� ����Ʈ�� �����ϴ�.");
        }
    }

    /// <summary>
    /// ����Ʈ �Ϸ� �޼ҵ�
    /// </summary>
    /// <param name="data"></param>
    public void CompleteQuest(QuestData data)
    {
        if (npcData.ValidQuests != null)
        {
            foreach (QuestData quest in npcData.ValidQuests)
            {
                // �Ű����� data�� npcData.NpcQuests�� �ִ� ����Ʈ����, �÷��̾ �������� ����Ʈ���� Ȯ��
                if (quest.QuestId == data.QuestId && quest.IsAccepted)
                {
                    // �ش� ����Ʈ�� ��ȿ�� ���, �÷��̾� ����Ʈ ����Ʈ���� ����Ʈ ����
                    QuestManager.Instance.PlayerQuests.Remove(quest);
                    QuestManager.Instance.CompletedQuests.Add(quest); // �Ϸ��� ����Ʈ ��Ͽ� �߰�

                    // to do : �ݺ� ����Ʈ�� ���� ���, isAvailable�� true�� �����ϴ� ó���� �߰��� ��
                    quest.IsAccepted = false;
                    quest.IsCompleted = true;

                    Debug.Log($"{quest.QuestName} ����Ʈ �Ϸ�");

                    return;
                }
            }
        }
        else
        {
            Debug.LogWarning("��ȿ���� ���� ����Ʈ�Դϴ�.");
        }
    }

    /// <summary>
    /// �÷��̾�� ��ȣ�ۿ��ϴ� �޼ҵ�
    /// </summary>
    public void OnInteract()
    {
        npcScriptUI = UIManager.Instance.NpcScriptUI;
        npcScriptUI.TargetNpc = this; // ���� NPC ������Ʈ ����

        if (npcScriptUI != null)
        {
            npcScriptUI.TargetNpcData = npcData; // ���� NPC ������ ����
            npcScriptUI.Show(); // NPC ��ũ��Ʈ UI ǥ��
        }
        else
        {
            Debug.LogWarning("NPC ��ũ��Ʈ UI�� �����Ǿ� ���� �ʽ��ϴ�.");
        }
    }

    public bool CheckQuestComplete()
    {
        bool result = false;

        return result;
    }
}
