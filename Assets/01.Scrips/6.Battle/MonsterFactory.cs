using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : MonoBehaviour
{
    [SerializeField] private GameObject[] monsters;

    [SerializeField] private GameObject monsterPivot;

    public void CreateMonster(int index)
    {
        GameObject newMonster = Instantiate(monsters[0]);
        SetMonsterToSkillManager(newMonster);
        SetMonsterPosition(newMonster);
    }
    private void SetMonsterToSkillManager(GameObject newMonster)
    {
        SkillManager.instance.TestMonster = newMonster;
    }
    private void SetMonsterPosition(GameObject newMonster)
    {
        newMonster.transform.parent = monsterPivot.transform;
    }
}
