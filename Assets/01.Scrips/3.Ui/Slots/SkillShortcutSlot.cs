using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillShortcutSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler,
    IPointerClickHandler
{
    public SkillDataSo skillData;
    public Image icon;
    public Image skillIcon;
    public Image selectMask;
    private bool isSkillOnSlot = false;

    void SetShortCut()
    {
        
        
        skillIcon.sprite = skillData.Icon;
        isSkillOnSlot = true;
    }

    void ResetShortCut()
    {
        skillIcon.sprite = null;
        skillData = null;
        isSkillOnSlot = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        SkillSlot dragged = eventData.pointerDrag.GetComponent<SkillSlot>();
        if (dragged != null)
        {
            Debug.Log("드롭 성공");
            dragged.transform.SetParent(transform);
            dragged.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            skillData = dragged.skillData;
            SetShortCut();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selectMask.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selectMask.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ResetShortCut();
        }
    }
}