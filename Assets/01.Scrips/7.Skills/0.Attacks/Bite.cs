using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Bite : BaseSkill
{
    [Button]
    private void UseSkill()
    {
        diceNumber = skillManager.RollDice();
        SetDirection();
        StartCoroutine(OnSkill());
    }
    private void MakeDamage(EntityInfo requester, IBattleEntity target)
    {
        int damage = 3 + diceNumber[0];
        if (diceNumber[2] > 4) diceNumber[1] = diceNumber[1] * 2;
        requester.currentHp = Mathf.Clamp(requester.currentHp + diceNumber[1], 0, requester.maxHp);
        target.GetDamage(damage);
    }
    private IEnumerator OnSkill()
    {
        EntityInfo requesterInfo = entitys[0].GetEntityInfo();
        EntityInfo targetInfo = entitys[1].GetEntityInfo();

        Vector3[] pos = new Vector3[2]
        {
            requesterInfo.transform.position,
            targetInfo.transform.position
        };

        Animator anim = requesterInfo.anim;

        effect[0].transform.position = pos[0];
        effect[1].transform.position = pos[1];
        effect[0].SetActive(true);
        requesterInfo.gameObject.transform.position = new Vector3(30, 20, 0);
        yield return new WaitForSeconds(0.5f);
        float x = 4f;
        if (pos[0].x > pos[1].x) x = -4f;
        effect[0].transform.position = new Vector3(pos[0].x + x, pos[0].y, 0);
        yield return new WaitForSeconds(0.5f);
        requesterInfo.anim.SetBool("isAction", true);
        requesterInfo.anim.SetTrigger("Attack");
        requesterInfo.gameObject.transform.position = new Vector3(pos[1].x + (-x / 2), pos[1].y, 0);
        effect[1].SetActive(true);
        targetInfo.anim.SetBool("isHit", true);
        targetInfo.anim.SetTrigger("Hit");
        yield return new WaitForSeconds(1f);
        targetInfo.anim.SetBool("isHit", false);
        requesterInfo.anim.SetTrigger("Buff");
        effect[0].transform.position = requesterInfo.gameObject.transform.position;
        yield return new WaitForSeconds(1f);

        MakeDamage(requesterInfo, entitys[1]);

        requesterInfo.anim.SetBool("isAction", false);
        effect[0].SetActive(false);
        effect[1].SetActive(false);
        requesterInfo.gameObject.transform.position = pos[0];
        SpriteRenderer sprite = effect[0].GetComponent<SpriteRenderer>();
        sprite.color = Color.white;
        yield break;
    }
}
