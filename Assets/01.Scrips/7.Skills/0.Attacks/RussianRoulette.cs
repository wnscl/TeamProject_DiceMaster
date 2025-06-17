using NaughtyAttributes;
using System.Collections;
using System.Linq;
using UnityEngine;

public class RussianRoulette : BaseSkill, IUseableSkill
{
    [Button]
    private void UseSkill()
    {        
        StartCoroutine(OnUse());
    }

    public override IEnumerator OnUse()
    {
        diceNumber = skillManager.RollDice();
        IBattleEntity entity = diceNumber.Sum() >= 9 ? entitys[1] : entitys[0];
        EntityInfo info = entity.GetEntityInfo();

        //주사위 굴러가는 이펙트
        yield return new WaitForSeconds(0.4f);
        //폭발 이펙트

        info.anim.SetBool("isHit", true);
        info.anim.SetTrigger("Hit");

        entity.GetDamage(info.currentHp / 2);

        yield return new WaitForSeconds(0.4f);
        info.anim.SetBool("isHit", false);
    }
}
