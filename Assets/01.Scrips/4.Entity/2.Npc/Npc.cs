using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;

/// <summary>
/// 플레이어와의 상호작용을 정의하는 인터페이스
/// </summary>
public interface IInteractable
{
    void OnInteract();  // 플레이어와 상호작용하는 메소드
}

/// <summary>
/// NPC 대화 유형을 정의
/// </summary>
public enum NpcTalkType
{
    Greeting,   // 인사말
    Conversation,   // 회화
    Farewell,   // 작별 인사
    AskQuest,   // 퀘스트 수락 요청
    AcceptQuest,    // 퀘스트 수락 시
    CompleteQuest,  // 퀘스트 완료 시
    IncompleteQuest // 퀘스트 미완료 시 해당 퀘스트 대화 요청
}

public class Npc : MonoBehaviour, IInteractable
{
    [SerializeField] private NpcData npcData; // NPC 데이터
    private JToken parseNpcScript; // 파싱된 NPC 스크립트 데이터
    private NpcScriptUI npcScriptUI; // NPC 스크립트 UI

    void Start()
    {

    }

    void Update()
    {
        
    }

    /// <summary>
    /// 퀘스트 수락 메소드
    /// </summary>
    /// <param name="data"></param>
    public void AcceptQuest(QuestData data)
    {
        if (npcData.ValidQuests != null)
        {
            foreach (QuestData quest in npcData.ValidQuests)
            {
                // 매개변수 data가 validQuests에 있는 퀘스트인지, 수락 가능한 퀘스트인지 확인
                if (quest.QuestId == data.QuestId && quest.IsAvailable)
                {
                    // 해당 퀘스트가 유효할 경우, 플레이어 퀘스트 리스트에 퀘스트 추가
                    QuestManager.Instance.PlayerQuests.Add(quest);

                    quest.IsAccepted = true;
                    quest.IsAvailable = false;  // 퀘스트 복수 수락 방지 플래그 활성화

                    Debug.Log($"{quest.QuestName} 퀘스트 수락");

                    return;
                }
            }
        }
        else
        {
            Debug.LogWarning("수락할 수 있는 퀘스트가 없습니다.");
        }
    }

    /// <summary>
    /// 퀘스트 완료 메소드
    /// </summary>
    /// <param name="data"></param>
    public void CompleteQuest(QuestData data)
    {
        if (npcData.ValidQuests != null)
        {
            foreach (QuestData quest in npcData.ValidQuests)
            {
                // 매개변수 data가 npcData.NpcQuests에 있는 퀘스트인지, 플레이어가 진행중인 퀘스트인지 확인
                if (quest.QuestId == data.QuestId && quest.IsAccepted)
                {
                    // 해당 퀘스트가 유효할 경우, 플레이어 퀘스트 리스트에서 퀘스트 제거
                    QuestManager.Instance.PlayerQuests.Remove(quest);
                    QuestManager.Instance.CompletedQuests.Add(quest); // 완료한 퀘스트 목록에 추가

                    // to do : 반복 퀘스트가 생길 경우, isAvailable을 true로 변경하는 처리를 추가할 것
                    quest.IsAccepted = false;
                    quest.IsCompleted = true;

                    Debug.Log($"{quest.QuestName} 퀘스트 완료");

                    return;
                }
            }
        }
        else
        {
            Debug.LogWarning("유효하지 않은 퀘스트입니다.");
        }
    }

    /// <summary>
    /// 퀘스트 로직 테스트용 메소드
    /// </summary>
    [Button]
    public void TestAcceptQuest()
    {
        // validQuests가 비어 있으면 갱신
        if (npcData.ValidQuests == null || npcData.ValidQuests.Count == 0)
        {
            npcData.GetValidQuest();
        }

        foreach (QuestData quest in npcData.ValidQuests)
        {
            // 매개변수 data가 validQuests에 있는 퀘스트인지, 수락 가능한 퀘스트인지 확인
            if (quest.QuestId == npcData.ValidQuests[0].QuestId && quest.IsAvailable)
            {
                // 해당 퀘스트가 유효할 경우, 플레이어 퀘스트 리스트에 퀘스트 추가
                QuestManager.Instance.PlayerQuests.Add(quest);

                Debug.Log("퀘스트 추가: " + QuestManager.Instance.PlayerQuests);

                quest.IsAccepted = true;
                quest.IsAvailable = false;  // 퀘스트 복수 수락 방지 플래그 활성화

                Debug.Log($"더미 테스트 코드 동작. < {quest.QuestName} > 퀘스트 수락");

                return;
            }
        }
    }

    [Button]
    public void TestCompleteQuest()
    {
        foreach (QuestData quest in npcData.ValidQuests)
        {
            // 매개변수 data가 npcData.NpcQuests에 있는 퀘스트인지, 플레이어가 진행중인 퀘스트인지 확인
            if (quest.QuestId == npcData.ValidQuests[0].QuestId && quest.IsAccepted)
            {
                // 해당 퀘스트가 유효할 경우, 플레이어 퀘스트 리스트에서 퀘스트 제거
                QuestManager.Instance.PlayerQuests.Remove(quest);
                QuestManager.Instance.CompletedQuests.Add(quest); // 완료한 퀘스트 목록에 추가

                // to do : 반복 퀘스트가 생길 경우, isAvailable을 true로 변경하는 처리를 추가할 것
                quest.IsAccepted = false;
                quest.IsCompleted = true;

                Debug.Log($"더미 테스트 코드 동작. < {quest.QuestName} > 퀘스트 완료");

                return;
            }
        }
    }

    [Button]
    public void TestNormalTalk()
    {
        // 테스트를 위한 NPC 스크립트 파싱
        npcData.ParseNpcScript();

        if (npcData.NpcScripts != null)
        {
            JToken normalTalk = parseNpcScript["Normal"]["Conversation"];   // Normal 타입의 대화 스크립트

            // 회화 ( Conversation ) 타입의 대화 반환
            List<JToken> validScriptList = normalTalk.Where(t => t["Type"].ToString() == NpcTalkType.Conversation.ToString()).ToList();

            if (validScriptList.Count > 0)
            {
                // 해당 타입의 대화 메세지가 존재할 경우, 랜덤으로 하나의 메세지를 출력
                int randomIndex = UnityEngine.Random.Range(0, validScriptList.Count);
                string message = validScriptList[randomIndex]["Message"]?.ToString();

                Debug.Log($"NPC 대화: {message}");
            }
            else
            {
                Debug.LogWarning($"NPC 대화 스크립트가 존재하지 않습니다.");

                return; // 해당 대화 유형이 없으면 메소드 종료
            }
        }
        else
        {
            Debug.LogWarning("NPC 스크립트가 설정되어 있지 않습니다.");
        }
    }

    /// <summary>
    /// 플레이어와 상호작용하는 메소드
    /// </summary>
    [Button]
    public void OnInteract()
    {
        npcScriptUI = UIManager.Instance.NpcScriptUI;

        Debug.Log("NPC 상호작용 시작");

        if (npcScriptUI != null)
        {
            Debug.Log("NPC 스크립트 UI 체크");

            npcScriptUI.TargetNpcData = npcData; // 현재 NPC 데이터 설정
            npcScriptUI.Show(); // NPC 스크립트 UI 표시

            Debug.Log("NPC 스크립트 UI 데이터 할당 : " + npcScriptUI.TargetNpcData);
        }
        else
        {
            Debug.LogWarning("NPC 스크립트 UI가 설정되어 있지 않습니다.");
        }
    }
}
