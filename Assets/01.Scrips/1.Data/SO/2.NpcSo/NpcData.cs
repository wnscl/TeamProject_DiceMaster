using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Linq;

[CreateAssetMenu(fileName = "NpcData", menuName = "SoDatas/Npc Data", order = 1)]
public class NpcData : ScriptableObject
{
    [SerializeField] private string npcId; // NPC ID
    [SerializeField] private Sprite npcIcon; // NPC ������
    [SerializeField] private string npcName; // NPC �̸�
    [SerializeField] private string npcDescription; // NPC ����
    [SerializeField] private List<QuestData> npcQuests; // �ش� NPC�� �����ϴ� ����Ʈ �÷���
    [SerializeField] private TextAsset npcScripts; // NPC�� ��ȭ ��ũ��Ʈ ������
    [SerializeField] private Dictionary<NpcMenuType, string> basicMenuDictionary = new Dictionary<NpcMenuType, string>();
    private List<QuestData> validQuests = new List<QuestData>(); // ���� ������ ����Ʈ �÷���
    private JToken parsedScript; // �Ľ̵� NPC ��ũ��Ʈ ������

    public Sprite NpcIcon { get { return npcIcon; } }
    public string NpcName { get { return npcName; } }
    public List<QuestData> NpcQuests { get { return npcQuests; } }
    public TextAsset NpcScripts { get { return npcScripts; } }
    public Dictionary<NpcMenuType, string> BasicMenuDictionary { get { return basicMenuDictionary; } }
    public List<QuestData> ValidQuests { get { return validQuests; } }
    public JToken ParsedScript { get { return parsedScript; } }

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

    /// <summary>
    /// NPC�� �����ϴ� ����Ʈ ��, �÷��̾ ���� ������ ����Ʈ�� �������� �޼ҵ�
    /// </summary>
    public void GetValidQuest()
    {
        validQuests.Clear(); // ���� ����Ʈ ��� �ʱ�ȭ

        // to do: �÷��̾ npc�� ��ȣ�ۿ��� �ϰ�, ����Ʈ �޴��� ������ �� �� �޼ҵ尡 ����ǵ��� ������ ��
        foreach (QuestData quest in NpcQuests)
        {
            // ����Ʈ�� ���� ������ �������� Ȯ��
            // memo : ������ ����Ʈ���� ǥ��������, �Ϸ��� ����Ʈ�� ����Ʈ���� ����. ������ ����Ʈ�� ��� ������ �� ������ �����Ǿ��ִٸ� CompleteQuest �޼ҵ�� ����
            if (quest.IsAvailable && !quest.IsCompleted)
            {
                validQuests.Add(quest);
            }
        }
    }

    public void ParseNpcScript()
    {
        parsedScript = JToken.Parse(npcScripts.text);
    }

    /// <summary>
    /// NPC ��ȭ ���� �޼ҵ�
    /// </summary>
    /// <param name="talkType"></param>
    public string NormalTalk(NpcTalkType talkType)
    {
        string message = "";

        if (npcScripts != null)
        {
            JToken normalTalk = parsedScript["Normal"]["Conversation"];   // Normal Ÿ���� ��ȭ ��ũ��Ʈ

            // �μ��� ���� ��ȭ Ÿ���� �޼����� ����Ʈȭ
            List<JToken> validScriptList = normalTalk.Where(t => t["Type"].ToString() == talkType.ToString()).ToList();

            if (validScriptList.Count > 0)
            {
                // �ش� Ÿ���� ��ȭ �޼����� ������ ���, �������� �ϳ��� �޼����� ���
                int randomIndex = UnityEngine.Random.Range(0, validScriptList.Count);
                message = validScriptList[randomIndex]["Message"]?.ToString();
            }
            else
            {
                Debug.LogWarning($"NPC ��ȭ ��ũ��Ʈ�� �������� �ʽ��ϴ�. ��ȭ ����: {talkType}");
            }
        }
        else
        {
            Debug.LogWarning($"NPC ��ȭ ��ũ��Ʈ�� �������� �ʽ��ϴ�. ��ȭ ����: {talkType}");
        }

        return message;
    }
}
