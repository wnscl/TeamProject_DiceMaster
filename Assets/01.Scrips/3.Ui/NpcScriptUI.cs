using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NpcScriptUI : MonoBehaviour
{
    [SerializeField] NpcScriptMenuUI npcScriptMenuUI;   // 메뉴 UI
    [SerializeField] Image npcIcon; // npc 아이콘 스프라이트
    [SerializeField] TextMeshProUGUI npcNameText; // npc 이름 표시용 텍스트
    [SerializeField] TextMeshProUGUI npcScriptText; // npc 대사 표시용 텍스트
    private NpcData targetNpcData; // 이벤트 중인 NPC 데이터
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
            Debug.LogWarning("UIManager에 NpcScriptUI를 등록할 수 없습니다.");
        }

        gameObject.SetActive(false); // 초기에는 비활성화
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.anyKeyDown && npcScriptMenuUI.TalkFlag)
        {
            // 대화 중일 경우, 아무 키를 누르면 대화 시퀀스를 종료
            npcScriptMenuUI.TalkFlag = false; // 대화 플래그를 비활성화

            Show(); // NPC 스크립트 UI를 다시 표시
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
                // 선택한 인덱스에 해당하는 메뉴 타입을 가져옴
                NpcMenuType selectedMenuType = (NpcMenuType)System.Enum.Parse(typeof(NpcMenuType), npcScriptMenuUI.MenuKeys[npcScriptMenuUI.SelectedIndex]);

                OnSelectEvent(selectedMenuType);
            }
        }
    }

    public void Show()
    {
        if (!gameObject.activeSelf)
        {
            // UI가 비활성화 상태일 때만 활성화
            gameObject.SetActive(true);
        }

        TargetNpcData.ParseNpcScript();

        UpdateNpcInfo(NpcTalkType.Greeting); // 상호작용 시 처음 표시할 대화를 인사말으로 설정

        ClearNpcMenuData(); // 메뉴 데이터 초기화

        // 상호작용시의 기본 메뉴 설정
        targetNpcData.BasicMenuDictionary.Add(NpcMenuType.Talk, "대화");

        if (targetNpcData.ValidQuests.Count > 0)
        {
            targetNpcData.BasicMenuDictionary.Add(NpcMenuType.Quest, "퀘스트"); // 퀘스트가 있을 경우 퀘스트 메뉴 추가
        }

        targetNpcData.BasicMenuDictionary.Add(NpcMenuType.Farewell, "작별");

        npcScriptMenuUI.InitializeMenu(targetNpcData.BasicMenuDictionary); // 기본 메뉴를 표시
    }

    public void Hide()
    {
        gameObject.SetActive(false); // UI 비활성화

        ClearNpcMenuData(); // 메뉴 데이터 초기화
    }

    public void UpdateNpcInfo(NpcTalkType talkType)
    {
        if (TargetNpcData == null)
        {
            Debug.LogError("NPC 정보가 유효하지 않습니다.");
            
            return;
        }

        npcIcon.sprite = TargetNpcData.NpcIcon; // NPC 아이콘 업데이트
        npcNameText.text = TargetNpcData.NpcName; // NPC 이름 업데이트
        npcScriptText.text = TargetNpcData.NormalTalk(talkType); // NPC 스크립트 텍스트 업데이트
    }

    public void QuestMenu()
    {
        targetNpcData.GetValidQuest();

        // to do : npcData의 ValidQuests를 이용하여 npcScriptMenuUI.InitializeMenu 함수에 퀘스트 이름으로 리스트 전달
    }

    /// <summary>
    /// 메뉴를 선택했을 때 각 메뉴에 맞게 UI를 업데이트하는 메소드
    /// </summary>
    public void OnSelectEvent(NpcMenuType menuType)
    {
        switch (menuType)
        {
            case NpcMenuType.Talk:
                // 대화 메뉴 선택 시 대화 UI 업데이트
                // 메뉴 아이템에 NormalTalk메소드를 호출해 내용 추가.
                npcScriptMenuUI.TalkFlag = true; // 대화 플래그를 활성화

                npcScriptText.text = targetNpcData.NormalTalk(NpcTalkType.Conversation);
                break;
            case NpcMenuType.Quest:
                // 퀘스트 메뉴 선택 시 퀘스트 UI 업데이트
                npcScriptText.text = targetNpcData.NormalTalk(NpcTalkType.Quest);

                break;
            case NpcMenuType.Farewell:
                // 작별 인사 메뉴 선택 시 UI 종료
                npcScriptText.text = targetNpcData.NormalTalk(NpcTalkType.Farewell);

                Hide(); // UI 비활성화

                break;
            default:
                Debug.LogWarning("알 수 없는 메뉴 타입입니다.");
                break;
        }
    }

    public void ClearNpcMenuData()
    {
        npcScriptMenuUI.MenuItemTexts.Clear(); // 메뉴 아이템 초기화
        npcScriptMenuUI.MenuKeys.Clear(); // 메뉴 키 초기화
        npcScriptMenuUI.MenuValues.Clear(); // 메뉴 값 초기화
        targetNpcData.BasicMenuDictionary.Clear(); // 기본 메뉴 딕셔너리 초기화
    }
}
