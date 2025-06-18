using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField] public QuestData questData; // 퀘스트 데이터

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    // 퀘스트 완료 조건 검사
    public virtual void CheckCondition()
    {

    }

    public virtual void CompleteQuest()
    {

    }
}
