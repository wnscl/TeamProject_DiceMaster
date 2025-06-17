using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour, IUseableSkill
{
    [SerializeField] protected SkillManager skillManager;

    protected IUseableSkill skill;

    [SerializeField] protected GameObject[] effect;
    [SerializeField] protected Animator[] effectAnim;

    protected IBattleEntity[] entitys;

    [SerializeField] protected int[] diceNumber = new int[3];
    [SerializeField] protected int TestSelectNum;

    public virtual IEnumerator OnUse()
    {
        yield return null;
    }
    public IUseableSkill GetSkill()
    {
        return skill;
    }
    protected virtual void Awake()
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
    }
    protected void SetDirection()
    {
        float angle = 0;
        EntityInfo info = entitys[0].GetEntityInfo();
        if (info.name == "BattlePlayer") angle = 180f;

        for (int i = 0; i < effect.Length; i++)
        {
            effect[i].transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
    [Button]
    protected void Test_ChangeRequester()
    {
        switch (TestSelectNum)
        {
            case 0:
                entitys[0] = skillManager.TestMonster.GetComponent<IBattleEntity>();
                entitys[1] = skillManager.TestPlayer.GetComponent<IBattleEntity>();
                break;

            case 1:
                entitys[1] = skillManager.TestMonster.GetComponent<IBattleEntity>();
                entitys[0] = skillManager.TestPlayer.GetComponent<IBattleEntity>();
                break;
        }
    }

    protected void TurnOffSkill()
    {
        foreach (GameObject obj in effect)
        {
            obj.SetActive(false);
        }
    }


}
