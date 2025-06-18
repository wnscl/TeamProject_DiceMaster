using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
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
        UIManager.Instance.NpcScriptUI = this;

        npcScriptMenuUI = GetComponent<NpcScriptMenuUI>();

        gameObject.SetActive(false); // 초기에는 비활성화
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
        gameObject.SetActive(true); // UI 활성화
        npcScriptMenuUI = GetComponent<NpcScriptMenuUI>();  // 테스트용 npcScriptMenuUI 초기화 코드

        UpdateNpcInfo(NpcTalkType.Greeting); // 상호작용 시 처음 표시할 대화를 인사말으로 설정
        npcScriptMenuUI.MenuItems.Clear(); // 메뉴 아이템 초기화
        npcScriptMenuUI.InitializeMenu(new List<string> { "대화", "퀘스트", "작별" }); // 기본 메뉴를 표시
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
}
