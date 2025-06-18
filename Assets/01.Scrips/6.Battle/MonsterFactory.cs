using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : MonoBehaviour
{
    [SerializeField] private GameObject[] monsters;

    [SerializeField] private GameObject monsterPivot;

    [SerializeField] private GameObject newMonster;

    private void Start()
    {
        GameManager.Instance.battleEvent += SettingMonster;
    }
    public void SettingMonster(bool isBattleStart)
    {
        if (!isBattleStart)
        {
            newMonster = null;
            return;
        }

        newMonster = Instantiate(monsters[0]);
        SetMonsterToSkillManager();
        SetMonsterPosition();
        SetMonsterToCondition();
    }
    private void SetMonsterToSkillManager()
    {
        SkillManager.instance.TestMonster = newMonster;
    }
    private void SetMonsterToCondition()
    {
        MonsterInfo newMonsterInfo = newMonster.GetComponent<MonsterInfo>();
        ConditionCollection.instance.GetMonster(newMonsterInfo);
    }
    private void SetMonsterPosition()
    {
        newMonster.transform.parent = monsterPivot.transform;
        newMonster.transform.position = monsterPivot.transform.position;
    }
}
