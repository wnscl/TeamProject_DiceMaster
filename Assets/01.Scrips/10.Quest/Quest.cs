using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField] public QuestData questData; // ����Ʈ ������

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    // ����Ʈ �Ϸ� ���� �˻�
    public virtual void CheckCondition()
    {

    }

    public virtual void CompleteQuest()
    {

    }
}
