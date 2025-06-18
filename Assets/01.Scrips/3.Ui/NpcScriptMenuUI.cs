using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum NpcMenuType
{
    Talk,
    Quest,
    Farewell
}

public class NpcScriptMenuUI : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect; // ��ũ�� �� ����
    [SerializeField] private RectTransform content; // �޴� ������ ���� ������ ����
    [SerializeField] private GameObject menuItemPrefab; // �޴��� ǥ�õ� ����Ʈ ������
    [SerializeField] GameObject menuUI; // �޴� UI ������Ʈ
    private List<string> menuKeys = new List<string>();   // �޴��� �� �ؽ�Ʈ ����Ʈ
    private List<string> menuValues = new List<string>();   // �޴��� �� �ؽ�Ʈ ����Ʈ
    private List<TextMeshProUGUI> menuItemTexts = new List<TextMeshProUGUI>();
    private NpcData targetNpcData; // �̺�Ʈ ���� NPC ������
    private int selectedIndex = 0;  // �÷��̾ ������ �޴��� �ε���
    private bool talkFlag = false; // ���� ��ȭ ���� ���, ����Ű�� �Է� �̺�Ʈ�� �����ϱ� ���� �÷���
    private bool isQustMenu = false; // ����Ʈ �޴����� ����

    public List<string> MenuKeys { get { return menuKeys; } set { menuKeys = value; } }
    public List<string> MenuValues { get { return menuValues; } set { menuValues = value; } }
    public List<TextMeshProUGUI> MenuItemTexts { get { return menuItemTexts; } set { menuItemTexts = value; } }
    public NpcData TargetNpcData { get { return targetNpcData; } set { targetNpcData = value; } }
    public int SelectedIndex { get { return selectedIndex; } set { selectedIndex = value; } }
    public bool TalkFlag { get { return talkFlag; }
        set
        {
            talkFlag = value;

            if (talkFlag)
            {
                // ��ȭ ���� ���� �޴� ������ ��Ȱ��ȭ
                menuUI.SetActive(false);
            }
            else
            {
                // ��ȭ�� ������ �޴� ������ Ȱ��ȭ
                menuUI.SetActive(true);
            }
        }
    }
    public bool IsQuestMenu { get { return isQustMenu; } set { isQustMenu = value; } }

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void UpdateMenuUI()
    {
        if (menuItemTexts.Count == 0)
            return;

        for (int i = 0; i < menuItemTexts.Count; i++)
        {
            // ���õ� �ؽ�Ʈ�� ������ ���������� ����
            menuItemTexts[i].text = (i == selectedIndex ? "<color=red> - " : "<color=white> - ") + menuValues[i].TrimStart('-', ' ') + "</color>";
        }

        // ���õ� �׸��� ��ũ�� ���� �ȿ� �������� �̵�
        RectTransform selectedItem = menuItemTexts[selectedIndex].GetComponent<RectTransform>();

        Canvas.ForceUpdateCanvases(); // ScrollRect ���� ������Ʈ
        scrollRect.content.anchoredPosition = (Vector2)scrollRect.transform.InverseTransformPoint(scrollRect.content.position) - (Vector2)scrollRect.transform.InverseTransformPoint(selectedItem.position);
    }

    /// <summary>
    /// �⺻ Npc ��ȣ�ۿ� �޴� ���� �޼ҵ�
    /// </summary>
    /// <param name="menuDict"></param>
    public void InitializeMenu(Dictionary<NpcMenuType, string> menuDict)
    {
        // ���� �׸� ����
        foreach (Transform child in content)
            Destroy(child.gameObject);

        ClearItemData();

        // �� �׸� �߰�
        foreach (var element in menuDict)
        {
            GameObject textPrefab = Instantiate(menuItemPrefab, content);
            TextMeshProUGUI textComp = textPrefab.GetComponent<TextMeshProUGUI>();
            textComp.text = " - " + element.Value;

            menuKeys.Add(element.Key.ToString()); // Ű ���� ���
            menuValues.Add(element.Value.ToString()); // ���� ���
            menuItemTexts.Add(textComp);    // �ؽ�Ʈ ������Ʈ ����
        }

        selectedIndex = 0;
        UpdateMenuUI();
    }

    /// <summary>
    /// ����Ʈ �޴� ���� �������̵� �޼ҵ�
    /// </summary>
    /// <param name="questList"></param>
    public void InitializeMenu(List<QuestData> questList)
    {
        // ���� �׸� ����
        foreach (Transform child in content)
            Destroy(child.gameObject);

        ClearItemData();

        // �ڷΰ��� ��ư�� ���� �տ� �߰�
        GameObject backBtnPrefab = Instantiate(menuItemPrefab, content);
        TextMeshProUGUI backBtnComp = backBtnPrefab.GetComponent<TextMeshProUGUI>();
        backBtnComp.text = " - �ڷΰ���";

        menuValues.Add("�ڷΰ���");
        menuItemTexts.Add(backBtnComp);

        // �� �׸� �߰�
        foreach (QuestData quest in questList)
        {
            GameObject textPrefab = Instantiate(menuItemPrefab, content);
            TextMeshProUGUI textComp = textPrefab.GetComponent<TextMeshProUGUI>();
            textComp.text = $" - {quest.QuestName}";

            menuValues.Add($"{quest.QuestName}{(quest.IsAccepted ? " [ ���� ]" : "")}"); // �޴� ǥ�ÿ����� ����Ʈ �̸��� ����
            menuItemTexts.Add(textComp);    // �ؽ�Ʈ ������Ʈ ����
        }

        selectedIndex = 0;
        UpdateMenuUI();
    }

    public void ClearItemData()
    {
        menuKeys.Clear();
        menuValues.Clear();
        menuItemTexts.Clear();
    }
}
