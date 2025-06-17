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
        entitys = skillManager.SelectEntitys();

        EntityInfo[] entityInfo = new EntityInfo[] 
        {entitys[0].GetEntityInfo(), entitys[1].GetEntityInfo()};

        diceNumber = skillManager.RollDice();
        IBattleEntity entity = diceNumber.Sum() >= 9 ? entitys[1] : entitys[0];
        EntityInfo info = entity.GetEntityInfo();

        effect[0].transform.position = Vector2.Lerp(entityInfo[0].transform.position, entityInfo[1].transform.position, 0.5f);

        foreach (EntityInfo animinfo in entityInfo)
        {
            animinfo.anim.SetBool("isAction", true);
            animinfo.anim.SetTrigger("Attack");
        }
        effect[0].SetActive(true);
        yield return new WaitForSeconds(1f);
        foreach (EntityInfo animinfo in entityInfo)
        {
            animinfo.anim.SetBool("isAction", false);
        }
        yield return new WaitForSeconds(0.5f);
        effect[0].transform.position = info.transform.position;
        info.anim.SetBool("isHit", true);
        info.anim.SetTrigger("Hit");
        yield return new WaitForSeconds(1f);


        entity.GetDamage(info.currentHp / 2);
        info.anim.SetBool("isHit", false);

        TurnOffSkill();
        yield break;
    }
}
