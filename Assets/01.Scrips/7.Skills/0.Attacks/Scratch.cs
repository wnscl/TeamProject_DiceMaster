using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Scratch : MonoBehaviour
{
    [SerializeField] private SkillManager skillManager;
    [SerializeField] private GameObject[] effects;
    [SerializeField] private Animator[] effectAnims;

    IBattleEntity[] entitys;

    [SerializeField] private int[] diceNumber = new int[3];

    private int attackCount;

    [SerializeField] float moveDuration;

    private void Awake()
    {
        //entitys = skillManager.SelectEntitys();
        entitys = new IBattleEntity[2];

        entitys[0] = skillManager.TestMonster.GetComponent<IBattleEntity>();
        entitys[1] = skillManager.TestPlayer.GetComponent<IBattleEntity>();

        effects[0].SetActive(false);
        effects[1].SetActive(false);

    }
    private void SetNums()
    {
        diceNumber = skillManager.RollDice();
        attackCount = GetAttackCount();
    }
    private int GetAttackCount()
    {
        int result = 2;
        if (diceNumber[2] % 2 == 0) result = 3;
        if (diceNumber[2] == 6) result = 4;

        return result;
    }
    private void HealEntity(int Amount)
    {

    }
    [Button]
    private void UseSkill()
    {
        SetNums();
        StartCoroutine(OnSkill());
    }
    private IEnumerator OnSkill()
    {
        EntityInfo requesterInfo = entitys[0].GetEntityInfo();
        Animator anim = requesterInfo.anim;

        EntityInfo targetInfo = entitys[1].GetEntityInfo();

        anim.SetBool("isAction", true);
        anim.SetTrigger("Move");

        yield return skillManager.MoveToTarget(entitys, moveDuration);
        anim.SetFloat("AttackSpeed", attackCount);

        effects[0].transform.position = requesterInfo.transform.position;
        effects[1].transform.position = targetInfo.transform.position;


        for (int i = 0; i < attackCount; i++)
        {
            OnOffEffect(true);
            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(((float)1 / (float)attackCount));
            OnOffEffect(false);
            entitys[1].GetDamage(diceNumber[0]);
            requesterInfo.currentHp = Mathf.Clamp(requesterInfo.currentHp + diceNumber[1], 1, requesterInfo.maxHp);
        }
        anim.SetBool("isAction", false);
        skillManager.BackToPosition(entitys[0]);
        yield break;
    }
    private void OnOffEffect(bool OnOff)
    {
        for (int j = 0; j < effects.Length; j++)
        {
            effects[j].SetActive(OnOff);
            effectAnims[j].SetFloat("AttackSpeed", attackCount);
        }
    }

}
