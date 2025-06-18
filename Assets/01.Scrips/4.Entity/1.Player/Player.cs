using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour, IBattleEntity, IInteractable
{
    public StatHandler statHandler;
    public PlayerInfo playerInfo;
    public GameObject interactableSign;

    private void Awake()
    {
        statHandler = GetComponent<StatHandler>();
    }

    public IEnumerator ActionOnTurn(BattlePhase phase)
    {
        Debug.Log("Player's turn action executed.");
        return null;
    }


    public void GetDamage(int dmg)
    {
        int Rd = UnityEngine.Random.Range(0, 100);

        if (statHandler.GetStat(StatType.Evasion) > Rd)
        {
            UIManager.Instance.SystemMessage("공격을 회피했습니다.");
            return;
        }

        playerInfo.currentHp -= dmg;
        UIManager.Instance.battleWindow.SetHPBar();
        Die();
    }

    public bool isDead { get; private set; }

    void Die()
    {
        if (playerInfo.currentHp <= 0)
        {
            isDead = true;
        }

        ;
    }

    public EntityInfo GetEntityInfo()
    {
        return playerInfo;
    }


    private IEnumerator interCoroutine;

    private IEnumerator interact(IInteractable inpc)
    {
        yield return new WaitUntil(() => interTrigger);
        inpc.OnInteract();
    }

    private bool isInteractable = false;
    private bool interTrigger = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other is IInteractable inpc)
        {
            interactableSign.SetActive(true);
            isInteractable = true;
            interCoroutine = interact(inpc);
            if (interCoroutine == null)
            {
                interCoroutine = interact(inpc);
                StartCoroutine(interCoroutine);
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other is IInteractable)
        {
            interactableSign.SetActive(false);
            isInteractable = false;
            interTrigger = false;
            if (interCoroutine != null)
            {
                StopCoroutine(interCoroutine);
                interCoroutine = null;
            }
        }
    }

    public void OnInterTry(InputAction.CallbackContext context)
    {
        if (!isInteractable) return;

        if (context.phase == InputActionPhase.Performed)
        {
           interTrigger = true;
        }
    }

    

    public void OnInteract()
    {
    }
}