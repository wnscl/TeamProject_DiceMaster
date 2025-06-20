using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUseableSkill
{
    IEnumerator OnUse();
}

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    [SerializeField] private BaseSkill[] baseSkill;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        skills = new IUseableSkill[baseSkill.Length];
        for (int i = 0; i < baseSkill.Length; i++)
        {
            skills[i] = baseSkill[i].GetSkill();
        }
    }


    [SerializeField] private BattleModel battleModel;

    public IUseableSkill[] skills;
    public SkillDataSo[] skillDatas;

    public GameObject TestPlayer;
    public GameObject TestMonster;

    public Vector3 firstPos;


    public IBattleEntity[] SelectEntitys()
    {
        IBattleEntity[] entitys = new IBattleEntity[2];
        entitys[0] = battleModel.nowTurnEntity;
        
        EntityInfo info = battleModel.nowTurnEntity.GetEntityInfo();

        if (info.name == "BattlePlayer")
            entitys[1] = TestMonster.GetComponent<IBattleEntity>();
        else
            entitys[1] = TestPlayer.GetComponent<IBattleEntity>();

        return entitys;
    }

    public int[] RollDice()
    {
        int[] dice = new int[3];

        for (int i = 0; i < 3; i++)
        {
            dice[i] = UnityEngine.Random.Range(1, 7);    
        }

        return dice;
    }

    public IEnumerator MoveToTarget(IBattleEntity[] entitys, float moveDuration)
    {
        GameObject requester = entitys[0].GetEntityInfo().gameObject;
        GameObject target = entitys[1].GetEntityInfo().gameObject;

        firstPos = requester.transform.position;

        Vector3 direction = target.GetDirection(requester);

        Vector3 startPos = requester.transform.position;
        Vector3 endPos = target.transform.position;

        if (direction.x < 0) endPos.x += 2f;
        else endPos.x -= 2f;

        float timer = 0;
        float t = 0;

        while (timer < moveDuration)
        {
            t = timer / moveDuration;
            requester.transform.position = Vector3.Lerp(startPos, endPos, t);
            timer += Time.deltaTime;
            yield return null;
        }
        requester.transform.position = endPos;

        yield break;
    }
    //public IEnumerator BackToPosition(IBattleEntity entity)
    //{

    //    yield break;
    //}
    public void BackToPosition(IBattleEntity requester)
    {
        GameObject entity = requester.GetEntityInfo().gameObject;
        entity.transform.position = firstPos;

        firstPos = Vector3.zero;
    }


}
