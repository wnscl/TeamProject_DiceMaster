using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;
using Unity.VisualScripting;

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
    Quest,   // 퀘스트 관련 대화
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

                    // memo : IsAvailable을 false로 하지 않는 이유 => 수락한 퀘스트도 메뉴에 표시하고, 수락한 퀘스트를 다시 선택할 시 퀘스트 완료시키기 위함
                    quest.IsAccepted = true;

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
    /// 플레이어와 상호작용하는 메소드
    /// </summary>
    public void OnInteract()
    {
        npcScriptUI = UIManager.Instance.NpcScriptUI;
        npcScriptUI.TargetNpc = this; // 현재 NPC 오브젝트 설정

        if (npcScriptUI != null)
        {
            npcScriptUI.TargetNpcData = npcData; // 현재 NPC 데이터 설정
            npcScriptUI.Show(); // NPC 스크립트 UI 표시
        }
        else
        {
            Debug.LogWarning("NPC 스크립트 UI가 설정되어 있지 않습니다.");
        }
    }
}
