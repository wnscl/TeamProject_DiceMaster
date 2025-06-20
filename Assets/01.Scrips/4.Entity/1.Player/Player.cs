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

        GameManager.Instance.player = this;
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
            UIManager.Instance.SystemMessage("ęłľę˛Š? ??ź??ľ??¤.");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable inpc = collision.GetComponent<IInteractable>();

        if (inpc != null)
        {
            interactableSign.SetActive(true);
            isInteractable = true;

            if (interCoroutine == null)
            {
                interCoroutine = interact(inpc);
                StartCoroutine(interCoroutine);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable inpc = collision.GetComponent<IInteractable>();

        if (inpc != null)
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