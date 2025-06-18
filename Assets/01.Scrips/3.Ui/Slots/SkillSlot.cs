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

    
    //UI 인터페이스용 필드 
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();

       
    }
    
    public void OnClickInfo()
    {
        if (isLocked)
        {
           UIManager.Instance.SystemMessage("아직 해금되지 않은 스킬입니다.");
            AudioManager.Instance.PlayAudioOnce(UISFXEnum.Fail);
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
                AudioManager.Instance.PlayAudioOnce(UISFXEnum.BuySell);
            }
            else
            {
                UIManager.Instance.SystemMessage("이미 해금 된"+UIManager.ColorText("스킬",ColorName.blue)+"입니다.",1.5f);
                AudioManager.Instance.PlayAudioOnce(UISFXEnum.Fail);
                return;
            }

        }
        
       
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isLocked) return;
        AudioManager.Instance.PlayAudioOnce(UISFXEnum.UseItem);
        originalParent = transform.parent;
        transform.SetParent(canvas.transform); // 가장 상위로 올려서 다른 UI 위로 올라오게
        canvasGroup.blocksRaycasts = false;    // 레이캐스트 막기 (슬롯 위로 드래그 통과하게)
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isLocked) return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isLocked) return;
        AudioManager.Instance.PlayAudioOnce(UISFXEnum.Equip);
        transform.SetParent(originalParent); // 원래 자리로
        rectTransform.anchoredPosition = Vector2.zero;
        canvasGroup.blocksRaycasts = true;
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
