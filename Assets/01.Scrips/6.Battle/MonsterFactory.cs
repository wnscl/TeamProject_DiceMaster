using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : MonoBehaviour
{
    [SerializeField] private GameObject[] monsters;

    public void CreateMonster(int index)
    {
        //Instantiate(monsters[0])

        SetMonsterToSkillManager(index);
        SetMonsterPosition(index);
    }
    private void SetMonsterToSkillManager(int index)
    {

    }
    private void SetMonsterPosition(int index)
    {

    }
}
