using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UIElements;

public class Scratch : BaseSkill, IUseableSkill
{
    private int attackCount;

    [SerializeField] float moveDuration;


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
    [Button]
    private void UseSkill()
    {
        SetNums();
        SetDirection();
        StartCoroutine(OnUse());
    }
    public override IEnumerator OnUse()
    {
        entitys = skillManager.SelectEntitys();
        SetNums();
        SetDirection();


        EntityInfo requesterInfo = entitys[0].GetEntityInfo();
        EntityInfo targetInfo = entitys[1].GetEntityInfo();
        Animator anim = requesterInfo.anim;


        anim.SetBool("isAction", true);
        anim.SetTrigger("Move");

        yield return skillManager.MoveToTarget(entitys, moveDuration);
        anim.SetFloat("AttackSpeed", attackCount);

        effect[0].transform.position = requesterInfo.transform.position;
        effect[1].transform.position = targetInfo.transform.position;

        targetInfo.anim.SetBool("isHit", true);
        for (int i = 0; i < attackCount; i++)
        {
            OnOffEffect(true);
            anim.SetTrigger("Attack");
            targetInfo.anim.SetTrigger("Hit");
            BuffManager.instance.AddBuffToList(BuffType.Bleeding, entitys[1]);
            AudioManager.Instance.PlayAudioOnce(PyhsicsSFXEnum.Slash);
            yield return new WaitForSeconds(((float)1 / (float)attackCount));
            OnOffEffect(false);
            entitys[1].GetDamage(diceNumber[0]);
            //requesterInfo.currentHp = Mathf.Clamp(requesterInfo.currentHp + diceNumber[1], 1, requesterInfo.maxHp);
        }
        anim.SetBool("isAction", false);
        targetInfo.anim.SetBool("isHit", false);
        skillManager.BackToPosition(entitys[0]);
        yield break;
    }
    private void OnOffEffect(bool OnOff)
    {
        for (int j = 0; j < effect.Length; j++)
        {
            effect[j].SetActive(OnOff);
            effectAnim[j].SetFloat("AttackSpeed", attackCount);
        }
    }

}
