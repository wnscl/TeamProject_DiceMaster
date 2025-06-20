using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using TMPro;
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
    private void MakeDamage(IBattleEntity target)
    {
        target.GetDamage(diceNumber.Sum());
    }
    //private void SetEffectDirection()
    //{
    //    float angle = 0;
    //    EntityInfo info = entitys[0].GetEntityInfo();
    //    if (info.name == "BattlePlayer") angle = 180f;

    //    for (int i = 0; i < effect.Length; i++)
    //    {
    //        effect[i].transform.rotation = Quaternion.Euler(0, angle, 0);
    //    }
    //}
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
        targetInfo.anim.SetBool("isHit", true);
        //targetInfo.anim.SetTrigger("Hit");
        targetInfo.anim.Play("Hit", 0, 0);
        MakeDamage(entitys[1]);
        effect[0].transform.position = startPos[1];
        yield return new WaitForSeconds(1f);
        targetInfo.anim.SetBool("isHit", false);
        anim.SetBool("isAction", false);
        TurnOffSkill();
        yield break;
    }
}
