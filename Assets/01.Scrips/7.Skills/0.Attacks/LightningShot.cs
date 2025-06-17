using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class LightningShot : BaseSkill
{
    [Button]
    private void UseSkill()
    {
        diceNumber = skillManager.RollDice();
        SetDirection();
        StartCoroutine(OnUse());
    }
    private void MakeDamage(EntityInfo requester, IBattleEntity target)
    {
        int damage = 3 + diceNumber[0];
        if (diceNumber[2] > 4) diceNumber[1] = diceNumber[1] * 2;
        requester.currentHp = Mathf.Clamp(requester.currentHp + diceNumber[1], 0, requester.maxHp);
        target.GetDamage(damage);
    }
    public override IEnumerator OnUse()
    {
        diceNumber = skillManager.RollDice();
        SetDirection();

        EntityInfo requesterInfo = entitys[0].GetEntityInfo();
        EntityInfo targetInfo = entitys[1].GetEntityInfo();
        Animator anim = requesterInfo.anim;

        Vector2[] startPos = new Vector2[2]
        {
            requesterInfo.gameObject.transform.position,
            targetInfo.gameObject.transform.position,
        };

        effect[0].SetActive(true);
        effect[0].transform.position = startPos[1];
        anim.SetBool("isAction", true);
        anim.SetTrigger("Buff");
        AudioManager.Instance.PlayAudioOnce(MagicSFXEnum.Thunder);
        yield return new WaitForSeconds(1f);
        anim.SetBool("isAction", false);
        effect[0].transform.position = Vector2.Lerp(startPos[0], startPos[1], 0.5f);
        yield return new WaitForSeconds(1f);
        anim.SetBool("isAction", true);
        anim.SetTrigger("Attack");
        effect[0].transform.position = startPos[1];
        yield return new WaitForSeconds(1f);
        anim.SetBool("isAction", false);
        TurnOffSkill();
        yield break;
    }
}
