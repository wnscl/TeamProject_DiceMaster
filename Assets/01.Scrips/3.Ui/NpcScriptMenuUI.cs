using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NpcScriptMenuUI : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect; // 스크롤 뷰 영역
    [SerializeField] private RectTransform content; // 메뉴 내용이 들어가는 컨텐츠 영역
    [SerializeField] private GameObject menuItemPrefab; // 메뉴에 표시될 리스트 프리펩
    [SerializeField] private List<string> menuItems = new List<string>();   // 메뉴에 들어갈 텍스트 리스트
    private List<TextMeshProUGUI> menuItemTexts = new List<TextMeshProUGUI>();
    private int selectedIndex = 0;  // 플레이어가 선택한 메뉴의 인덱스

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
            // 선택된 텍스트는 색상을 빨간색으로 변경 후, 기호를 변경한다
            menuItems[i] = $"{(i == selectedIndex ? "<color=red> ▶ " : "<color=white> - ") + menuItems[i].TrimStart('▶', '-', ' ') + "</color>"}";
        }

        // 선택된 항목이 스크롤 범위 안에 들어오도록 이동
        RectTransform selectedItem = menuItemTexts[selectedIndex].GetComponent<RectTransform>();

        Canvas.ForceUpdateCanvases(); // ScrollRect 강제 업데이트
        scrollRect.content.anchoredPosition = (Vector2)scrollRect.transform.InverseTransformPoint(scrollRect.content.position)
                                            - (Vector2)scrollRect.transform.InverseTransformPoint(selectedItem.position);
    }

    public void InitializeMenu(List<string> menuTexts)
    {
        // 기존 항목 제거
        foreach (Transform child in content)
            Destroy(child.gameObject);

        menuItems.Clear();
        menuItemTexts.Clear();

        // 새 항목 추가
        foreach (string text in menuTexts)
        {
            // to do : 퀘스트 메뉴를 선택하거나, npc와 상호작용한 시점에서 NpcScriptUI에서 InitializeMenu 메소드를 호출해 < 대화, 퀘스트, 작별
            GameObject textPrefab = Instantiate(menuItemPrefab, content);
            TextMeshProUGUI textComp = textPrefab.GetComponent<TextMeshProUGUI>();
            textComp.text = "- " + text;

            menuItemTexts.Add(textComp);
        }

        selectedIndex = 0;
        UpdateMenuUI();
    }
}
