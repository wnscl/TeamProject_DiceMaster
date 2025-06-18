using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 골드를 모아오면 클리어되는 퀘스트
/// </summary>
public class GoldQuest : Quest
{
    [SerializeField] int requireGold; // 퀘스트 완료에 필요한 골드

    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    /// <summary>
    /// 퀘스트 완료 조건을 검사하는 메소드
    /// 요구치만큼 골드를 모았는지 확인하고, 모았다면 퀘스트를 완료
    /// </summary>
    public override void CheckCondition()
    {
        base.CheckCondition();

        int playerGold = GameManager.Instance.player.statHandler.GetStat(StatType.Money);   // 현재 플레이어 골드

        Debug.Log($"현재 플레이어 골드: {playerGold}, 요구 골드: {requireGold}");

        if (playerGold >= requireGold)
        {
            GameManager.Instance.player.statHandler.ModifyStat(StatType.Money, -requireGold);   // 플레이어 골드를 요구치만큼 감소

            CompleteQuest();
        }
        else
        {
            Debug.Log("퀘스트 조건 미충족: 골드 부족");
            // to do : UI에 퀘스트 실패 메세지 출력할 것
        }
    }

    public override void CompleteQuest()
    {
        base.CompleteQuest();

        foreach (QuestData item in QuestManager.Instance.PlayerQuests)
        {
            if (item.QuestId == questData.QuestId && questData.IsAccepted)
            {
                questData.IsCompleted = true; // 퀘스트 완료 상태로 변경
                item.IsCompleted = true; // 퀘스트 완료 상태로 변경 / to do : 같은 요소인 IsCompleted 통일할 필요 있음

                QuestManager.Instance.PlayerQuests.Remove(item);    // 플레이어 퀘스트 리스트에서 퀘스트 제거
                QuestManager.Instance.CompletedQuests.Add(item);    // 완료한 퀘스트 목록에 추가

                Debug.Log($"{item.QuestName} 퀘스트 완료");
                break;
            }
        }
    }
}
