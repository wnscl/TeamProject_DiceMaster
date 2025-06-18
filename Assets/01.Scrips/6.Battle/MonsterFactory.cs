using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : MonoBehaviour
{
    [SerializeField] private GameObject[] monsters;

    [SerializeField] private GameObject monsterPivot;

    private void Start()
    {
        GameManager.Instance.battleEvent += CreateMonster;
    }
    public void CreateMonster()
    {
        GameObject newMonster = Instantiate(monsters[0]);
        SetMonsterToSkillManager(newMonster);
        SetMonsterPosition(newMonster);
        MonsterInfo newMonsterInfo = newMonster.GetComponent<MonsterInfo>();
        ConditionCollection.instance.GetMonster(newMonsterInfo);
    }
    private void SetMonsterToSkillManager(GameObject newMonster)
    {
        SkillManager.instance.TestMonster = newMonster;
    }
    private void SetMonsterPosition(GameObject newMonster)
    {
        newMonster.transform.parent = monsterPivot.transform;
        newMonster.transform.position = monsterPivot.transform.position;
    }
}
