using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;
using UnityEngine.UI;

public class NpcScriptUI : MonoBehaviour
{
    [SerializeField] NpcScriptMenuUI npcScriptMenuUI;   // �޴� UI
    [SerializeField] Image npcIcon; // npc ������ ��������Ʈ
    [SerializeField] TextMeshProUGUI npcNameText; // npc �̸� ǥ�ÿ� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI npcScriptText; // npc ��� ǥ�ÿ� �ؽ�Ʈ
    private NpcData targetNpcData; // �̺�Ʈ ���� NPC ������
    public NpcData TargetNpcData { get { return targetNpcData; } set { targetNpcData = value; } }

    private void Awake()
    {
        UIManager.Instance.NpcScriptUI = this;

        npcScriptMenuUI = GetComponent<NpcScriptMenuUI>();

        gameObject.SetActive(false); // �ʱ⿡�� ��Ȱ��ȭ
        Debug.Log("npc scriptUI setActive : " + gameObject.activeSelf);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    [Button]
    public void Show()
    {
        gameObject.SetActive(true); // UI Ȱ��ȭ
        npcScriptMenuUI = GetComponent<NpcScriptMenuUI>();  // �׽�Ʈ�� npcScriptMenuUI �ʱ�ȭ �ڵ�

        UpdateNpcInfo(NpcTalkType.Greeting); // ��ȣ�ۿ� �� ó�� ǥ���� ��ȭ�� �λ縻���� ����
        npcScriptMenuUI.MenuItems.Clear(); // �޴� ������ �ʱ�ȭ
        npcScriptMenuUI.InitializeMenu(new List<string> { "��ȭ", "����Ʈ", "�ۺ�" }); // �⺻ �޴��� ǥ��
    }

    public void UpdateNpcInfo(NpcTalkType talkType)
    {
        if (TargetNpcData == null)
        {
            Debug.LogError("NPC ������ ��ȿ���� �ʽ��ϴ�.");
            
            return;
        }

        npcIcon.sprite = TargetNpcData.NpcIcon; // NPC ������ ������Ʈ
        npcNameText.text = TargetNpcData.NpcName; // NPC �̸� ������Ʈ
        npcScriptText.text = TargetNpcData.NormalTalk(talkType); // NPC ��ũ��Ʈ �ؽ�Ʈ ������Ʈ
    }

    public void QuestMenu()
    {
        targetNpcData.GetValidQuest();

        // to do : npcData�� ValidQuests�� �̿��Ͽ� npcScriptMenuUI.InitializeMenu �Լ��� ����Ʈ �̸����� ����Ʈ ����
    }
}
