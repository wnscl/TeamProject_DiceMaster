using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IBuff
{
    bool Execute(IBattleEntity entity);
    IBuff Clone();
}

public class Bleeding : IBuff
{
    private int buffCount;
    private int damage;

    public Bleeding()
    {
        buffCount = 2;
        damage = 4;
    }

    public bool Execute(IBattleEntity entity)
    {
        if(buffCount == 0) return false;
        buffCount--;


        BuffManager bm = BuffManager.instance;
        EntityInfo info = entity.GetEntityInfo();

        float x = 1;
        if (info.gameObject.transform.position.x > SkillManager.instance.transform.position.x) x = -1f;

        bm.buffEffect.SetActive(true);
        bm.buffEffect.transform.position = 
            new Vector2(
                info.gameObject.transform.position.x + x, 
                info.gameObject.transform.position.y
                );

        bm.buffAnim.SetTrigger("Bleeding");
        info.anim.Play("Hit", 0, 0);
        entity.GetDamage(damage);
        AudioManager.Instance.PlayAudioOnce(BuffSFXEnum.Debuff);
        return true;
    }
    public IBuff Clone()
    {
        return new Bleeding();
    }
}
