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
    [SerializeField] private ScrollRect scrollRect; // 스크롤 뷰 영역
    [SerializeField] private RectTransform content; // 메뉴 내용이 들어가는 컨텐츠 영역
    [SerializeField] private GameObject menuItemPrefab; // 메뉴에 표시될 리스트 프리펩
    [SerializeField] GameObject menuUI; // 메뉴 UI 오브젝트
    private List<string> menuKeys = new List<string>();   // 메뉴에 들어갈 텍스트 리스트
    private List<string> menuValues = new List<string>();   // 메뉴에 들어갈 텍스트 리스트
    private List<TextMeshProUGUI> menuItemTexts = new List<TextMeshProUGUI>();
    private NpcData targetNpcData; // 이벤트 중인 NPC 데이터
    private int selectedIndex = 0;  // 플레이어가 선택한 메뉴의 인덱스
    private bool talkFlag = false; // 현재 대화 중일 경우, 방향키의 입력 이벤트를 제어하기 위한 플래그
    private bool isQustMenu = false; // 퀘스트 메뉴인지 여부

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
                // 대화 중일 때는 메뉴 선택을 비활성화
                menuUI.SetActive(false);
            }
            else
            {
                // 대화가 끝나면 메뉴 선택을 활성화
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
            // 선택된 텍스트는 색상을 빨간색으로 변경
            menuItemTexts[i].text = (i == selectedIndex ? "<color=red> - " : "<color=white> - ") + menuValues[i].TrimStart('-', ' ') + "</color>";
        }

        // 선택된 항목이 스크롤 범위 안에 들어오도록 이동
        RectTransform selectedItem = menuItemTexts[selectedIndex].GetComponent<RectTransform>();

        Canvas.ForceUpdateCanvases(); // ScrollRect 강제 업데이트
        scrollRect.content.anchoredPosition = (Vector2)scrollRect.transform.InverseTransformPoint(scrollRect.content.position) - (Vector2)scrollRect.transform.InverseTransformPoint(selectedItem.position);
    }

    /// <summary>
    /// 기본 Npc 상호작용 메뉴 구현 메소드
    /// </summary>
    /// <param name="menuDict"></param>
    public void InitializeMenu(Dictionary<NpcMenuType, string> menuDict)
    {
        // 기존 항목 제거
        foreach (Transform child in content)
            Destroy(child.gameObject);

        ClearItemData();

        // 새 항목 추가
        foreach (var element in menuDict)
        {
            GameObject textPrefab = Instantiate(menuItemPrefab, content);
            TextMeshProUGUI textComp = textPrefab.GetComponent<TextMeshProUGUI>();
            textComp.text = " - " + element.Value;

            menuKeys.Add(element.Key.ToString()); // 키 값만 백업
            menuValues.Add(element.Value.ToString()); // 값만 백업
            menuItemTexts.Add(textComp);    // 텍스트 컴포넌트 저장
        }

        selectedIndex = 0;
        UpdateMenuUI();
    }

    /// <summary>
    /// 퀘스트 메뉴 구현 오버라이드 메소드
    /// </summary>
    /// <param name="questList"></param>
    public void InitializeMenu(List<QuestData> questList)
    {
        // 기존 항목 제거
        foreach (Transform child in content)
            Destroy(child.gameObject);

        ClearItemData();

        // 뒤로가기 버튼을 가장 앞에 추가
        GameObject backBtnPrefab = Instantiate(menuItemPrefab, content);
        TextMeshProUGUI backBtnComp = backBtnPrefab.GetComponent<TextMeshProUGUI>();
        backBtnComp.text = " - 뒤로가기";

        menuValues.Add("뒤로가기");
        menuItemTexts.Add(backBtnComp);

        // 새 항목 추가
        foreach (QuestData quest in questList)
        {
            GameObject textPrefab = Instantiate(menuItemPrefab, content);
            TextMeshProUGUI textComp = textPrefab.GetComponent<TextMeshProUGUI>();
            textComp.text = $" - {quest.QuestName}";

            menuValues.Add($"{quest.QuestName}{(quest.IsAccepted ? " [ 수락 ]" : "")}"); // 메뉴 표시용으로 퀘스트 이름만 저장
            menuItemTexts.Add(textComp);    // 텍스트 컴포넌트 저장
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
