using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using NaughtyAttributes;
using UnityEngine;

public class OneCut : BaseSkill
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
        int damage = ((diceNumber[0] * 2) + (diceNumber[1] * 2) + (diceNumber[2] * 2));
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
            requesterInfo.transform.position,
            targetInfo.transform.position,
        };

        float t = 0;

        effect[0].SetActive(true);
        effect[0].transform.position = startPos[0];
        requesterInfo.anim.SetBool("isAction", true);
        requesterInfo.anim.SetTrigger("Attack");
        AudioManager.Instance.PlayAudioOnce(PyhsicsSFXEnum.Slash);
        yield return new WaitForSeconds(1f);
        requesterInfo.gameObject.transform.position = new Vector3(30, 20, 0);
        effect[0].transform.position = Vector3.Lerp(
            startPos[0], startPos[1], 0.5f);
        yield return new WaitForSeconds(0.5f);
        
        for (int i = 0; i < 5; i++)
        {
            effectAnim[0].SetTrigger("Slice");
            effect[0].transform.position = Vector2.Lerp(
            startPos[0], startPos[1], t);
            t += 0.2f;
            yield return new WaitForSeconds(0.2f);
        }

        effect[0].transform.position = targetInfo.transform.position;
        requesterInfo.transform.position = Vector2.Lerp(
            startPos[0], startPos[1], 0.8f);
        requesterInfo.anim.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);

        anim.SetBool("isAction", false);
        MakeDamage(requesterInfo, entitys[1]);

        requesterInfo.gameObject.transform.position = startPos[0];
        TurnOffSkill();
        yield break;
    }
}
