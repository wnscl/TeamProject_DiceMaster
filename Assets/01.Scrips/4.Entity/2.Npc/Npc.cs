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
    private List<QuestData> validQuests; // ���� ������ ����Ʈ �÷���
    private JToken parseNpcScript; // �Ľ̵� NPC ��ũ��Ʈ ������

    void Start()
    {
        validQuests = new List<QuestData>();

        GetValidQuest();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// NPC�� �����ϴ� ����Ʈ ��, �÷��̾ ���� ������ ����Ʈ�� �������� �޼ҵ�
    /// </summary>
    public void GetValidQuest()
    {
        // to do: �÷��̾ npc�� ��ȣ�ۿ��� �ϰ�, ����Ʈ �޴��� ������ �� �� �޼ҵ尡 ����ǵ��� ������ ��
        foreach (QuestData quest in npcData.NpcQuests)
        {
            // ����Ʈ�� ���� ������ �������� Ȯ��
            // memo : ������ ����Ʈ���� ǥ��������, �Ϸ��� ����Ʈ�� ����Ʈ���� ����. ������ ����Ʈ�� ��� ������ �� ������ �����Ǿ��ִٸ� CompleteQuest �޼ҵ�� ����
            if (quest.IsAvailable && !quest.IsCompleted)
            {
                validQuests.Add(quest);
            }
        }
    }

    /// <summary>
    /// ����Ʈ ���� �޼ҵ�
    /// </summary>
    /// <param name="data"></param>
    public void AcceptQuest(QuestData data)
    {
        if (validQuests != null)
        {
            foreach (QuestData quest in validQuests)
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
        if (validQuests != null)
        {
            foreach (QuestData quest in validQuests)
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

        // �Ϸ� �׽�Ʈ�� ���� �ڵ�
        QuestManager.Instance.PlayerQuests.Remove(validQuests[0]);
        QuestManager.Instance.CompletedQuests.Add(validQuests[0]); // �Ϸ��� ����Ʈ ��Ͽ� �߰�

        // to do : �ݺ� ����Ʈ�� ���� ���, isAvailable�� true�� �����ϴ� ó���� �߰��� ��
        validQuests[0].IsAccepted = false;
        validQuests[0].IsCompleted = true;

        Debug.Log($"���� �׽�Ʈ �ڵ� ����. {validQuests[0].QuestName} ����Ʈ �Ϸ�");
    }

    /// <summary>
    /// NPC ��ȭ ���� �޼ҵ�
    /// </summary>
    /// <param name="talkType"></param>
    public void NormalTalk(NpcTalkType talkType)
    {
        if (npcData.NpcScripts != null)
        {
            JToken normalTalk = parseNpcScript["Normal"]["Conversation"];   // Normal Ÿ���� ��ȭ ��ũ��Ʈ

            // �μ��� ���� ��ȭ Ÿ���� �޼����� ����Ʈȭ
            List<JToken> validScriptList = normalTalk.Where( t => t["Type"].ToString() == talkType.ToString()).ToList();

            if (validScriptList.Count > 0)
            {
                // �ش� Ÿ���� ��ȭ �޼����� ������ ���, �������� �ϳ��� �޼����� ���
                int randomIndex = UnityEngine.Random.Range(0, validScriptList.Count);
                string message = validScriptList[randomIndex]["Message"]?.ToString();

                Debug.Log($"NPC ��ȭ: {message}");
            }
            else
            {
                Debug.LogWarning($"NPC ��ȭ ��ũ��Ʈ�� �������� �ʽ��ϴ�. ��ȭ ����: {talkType}");

                return; // �ش� ��ȭ ������ ������ �޼ҵ� ����
            }
        }
        else
        {
            Debug.LogWarning($"NPC ��ȭ ��ũ��Ʈ�� �������� �ʽ��ϴ�. ��ȭ ����: {talkType}");
        }
    }

    /// <summary>
    /// NPC ��ũ��Ʈ ������ �Ľ� �޼ҵ�
    /// memo : ��ȭ�� ������ ��ũ��Ʈ�� �Ľ��ϴ� ���� ��ȿ�����̹Ƿ�, ���� ��ȭ �� �� ���� �Ľ��ϵ��� ����
    /// </summary>
    public void ParseNpcScript()
    {
        parseNpcScript = JToken.Parse(npcData.NpcScripts.text);
    }

    /// <summary>
    /// ����Ʈ ���� �׽�Ʈ�� �޼ҵ�
    /// </summary>
    [Button]
    public void TestAcceptQuest()
    {
        // validQuests�� ��� ������ ����
        if (validQuests == null || validQuests.Count == 0)
        {
            validQuests = new List<QuestData>();
            GetValidQuest();
        }

        foreach (QuestData quest in validQuests)
        {
            // �Ű����� data�� validQuests�� �ִ� ����Ʈ����, ���� ������ ����Ʈ���� Ȯ��
            if (quest.QuestId == validQuests[0].QuestId && quest.IsAvailable)
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
        foreach (QuestData quest in validQuests)
        {
            // �Ű����� data�� npcData.NpcQuests�� �ִ� ����Ʈ����, �÷��̾ �������� ����Ʈ���� Ȯ��
            if (quest.QuestId == validQuests[0].QuestId && quest.IsAccepted)
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
        ParseNpcScript();

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
    public void OnInteract()
    {
        
    }
}
