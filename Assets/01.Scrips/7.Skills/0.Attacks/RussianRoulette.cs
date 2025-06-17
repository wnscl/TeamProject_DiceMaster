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

        //¡÷ªÁ¿ß ±º∑Ø∞°¥¬ ¿Ã∆Â∆Æ
        yield return new WaitForSeconds(0.4f);
        //∆¯πﬂ ¿Ã∆Â∆Æ

        info.anim.SetBool("isHit", true);
        info.anim.SetTrigger("Hit");

        entity.GetDamage(info.currentHp / 2);

        yield return new WaitForSeconds(0.4f);
        info.anim.SetBool("isHit", false);
    }
}
