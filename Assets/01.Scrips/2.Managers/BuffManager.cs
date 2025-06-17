
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BuffType
{
    Bleeding,
    Healing
}

public class BuffManager : MonoBehaviour
{
    public static BuffManager instance;

    private void Awake()
    {
        instance = this;
        
        buffDic = new Dictionary<BuffType, IBuff> ();
        buffDic.Add(BuffType.Bleeding, new Bleeding());
        buffDic.Add(BuffType.Healing, new Healing());
    }

    private Dictionary<BuffType, IBuff> buffDic;

    public void AddBuffToList(BuffType type,IBattleEntity entity)
    {
        EntityInfo info = entity.GetEntityInfo();
        if (buffDic.TryGetValue(type, out IBuff value))
        {
            info.buffList.Add(value.Clone());
        }
    }
    public IEnumerator UseBuff(IBattleEntity entity)
    {
        EntityInfo info = entity.GetEntityInfo();

        if (info.buffList == null)
        {
            yield return new WaitForSeconds(0.5f);
            yield break;
        }

        
        for (int i = info.buffList.Count - 1; i >= 0; i--)
        {
            bool isUsed = info.buffList[i].Execute(entity);
            if (!isUsed) info.buffList.RemoveAt(i);
            else yield return new WaitForSeconds(1f);

        }
    }
}
