using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class SwordFlow : MonoBehaviour
{
    [SerializeField] private SkillManager skillManager;

    [SerializeField] private GameObject effect;
    [SerializeField] private Animator effectAnim;

    [SerializeField] private GameObject[] swords;

    IBattleEntity[] entitys;

    [SerializeField] private int[] diceNumber = new int[3];
    private int swordCount;

    [SerializeField] float moveDuration;

    private void Awake()
    {
        //entitys = skillManager.SelectEntitys();
        entitys = new IBattleEntity[2];

        entitys[0] = skillManager.TestMonster.GetComponent<IBattleEntity>();
        entitys[1] = skillManager.TestPlayer.GetComponent<IBattleEntity>();

        effect.SetActive(false);

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
        StartCoroutine(OnSkill());
    }
    private IEnumerator OnSkill()
    {
        EntityInfo info = entitys[0].GetEntityInfo();

        info.anim.SetBool("isAction", true);
        info.anim.SetTrigger("Attack");
        effect.SetActive(true);
        effect.transform.position = entitys[1].GetEntityInfo().gameObject.transform.position;
        yield return new WaitForSeconds(0.5f);
        yield return SetPositionOfSword();
        yield return OnMoveSword();
        effect.SetActive(true);
        effectAnim.SetTrigger("Explode");
        yield return new WaitForSeconds(1f);
        effect.SetActive(false);
        
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

            effect.transform.position = swordPos;
            effectAnim.SetTrigger("Spawn");

            angle += (360 / swordCount); 
            yield return new WaitForSeconds(0.3f);
        }
        effect.transform.position = target;
        effect.SetActive(false);
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
