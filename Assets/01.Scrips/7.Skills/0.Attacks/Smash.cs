using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Smash : MonoBehaviour
{
    [SerializeField] private SkillManager skillManager;

    [SerializeField] private GameObject Effect;

    IBattleEntity[] entitys;

    [SerializeField] private int[] diceNumber = new int[3];

    [SerializeField] float moveDuration;

    private void Awake()
    {
        //entitys = skillManager.SelectEntitys();
        entitys = new IBattleEntity[2];

        entitys[1] = skillManager.TestMonster.GetComponent<IBattleEntity>();    
        entitys[0] = skillManager.TestPlayer.GetComponent<IBattleEntity>();

        Effect.SetActive(false);
    }

    [Button]
    private void UseSkill()
    {
        diceNumber = skillManager.RollDice();
        StartCoroutine(OnSkill());
    }
    private void SetDirection()
    {
        EntityInfo info = entitys[0].GetEntityInfo();
        if (info.name != "Player") return;

        Effect.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    private int MakeDamage(EntityInfo info)
    {
        int damage = 0;
        int bounes = 1;

        if (diceNumber[1] > 3) bounes = 2;

        if ((diceNumber[2] * 6) > info.dodge) bounes = 3;

        return damage = (diceNumber[0] * bounes);
    }
    private IEnumerator OnSkill()
    {
        EntityInfo info = entitys[0].GetEntityInfo();
        Animator anim = info.anim;
        anim.SetBool("isAction",true);
        anim.SetTrigger("Move");

        yield return skillManager.MoveToTarget(entitys, moveDuration);

        anim.SetTrigger("Attack");
        Effect.transform.position = entitys[1].GetEntityInfo().gameObject.transform.position;

        yield return new WaitForSeconds(0.6f);
        Effect.SetActive(true);
        SetDirection();
        entitys[1].GetDamage(MakeDamage(entitys[1].GetEntityInfo()));
        yield return new WaitForSeconds(0.4f);
        
        anim.SetBool("isAction", false);
        skillManager.BackToPosition(entitys[0]);
        Effect.transform.rotation = Quaternion.identity;
        Effect.SetActive(false);

        yield break;
    }
    private void InitSkill()
    {

    }


}
