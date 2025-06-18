using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Linq;

[CreateAssetMenu(fileName = "NpcData", menuName = "SoDatas/Npc Data", order = 1)]
public class NpcData : ScriptableObject
{
    [SerializeField] private string npcId; // NPC ID
    [SerializeField] private Sprite npcIcon; // NPC 아이콘
    [SerializeField] private string npcName; // NPC 이름
    [SerializeField] private string npcDescription; // NPC 설명
    [SerializeField] private List<QuestData> npcQuests; // 해당 NPC가 제공하는 퀘스트 컬렉션
    [SerializeField] private TextAsset npcScripts; // NPC용 대화 스크립트 데이터
    [SerializeField] private Dictionary<NpcMenuType, string> basicMenuDictionary = new Dictionary<NpcMenuType, string>();
    private List<QuestData> validQuests = new List<QuestData>(); // 수락 가능한 퀘스트 컬렉션
    private JToken parsedScript; // 파싱된 NPC 스크립트 데이터

    public Sprite NpcIcon { get { return npcIcon; } }
    public string NpcName { get { return npcName; } }
    public List<QuestData> NpcQuests { get { return npcQuests; } }
    public TextAsset NpcScripts { get { return npcScripts; } }
    public Dictionary<NpcMenuType, string> BasicMenuDictionary { get { return basicMenuDictionary; } }
    public List<QuestData> ValidQuests { get { return validQuests; } }
    public JToken ParsedScript { get { return parsedScript; } }

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

    /// <summary>
    /// NPC가 제공하는 퀘스트 중, 플레이어가 수락 가능한 퀘스트를 가져오는 메소드
    /// </summary>
    public void GetValidQuest()
    {
        validQuests.Clear(); // 기존 퀘스트 목록 초기화

        // to do: 플레이어가 npc와 상호작용을 하고, 퀘스트 메뉴를 수락할 때 이 메소드가 실행되도록 구현할 것
        foreach (QuestData quest in NpcQuests)
        {
            // 퀘스트가 수락 가능한 상태인지 확인
            // memo : 수락한 퀘스트까지 표시하지만, 완료한 퀘스트는 리스트에서 제거. 수락한 퀘스트의 경우 선택할 시 조건이 만족되어있다면 CompleteQuest 메소드로 이행
            if (quest.IsAvailable && !quest.IsCompleted)
            {
                validQuests.Add(quest);
            }
        }
    }

    public void ParseNpcScript()
    {
        parsedScript = JToken.Parse(npcScripts.text);
    }

    /// <summary>
    /// NPC 대화 로직 메소드
    /// </summary>
    /// <param name="talkType"></param>
    public string NormalTalk(NpcTalkType talkType)
    {
        string message = "";

        if (npcScripts != null)
        {
            JToken normalTalk = parsedScript["Normal"]["Conversation"];   // Normal 타입의 대화 스크립트

            // 인수로 받은 대화 타입의 메세지를 리스트화
            List<JToken> validScriptList = normalTalk.Where(t => t["Type"].ToString() == talkType.ToString()).ToList();

            if (validScriptList.Count > 0)
            {
                // 해당 타입의 대화 메세지가 존재할 경우, 랜덤으로 하나의 메세지를 출력
                int randomIndex = UnityEngine.Random.Range(0, validScriptList.Count);
                message = validScriptList[randomIndex]["Message"]?.ToString();
            }
            else
            {
                Debug.LogWarning($"NPC 대화 스크립트가 존재하지 않습니다. 대화 유형: {talkType}");
            }
        }
        else
        {
            Debug.LogWarning($"NPC 대화 스크립트가 존재하지 않습니다. 대화 유형: {talkType}");
        }

        return message;
    }
}
