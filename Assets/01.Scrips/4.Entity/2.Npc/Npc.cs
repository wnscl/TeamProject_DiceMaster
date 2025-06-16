using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] private NpcData npcData; // NPC 데이터
    private List<QuestData> validQuests; // 수락 가능한 퀘스트 컬렉션

    void Start()
    {
        validQuests = new List<QuestData>();

        GetValidQuest();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// NPC가 제공하는 퀘스트 중, 플레이어가 수락 가능한 퀘스트를 가져오는 메소드
    /// </summary>
    public void GetValidQuest()
    {
        // to do: 플레이어가 npc와 상호작용을 하고, 퀘스트 메뉴를 수락할 때 이 메소드가 실행되도록 구현할 것
        foreach (QuestData quest in npcData.NpcQuests)
        {
            // 퀘스트가 수락 가능한 상태인지 확인
            // memo : 수락한 퀘스트까지 표시하지만, 완료한 퀘스트는 리스트에서 제거. 수락한 퀘스트의 경우 선택할 시 조건이 만족되어있다면 CompleteQuest 메소드로 이행
            if (quest.IsAvailable && !quest.IsCompleted)
            {
                validQuests.Add(quest);
            }
        }
    }

    /// <summary>
    /// 퀘스트 수락 메소드
    /// </summary>
    /// <param name="data"></param>
    public void AcceptQuest(QuestData data)
    {
        if (validQuests != null)
        {
            foreach (QuestData quest in validQuests)
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
        if (validQuests != null)
        {
            foreach (QuestData quest in validQuests)
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

        // 완료 테스트용 더미 코드
        QuestManager.Instance.PlayerQuests.Remove(validQuests[0]);
        QuestManager.Instance.CompletedQuests.Add(validQuests[0]); // 완료한 퀘스트 목록에 추가

        // to do : 반복 퀘스트가 생길 경우, isAvailable을 true로 변경하는 처리를 추가할 것
        validQuests[0].IsAccepted = false;
        validQuests[0].IsCompleted = true;

        Debug.Log($"더미 테스트 코드 동작. {validQuests[0].QuestName} 퀘스트 완료");
    }

    /// <summary>
    /// 퀘스트 로직 테스트용 메소드
    /// </summary>
    [Button]
    public void TestAcceptQuest()
    {
        // validQuests가 비어 있으면 갱신
        if (validQuests == null || validQuests.Count == 0)
        {
            validQuests = new List<QuestData>();
            GetValidQuest();
        }

        foreach (QuestData quest in validQuests)
        {
            // 매개변수 data가 validQuests에 있는 퀘스트인지, 수락 가능한 퀘스트인지 확인
            if (quest.QuestId == validQuests[0].QuestId && quest.IsAvailable)
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
        foreach (QuestData quest in validQuests)
        {
            // 매개변수 data가 npcData.NpcQuests에 있는 퀘스트인지, 플레이어가 진행중인 퀘스트인지 확인
            if (quest.QuestId == validQuests[0].QuestId && quest.IsAccepted)
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
}
