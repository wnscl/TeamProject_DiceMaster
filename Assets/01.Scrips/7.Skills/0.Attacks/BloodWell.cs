using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
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

        if ((diceNumber[0] + diceNumber[1] + diceNumber[2]) >= 12) attackCount = 4f;
        else attackCount = 3f;

    }
    public override IEnumerator OnUse()
    {
        entitys = skillManager.SelectEntitys();
        SetDice();
        SetDirection();


        EntityInfo requesterInfo = entitys[0].GetEntityInfo();
        EntityInfo targetInfo = entitys[1].GetEntityInfo();

        Animator anim = requesterInfo.anim;

        effect[0].transform.position = new Vector3(targetInfo.gameObject.transform.position.x, targetInfo.gameObject.transform.position.y - 0.5f, 0);
        effect[0].SetActive(true);
        requesterInfo.anim.SetBool("isAction", true);
        requesterInfo.anim.SetTrigger("Attack");
        targetInfo.anim.SetBool("isHit", true);
        //targetInfo.anim.SetTrigger("Hit");
        targetInfo.anim.Play("Hit", 0, 0);
        AudioManager.Instance.PlayAudioOnce(MagicSFXEnum.Wind);
        yield return new WaitForSeconds(1f);
        //targetInfo.anim.SetTrigger("Hit");
        targetInfo.anim.Play("Hit", 0, 0);
        requesterInfo.anim.SetBool("isAction", false);
        yield return new WaitForSeconds(1f);
        //targetInfo.anim.SetTrigger("Hit");
        targetInfo.anim.Play("Hit", 0, 0);
        effect[0].transform.position = new Vector3(targetInfo.gameObject.transform.position.x, targetInfo.gameObject.transform.position.y + 0.5f, 0);
        yield return new WaitForSeconds(1f);

        if (attackCount == 4)
        {
            requesterInfo.anim.SetBool("isAction", true);
            requesterInfo.anim.SetTrigger("Attack");
            //targetInfo.anim.SetTrigger("Hit");
            targetInfo.anim.Play("Hit", 0, 0);
            yield return new WaitForSeconds(1f);
            requesterInfo.anim.SetBool("isAction", false);
        }
        targetInfo.anim.SetBool("isHit", false);

        TurnOffSkill();
        entitys[1].GetDamage((int)attackCount * 10);
        yield return new WaitForSeconds(0.5f);

        SpriteRenderer sprite = effect[0].GetComponent<SpriteRenderer>();
        sprite.color = Color.white;
        yield break;
    }
}
