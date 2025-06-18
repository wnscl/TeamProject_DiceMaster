using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SwordFlow : BaseSkill, IUseableSkill
{
    [SerializeField] private GameObject[] swords;

    private int swordCount;

    [SerializeField] private float moveDuration;

    protected override void Awake()
    {
        //entitys = skillManager.SelectEntitys();
        entitys = new IBattleEntity[2];

        entitys[0] = skillManager.TestMonster.GetComponent<IBattleEntity>();
        entitys[1] = skillManager.TestPlayer.GetComponent<IBattleEntity>();

        skill = this.GetComponent<IUseableSkill>();
        Debug.Log($"{this.name}ÀÇ GetComponent<IUseableSkill>() °á°ú: {skill}");

        for (int i = 0; i < effect.Length; i++)
        {
            effect[i].SetActive(false);
        }

        for (int i = 0; i < swords.Length; i++)
        {
            swords[i].SetActive(false);
        }
    }
    private void SetNums()
    {
        diceNumber = skillManager.RollDice();
        if (diceNumber[2] > 4) swordCount = 6;
        else swordCount = 3;
        if (diceNumber[1] > 4) diceNumber[1] = 4;
    }
    [Button]
    private void UseSkill()
    {
        SetNums();
        StartCoroutine(OnUse());
    }
    public override IEnumerator OnUse()
    {
        entitys = skillManager.SelectEntitys();

        SetNums();

        EntityInfo info = entitys[0].GetEntityInfo();
        EntityInfo target = entitys[1].GetEntityInfo();

        info.anim.SetBool("isAction", true);
        info.anim.SetTrigger("Attack");
        AudioManager.Instance.PlayAudioOnce(PyhsicsSFXEnum.Slash);
        effect[0].SetActive(true);
        effect[0].transform.position = entitys[1].GetEntityInfo().gameObject.transform.position;
        yield return new WaitForSeconds(0.5f);
        info.anim.SetBool("isAction", false);
        yield return SetPositionOfSword();
        yield return OnMoveSword();
        effect[0].SetActive(true);
        target.anim.SetBool("isHit", true);
        //target.anim.SetTrigger("Hit");
        target.anim.Play("Hit", 0, 0);
        effectAnim[0].SetTrigger("Explode");
        AudioManager.Instance.PlayAudioOnce(MagicSFXEnum.Fire);
        yield return new WaitForSeconds(1f);
        TurnOffSkill();
        target.anim.SetBool("isHit", false);
        
        for (int i = 0; i < swordCount; i++)
        {
            entitys[1].GetDamage(diceNumber[0] + diceNumber[1]);
        }

        yield break;
    }
    private IEnumerator SetPositionOfSword()
    {
        float angle = 0f;
        float distance = 3f;
        float rad = 0;
        Vector2 target = entitys[1].GetEntityInfo().gameObject.transform.position;
        Vector2 swordPos= Vector2.zero;

        for (int i = 0; i < swordCount; i++)
        {
            swords[i].SetActive(true);
            rad = angle * Mathf.Deg2Rad;
            Vector2 offset = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * distance;
            swords[i].transform.position = target + offset;
            swordPos = swords[i].transform.position;
            Vector2 direction = target - swordPos;
            float swordAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            swords[i].transform.rotation = Quaternion.Euler(0, 0, swordAngle);

            effect[0].transform.position = swordPos;
            effectAnim[0].SetTrigger("Spawn");

            angle += (360 / swordCount); 
            yield return new WaitForSeconds(0.3f);
        }
        effect[0].transform.position = target;
        effect[0].SetActive(false);
    }
    private IEnumerator OnMoveSword()
    {
        float timer = 0;
        float t = 0;

        Vector2 target = entitys[1].GetEntityInfo().gameObject.transform.position;
        Vector2[] startPos = new Vector2[swordCount];
        Vector2[] endPos = new Vector2[swordCount];
        for (int i = 0; i < swordCount; i++)
        {
            startPos[i] = swords[i].transform.position;
            Vector2 direction = (target - startPos[i]).normalized;
            endPos[i] = startPos[i] + (direction * 2.5f);
        }

        while (timer < moveDuration)
        {
            t = timer / moveDuration;
            for (int i = 0; i < swordCount; i++)
            {
                swords[i].transform.position = Vector2.Lerp(startPos[i], endPos[i], t);
            }
            timer += Time.deltaTime;
            yield return null;
        }
        foreach (var sword in swords)
        {
            sword.SetActive(false);
        }
        yield break;
    }

}
