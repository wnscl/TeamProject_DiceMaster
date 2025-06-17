using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Smash : BaseSkill, IUseableSkill
{
    [SerializeField] private float moveDuration;


    [Button]
    private void UseSkill()
    {
        diceNumber = skillManager.RollDice();
        SetDirection();
        StartCoroutine(OnUse());
    }
    private int MakeDamage(EntityInfo info)
    {
        int damage = 0;
        int bounes = 1;

        if (diceNumber[1] > 3) bounes = 2;

        if ((diceNumber[2] * 6) > info.def) bounes = 3;

        return damage = (diceNumber[0] * bounes);
    }
    public override IEnumerator OnUse()
    {
        diceNumber = skillManager.RollDice();
        SetDirection();


        EntityInfo info = entitys[0].GetEntityInfo();
        EntityInfo targetinfo = entitys[1].GetEntityInfo(); 
        Animator anim = info.anim;
        anim.SetBool("isAction",true);
        anim.SetTrigger("Move");

        yield return skillManager.MoveToTarget(entitys, moveDuration);

        anim.SetTrigger("Attack");
        effect[0].transform.position = entitys[1].GetEntityInfo().gameObject.transform.position;

        yield return new WaitForSeconds(0.6f);
        effect[0].SetActive(true);
        targetinfo.anim.SetBool("isHit", true);
        targetinfo.anim.SetTrigger("Hit");
        entitys[1].GetDamage(MakeDamage(entitys[1].GetEntityInfo()));
        yield return new WaitForSeconds(0.4f);
        targetinfo.anim.SetBool("isHit", false);

        anim.SetBool("isAction", false);
        skillManager.BackToPosition(entitys[0]);
        effect[0].SetActive(false);

        yield break;
    }


}
