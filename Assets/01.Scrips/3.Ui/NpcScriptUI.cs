using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        npcScriptMenuUI = GetComponent<NpcScriptMenuUI>();

        if (UIManager.Instance != null)
        {
            UIManager.Instance.NpcScriptUI = this;
        }
        else
        {
            Debug.LogWarning("UIManager�� NpcScriptUI�� ����� �� �����ϴ�.");
        }

        gameObject.SetActive(false); // �ʱ⿡�� ��Ȱ��ȭ
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.anyKeyDown && npcScriptMenuUI.TalkFlag)
        {
            // ��ȭ ���� ���, �ƹ� Ű�� ������ ��ȭ �������� ����
            npcScriptMenuUI.TalkFlag = false; // ��ȭ �÷��׸� ��Ȱ��ȭ

            Show(); // NPC ��ũ��Ʈ UI�� �ٽ� ǥ��
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            npcScriptMenuUI.SelectedIndex = Mathf.Max(0, npcScriptMenuUI.SelectedIndex - 1);
            npcScriptMenuUI.UpdateMenuUI();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            npcScriptMenuUI.SelectedIndex = Mathf.Min(npcScriptMenuUI.MenuItemTexts.Count - 1, npcScriptMenuUI.SelectedIndex + 1);
            npcScriptMenuUI.UpdateMenuUI();
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (npcScriptMenuUI.MenuItemTexts.Count > 0)
            {
                // ������ �ε����� �ش��ϴ� �޴� Ÿ���� ������
                NpcMenuType selectedMenuType = (NpcMenuType)System.Enum.Parse(typeof(NpcMenuType), npcScriptMenuUI.MenuKeys[npcScriptMenuUI.SelectedIndex]);

                OnSelectEvent(selectedMenuType);
            }
        }
    }

    public void Show()
    {
        if (!gameObject.activeSelf)
        {
            // UI�� ��Ȱ��ȭ ������ ���� Ȱ��ȭ
            gameObject.SetActive(true);
        }

        TargetNpcData.ParseNpcScript();

        UpdateNpcInfo(NpcTalkType.Greeting); // ��ȣ�ۿ� �� ó�� ǥ���� ��ȭ�� �λ縻���� ����

        ClearNpcMenuData(); // �޴� ������ �ʱ�ȭ

        // ��ȣ�ۿ���� �⺻ �޴� ����
        targetNpcData.BasicMenuDictionary.Add(NpcMenuType.Talk, "��ȭ");

        if (targetNpcData.ValidQuests.Count > 0)
        {
            targetNpcData.BasicMenuDictionary.Add(NpcMenuType.Quest, "����Ʈ"); // ����Ʈ�� ���� ��� ����Ʈ �޴� �߰�
        }

        targetNpcData.BasicMenuDictionary.Add(NpcMenuType.Farewell, "�ۺ�");

        npcScriptMenuUI.InitializeMenu(targetNpcData.BasicMenuDictionary); // �⺻ �޴��� ǥ��
    }

    public void Hide()
    {
        gameObject.SetActive(false); // UI ��Ȱ��ȭ

        ClearNpcMenuData(); // �޴� ������ �ʱ�ȭ
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

    /// <summary>
    /// �޴��� �������� �� �� �޴��� �°� UI�� ������Ʈ�ϴ� �޼ҵ�
    /// </summary>
    public void OnSelectEvent(NpcMenuType menuType)
    {
        switch (menuType)
        {
            case NpcMenuType.Talk:
                // ��ȭ �޴� ���� �� ��ȭ UI ������Ʈ
                // �޴� �����ۿ� NormalTalk�޼ҵ带 ȣ���� ���� �߰�.
                npcScriptMenuUI.TalkFlag = true; // ��ȭ �÷��׸� Ȱ��ȭ

                npcScriptText.text = targetNpcData.NormalTalk(NpcTalkType.Conversation);
                break;
            case NpcMenuType.Quest:
                // ����Ʈ �޴� ���� �� ����Ʈ UI ������Ʈ
                npcScriptText.text = targetNpcData.NormalTalk(NpcTalkType.Quest);

                break;
            case NpcMenuType.Farewell:
                // �ۺ� �λ� �޴� ���� �� UI ����
                npcScriptText.text = targetNpcData.NormalTalk(NpcTalkType.Farewell);

                Hide(); // UI ��Ȱ��ȭ

                break;
            default:
                Debug.LogWarning("�� �� ���� �޴� Ÿ���Դϴ�.");
                break;
        }
    }

    public void ClearNpcMenuData()
    {
        npcScriptMenuUI.MenuItemTexts.Clear(); // �޴� ������ �ʱ�ȭ
        npcScriptMenuUI.MenuKeys.Clear(); // �޴� Ű �ʱ�ȭ
        npcScriptMenuUI.MenuValues.Clear(); // �޴� �� �ʱ�ȭ
        targetNpcData.BasicMenuDictionary.Clear(); // �⺻ �޴� ��ųʸ� �ʱ�ȭ
    }
}
