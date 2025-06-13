using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : MonoBehaviour
{
    [SerializeField] private IBattleEntity requester;

    [SerializeField] private IBattleEntity target;

    [SerializeField] private int[] diceNumber = new int[3];

    //private void Awake()
    //{
    //    requester = BattleManager.Instance.Battle.nowTurnEntity;
    //    diceNumber = SkillManager.instance.RollDice(); 
    //}
    //private void Start()
    //{
    //    //UseSkill();
    //}

    //private void UseSkill()
    //{
    //    GameObject[] entitiys = GetTarget();
    //    StartCoroutine(OnUseSkill(entitiys));

    //}
    //private GameObject[] GetTarget()
    //{
    //    GameObject requesterObj = requester.GetEntity();

    //    if (requesterObj.CompareTag("Player")) target = BattleManager.Instance.Battle.monster;
    //    else target = BattleManager.Instance.Battle.player;

    //    GameObject[] entitys = new GameObject[] {requesterObj, target.GetEntity()};

    //    return entitys;

    //}

    //private IEnumerator OnUseSkill(GameObject[] entitys)
    //{
    //    yield return OnMoveTarget(entitys);


    //    yield break;
    //}

    //private IEnumerator OnMoveTarget(GameObject[] entitys)
    //{
        
    //    Vector3 direction = entitys[1].GetDirection(entitys[0]);



    //    yield break;
    //}



}
