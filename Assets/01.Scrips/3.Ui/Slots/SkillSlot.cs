using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SkillSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public List<GameObject> skillPrefabs;//아직 미사용
    public SkillDataSo skillData;
    public Image skillIcon;
    public Image settingMark; //주사위 그림 받아서 들어갈곳
    public Image selectMask;
    public GameObject lockMark;
   
    public bool isLocked =true;

    public void OnClickInfo()
    {
        if (isLocked)
        {
           UIManager.Instance.SystemMessage("아직 해금되지 않은 스킬입니다.");
            return;
        }
        if (UIManager.Instance.skillInfo.gameObject.activeInHierarchy &&
            UIManager.Instance.skillInfo.skillSlot.skillData.name != skillData.name)
        {
            Debug.Log("다른 슬롯 클릭 정보 갱신");
            UIManager.Instance.skillInfo.skillSlot = this;
            UIManager.Instance.skillInfo.InitSetInfo();
            return;
            
        }
        
        UIManager.Instance.skillInfo.skillSlot = this;
       
        UIManager.Instance.skillInfo.InfoWIndowOnAndOff();
   
        
          
       
    }

    public void setSetSkillSlot()
    {
        
        skillIcon.sprite = skillData.Icon;

    }

    public void UnlockSkill(SkillDataSo newSkill)
    {
    
            
        if (skillData == newSkill)
        {
            if (isLocked)
            {
                UIManager.Instance.SystemMessage(skillData.Name+UIManager.ColorText("스킬",ColorName.blue)+"이 해금 되었습니다.",1.5f); 
                isLocked = false;
                lockMark.SetActive(false);
            }
            else
            {
                UIManager.Instance.SystemMessage("이미 해금 된"+UIManager.ColorText("스킬",ColorName.blue)+"입니다.",1.5f);
                return;
            }

        }
        
       
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickInfo();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      selectMask.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      selectMask.gameObject.SetActive(false);
    }
}
