using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NpcData", menuName = "SoDatas/Npc Data", order = 1)]
public class NpcData : ScriptableObject
{
    [SerializeField] private string npcId; // NPC ID
    [SerializeField] private string npcName; // NPC 이름
    [SerializeField] private string npcDescription; // NPC 설명
    [SerializeField] private List<QuestData> npcQuests; // 해당 NPC가 제공하는 퀘스트 컬렉션
    [SerializeField] private TextAsset npcScripts; // NPC용 대화 스크립트 데이터
    public List<QuestData> NpcQuests { get { return npcQuests; } }

#if UNITY_EDITOR
    // 에디터에서 값이 바뀔 때 자동 호출됨
    private void OnValidate()
    {
        // npcId가 비어있으면 고유 ID를 새로 생성
        if (string.IsNullOrEmpty(npcId))
        {
            npcId = Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this); // 변경사항 저장 표시
        }
    }
#endif
}
