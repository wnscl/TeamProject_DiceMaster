using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 전체 퀘스트 관리 매니저
/// </summary>
public class QuestManager : MonoBehaviour
{
    private static QuestManager _instance;
    public static QuestManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("QuestManager").AddComponent<QuestManager>();
            }

            return _instance;
        }
    }

    private List<QuestData> playerQuests; // 현재 진행 중인 퀘스트 목록

    public List<QuestData> PlayerQuests
    {
        get { return playerQuests; }
        set { playerQuests = value; }
    }

    private List<QuestData> completedQuests; // 완료한 퀘스트 목록 ( 퀘스트 보고 전 )
    public List<QuestData> CompletedQuests
    {
        get { return completedQuests; }
        set { completedQuests = value; }
    }

    // to do : 전체 퀘스트를 관리하는 컬렉션을 추가하여 수락 가능 퀘스트, 완료한 퀘스트 등의 정보를 유저가 확인할 수 있도록 구현해볼 것

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);

            playerQuests = new List<QuestData>();
            completedQuests = new List<QuestData>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
