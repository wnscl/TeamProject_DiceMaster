using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BloodWell : BaseSkill
{
    private float attackCount;
    [Button]
    private void UseSkill()
    {
        SetDice();
        SetDirection();
        StartCoroutine(OnUse());
    }
    private void SetDice()
    {
        diceNumber = skillManager.RollDice();

        if ((diceNumber[0] + diceNumber[1] + diceNumber[2]) > 15) attackCount = 4f;
        else attackCount = 3f;

    }
    public override IEnumerator OnUse()
    {
        SetDice();
        SetDirection();


        EntityInfo requesterInfo = entitys[0].GetEntityInfo();
        EntityInfo targetInfo = entitys[1].GetEntityInfo();

        Animator anim = requesterInfo.anim;

        effect[0].transform.position = new Vector3(targetInfo.gameObject.transform.position.x, targetInfo.gameObject.transform.position.y - 0.5f, 0);
        effect[0].SetActive(true);
        requesterInfo.anim.SetBool("isAction", true);
        requesterInfo.anim.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);

        requesterInfo.anim.SetBool("isAction", false);
        yield return new WaitForSeconds(attackCount - 1);
        effect[0].SetActive(false);
        entitys[1].GetDamage((int)attackCount * 10);
        targetInfo.anim.SetBool("isHit", true);
        targetInfo.anim.SetTrigger("Hit");
        yield return new WaitForSeconds(0.5f);

        SpriteRenderer sprite = effect[0].GetComponent<SpriteRenderer>();
        sprite.color = Color.white;
        yield break;
    }
}
