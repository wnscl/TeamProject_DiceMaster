using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NpcScriptMenuUI : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect; // ��ũ�� �� ����
    [SerializeField] private RectTransform content; // �޴� ������ ���� ������ ����
    [SerializeField] private GameObject menuItemPrefab; // �޴��� ǥ�õ� ����Ʈ ������
    [SerializeField] private List<string> menuItems = new List<string>();   // �޴��� �� �ؽ�Ʈ ����Ʈ
    private List<TextMeshProUGUI> menuItemTexts = new List<TextMeshProUGUI>();
    private int selectedIndex = 0;  // �÷��̾ ������ �޴��� �ε���

    public List<string> MenuItems { get { return menuItems; } set { menuItems = value; } }

    private void Start()
    {
        // UpdateMenuUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex = Mathf.Max(0, selectedIndex - 1);
            UpdateMenuUI();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex = Mathf.Min(menuItems.Count - 1, selectedIndex + 1);
            UpdateMenuUI();
        }
    }

    private void UpdateMenuUI()
    {
        if (menuItems.Count == 0 || menuItemTexts.Count == 0)
            return;

        for (int i = 0; i < menuItems.Count; i++)
        {
            // ���õ� �ؽ�Ʈ�� ������ ���������� ���� ��, ��ȣ�� �����Ѵ�
            menuItems[i] = $"{(i == selectedIndex ? "<color=red> �� " : "<color=white> - ") + menuItems[i].TrimStart('��', '-', ' ') + "</color>"}";
        }

        // ���õ� �׸��� ��ũ�� ���� �ȿ� �������� �̵�
        RectTransform selectedItem = menuItemTexts[selectedIndex].GetComponent<RectTransform>();

        Canvas.ForceUpdateCanvases(); // ScrollRect ���� ������Ʈ
        scrollRect.content.anchoredPosition = (Vector2)scrollRect.transform.InverseTransformPoint(scrollRect.content.position)
                                            - (Vector2)scrollRect.transform.InverseTransformPoint(selectedItem.position);
    }

    public void InitializeMenu(List<string> menuTexts)
    {
        // ���� �׸� ����
        foreach (Transform child in content)
            Destroy(child.gameObject);

        menuItems.Clear();
        menuItemTexts.Clear();

        // �� �׸� �߰�
        foreach (string text in menuTexts)
        {
            // to do : ����Ʈ �޴��� �����ϰų�, npc�� ��ȣ�ۿ��� �������� NpcScriptUI���� InitializeMenu �޼ҵ带 ȣ���� < ��ȭ, ����Ʈ, �ۺ�
            GameObject textPrefab = Instantiate(menuItemPrefab, content);
            TextMeshProUGUI textComp = textPrefab.GetComponent<TextMeshProUGUI>();
            textComp.text = "- " + text;

            menuItemTexts.Add(textComp);
        }

        selectedIndex = 0;
        UpdateMenuUI();
    }
}
