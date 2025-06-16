using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    Collection, // 수집 퀘스트
    Battle,     // 처치 퀘스트
    Exploration // 탐험 퀘스트
}

[CreateAssetMenu(fileName = "QuestData", menuName = "SoDatas/Quest Data", order = 1)]

public class QuestData : ScriptableObject
{
    /*
     * [ to do list ]
     * 뭔가의 퀘스트 트리거가 발동했을 때, 다른 퀘스트를 활성화 시키고 싶으면 전체 퀘스트 리스트에서 questId를 통해 해당 퀘스트를 찾아 활성화
     * 플레이어 레벨에 맞춘 퀘스트의 경우, npc에서 플레이어 레벨에 맞는 퀘스트를 탐지할 예정
     * 상호작용이 퀘스트 완료 조건일 경우, 델리게이트를 통해 상호작용한 오브젝트 쪽에서 퀘스트 쪽의 델리게이트를 활성화 시켜서 퀘스트 클리어
     * ex ) Action QuestFlags 함수 존재, 복수의 오브젝트와 상호작용해야함, 오브젝트를 활성화 시킬 때 마다 QuestFlags 델리게이트 호출, Count++ 처리, 퀘스트 쪽에서는 Count가 얼마일 때 퀘스트 완료 처리를 할 것 정도만 구현
     */

    [Header("Quest Info")]
    [SerializeField] private string questId; // 퀘스트 고유 ID * memo : 현재 사용처 없음, 퀘스트 탐색 로직 추가 예정
    [SerializeField] private QuestType questType;
    [SerializeField] private string questName;   // 표시될 퀘스트 이름
    [SerializeField] private string questDescription;    // 간략하게 표시할 퀘스트 설명
    [SerializeField] private List<ItemData> questObjectives; // 수집 퀘스트일 경우, 퀘스트 목표 아이템 컬렉션
    public string QuestId { get { return questId; } }
    public string QuestName {get { return questName; } }
    public string QuestDescription { get { return questDescription; } }
    public List<ItemData> QuestObjectives { get { return questObjectives; } }

    [Header("Quest Rewards")]
    [SerializeField] private int questRewardExp;
    [SerializeField] private int questRewardGold;
    [SerializeField] private List<ItemData> questRewards;    // 퀘스트 완료 보상 아이템 컬렉션
    public int QuestRewardExp { get { return questRewardExp; } }
    public int QuestRewardGold { get { return questRewardGold; } }
    public List<ItemData> QuestRewards { get { return questRewards; } }

    [Header("Quest Status")]
    [SerializeField] private bool isAvailable; // 퀘스트 수락 가능 여부
    [SerializeField] private bool isAccepted;  // 퀘스트 수락 여부
    [SerializeField] private bool isCompleted;  // 퀘스트 완료 여부
    public bool IsAvailable
    {
        get { return isAvailable; }
        set { isAvailable = value; }
    }
    public bool IsAccepted
    {
        get { return isAccepted; }
        set { isAccepted = value; }
    }
    public bool IsCompleted
    {
        get { return isCompleted; }
        set { isCompleted = value; }
    }

#if UNITY_EDITOR
    // 에디터에서 값이 바뀔 때 자동 호출됨
    private void OnValidate()
    {
        // questId가 비어있으면 고유 ID를 새로 생성
        if (string.IsNullOrEmpty(questId))
        {
            questId = Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this); // 변경사항 저장 표시
        }
    }
#endif
}
