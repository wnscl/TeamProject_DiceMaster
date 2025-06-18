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

                    quest.IsAccepted = true;
                    quest.IsAvailable = false;  // ����Ʈ ���� ���� ���� �÷��� Ȱ��ȭ

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
    /// ����Ʈ ���� �׽�Ʈ�� �޼ҵ�
    /// </summary>
    [Button]
    public void TestAcceptQuest()
    {
        // validQuests�� ��� ������ ����
        if (npcData.ValidQuests == null || npcData.ValidQuests.Count == 0)
        {
            npcData.GetValidQuest();
        }

        foreach (QuestData quest in npcData.ValidQuests)
        {
            // �Ű����� data�� validQuests�� �ִ� ����Ʈ����, ���� ������ ����Ʈ���� Ȯ��
            if (quest.QuestId == npcData.ValidQuests[0].QuestId && quest.IsAvailable)
            {
                // �ش� ����Ʈ�� ��ȿ�� ���, �÷��̾� ����Ʈ ����Ʈ�� ����Ʈ �߰�
                QuestManager.Instance.PlayerQuests.Add(quest);

                Debug.Log("����Ʈ �߰�: " + QuestManager.Instance.PlayerQuests);

                quest.IsAccepted = true;
                quest.IsAvailable = false;  // ����Ʈ ���� ���� ���� �÷��� Ȱ��ȭ

                Debug.Log($"���� �׽�Ʈ �ڵ� ����. < {quest.QuestName} > ����Ʈ ����");

                return;
            }
        }
    }

    [Button]
    public void TestCompleteQuest()
    {
        foreach (QuestData quest in npcData.ValidQuests)
        {
            // �Ű����� data�� npcData.NpcQuests�� �ִ� ����Ʈ����, �÷��̾ �������� ����Ʈ���� Ȯ��
            if (quest.QuestId == npcData.ValidQuests[0].QuestId && quest.IsAccepted)
            {
                // �ش� ����Ʈ�� ��ȿ�� ���, �÷��̾� ����Ʈ ����Ʈ���� ����Ʈ ����
                QuestManager.Instance.PlayerQuests.Remove(quest);
                QuestManager.Instance.CompletedQuests.Add(quest); // �Ϸ��� ����Ʈ ��Ͽ� �߰�

                // to do : �ݺ� ����Ʈ�� ���� ���, isAvailable�� true�� �����ϴ� ó���� �߰��� ��
                quest.IsAccepted = false;
                quest.IsCompleted = true;

                Debug.Log($"���� �׽�Ʈ �ڵ� ����. < {quest.QuestName} > ����Ʈ �Ϸ�");

                return;
            }
        }
    }

    [Button]
    public void TestNormalTalk()
    {
        // �׽�Ʈ�� ���� NPC ��ũ��Ʈ �Ľ�
        npcData.ParseNpcScript();

        if (npcData.NpcScripts != null)
        {
            JToken normalTalk = parseNpcScript["Normal"]["Conversation"];   // Normal Ÿ���� ��ȭ ��ũ��Ʈ

            // ȸȭ ( Conversation ) Ÿ���� ��ȭ ��ȯ
            List<JToken> validScriptList = normalTalk.Where(t => t["Type"].ToString() == NpcTalkType.Conversation.ToString()).ToList();

            if (validScriptList.Count > 0)
            {
                // �ش� Ÿ���� ��ȭ �޼����� ������ ���, �������� �ϳ��� �޼����� ���
                int randomIndex = UnityEngine.Random.Range(0, validScriptList.Count);
                string message = validScriptList[randomIndex]["Message"]?.ToString();

                Debug.Log($"NPC ��ȭ: {message}");
            }
            else
            {
                Debug.LogWarning($"NPC ��ȭ ��ũ��Ʈ�� �������� �ʽ��ϴ�.");

                return; // �ش� ��ȭ ������ ������ �޼ҵ� ����
            }
        }
        else
        {
            Debug.LogWarning("NPC ��ũ��Ʈ�� �����Ǿ� ���� �ʽ��ϴ�.");
        }
    }

    /// <summary>
    /// �÷��̾�� ��ȣ�ۿ��ϴ� �޼ҵ�
    /// </summary>
    [Button]
    public void OnInteract()
    {
        npcScriptUI = UIManager.Instance.NpcScriptUI;

        Debug.Log("NPC ��ȣ�ۿ� ����");

        if (npcScriptUI != null)
        {
            Debug.Log("NPC ��ũ��Ʈ UI üũ");

            npcScriptUI.TargetNpcData = npcData; // ���� NPC ������ ����
            npcScriptUI.Show(); // NPC ��ũ��Ʈ UI ǥ��

            Debug.Log("NPC ��ũ��Ʈ UI ������ �Ҵ� : " + npcScriptUI.TargetNpcData);
        }
        else
        {
            Debug.LogWarning("NPC ��ũ��Ʈ UI�� �����Ǿ� ���� �ʽ��ϴ�.");
        }
    }
}
