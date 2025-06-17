using System.Linq;
using UnityEngine;

public class Healing : IBuff
{
    private int buffCount = 1;

    public bool Execute(IBattleEntity entity) //주사위 눈금 합의 2배만큼의 비율 회복
    {
        if (buffCount == 0) return false;
        buffCount--;

        EntityInfo info = entity.GetEntityInfo();

        int[] diceNum = SkillManager.instance.RollDice();
        int healRatio = diceNum.Sum() * 2;
        
        info.currentHp = Mathf.Clamp(info.currentHp + (info.currentHp * healRatio), 0, info.maxHp);

        return true;
    }

    public IBuff Clone()
    {
        return new Healing();
    }
}
