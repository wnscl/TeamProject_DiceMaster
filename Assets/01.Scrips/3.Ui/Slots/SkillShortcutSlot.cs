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
        SkillShortcutSlot dragged = eventData.pointerDrag.GetComponent<SkillShortcutSlot>();
        if (dragged != null)
        {
            for (int i = 0; i < SkillShortCut.slots.Count; i++)
            {
                SkillShortcutSlot slot = SkillShortCut.slots[i].GetComponent<SkillShortcutSlot>();
                if (slot.skillData == null)
                {
                    continue;
                }

                if (slot.skillData.name == dragged.skillData.name)
                {
                    UIManager.Instance.SystemMessage("이미 할당 된 스킬입니다.");
                    return;
                }
            }

            dragged.transform.SetParent(transform);
            dragged.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            skillData = dragged.skillData;
            SetShortCut();
            Debug.Log("드롭 성공");
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