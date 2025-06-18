using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSlice : BaseSkill
{
    private void MakeDamage(IBattleEntity target)
    {
        int damage = ((diceNumber[0] * 12) - ((diceNumber[1] * 2) + (diceNumber[2] * 2)));
        target.GetDamage(damage);
    }
    public override IEnumerator OnUse()
    {
        entitys = skillManager.SelectEntitys();
        diceNumber = skillManager.RollDice();
        SetDirection();

        EntityInfo requesterInfo = entitys[0].GetEntityInfo();
        EntityInfo targetInfo = entitys[1].GetEntityInfo();
        Animator anim = requesterInfo.anim;

        Vector2[] startPos = new Vector2[2]
        {
            requesterInfo.transform.position,
            targetInfo.transform.position,
        };


        effect[0].SetActive(true);
        effect[0].transform.position = Vector2.Lerp(startPos[0], startPos[1], 0.5f);

        requesterInfo.anim.SetBool("isAction", true);
        requesterInfo.anim.SetTrigger("Buff");
        yield return new WaitForSeconds(1.8f);
        effect[1].SetActive(true);
        yield return new WaitForSeconds(1.2f);

        float x = -2.4f;
        if (requesterInfo.name == "BattlePlayer") x = 2.4f;

        requesterInfo.transform.position = new Vector2(startPos[1].x + x, startPos[1].y);
        requesterInfo.anim.SetTrigger("Attack");
        targetInfo.anim.SetBool("isHit", true);
        targetInfo.anim.Play("Hit", 0, 0);
        yield return new WaitForSeconds(1f);

        targetInfo.anim.SetBool("isHit", false);
        anim.SetBool("isAction", false);
        MakeDamage(entitys[1]);

        requesterInfo.gameObject.transform.position = startPos[0];
        TurnOffSkill();
        yield break;
    }
}
