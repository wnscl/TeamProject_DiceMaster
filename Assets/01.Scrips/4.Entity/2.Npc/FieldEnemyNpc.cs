using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class FieldEnemyNpc : MonoBehaviour
{

    private bool isLookPlayer = false;
    [SerializeField] private GameObject target;
    private Coroutine myCor;
    public float moveDuration;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject interctionIcon;

    [SerializeField] private int monsterIndex;

    [Button]
    private void NowBattle()
    {
        GameManager.Instance.monsterIndex = this.monsterIndex;
        if(myCor != null) StopCoroutine(myCor);
        myCor = StartCoroutine(RunToPlayer());
    }
    private IEnumerator RunToPlayer()
    {
        interctionIcon.SetActive(true);
        yield return new WaitForSeconds(1f);
        interctionIcon.SetActive(false);
        float timer = 0;
        float t = 0;

        Vector2 startPos = transform.position;

        anim.SetBool("isAction", true);
        anim.SetTrigger("Move");

        while (timer < moveDuration)
        {
            float distance = target.transform.position.GetDistance(this.transform.position, Axis.Z);
            if (distance <= 1.5f)
            {
                break;
            }

            t = timer / moveDuration;
            transform.position = Vector2.Lerp(startPos, target.transform.position, t);
            timer += Time.deltaTime;
            yield return null;  
        }

        myCor = null;
        anim.SetBool("isAction", false);
        GameManager.Instance.ExcuteBattleEvent(true);
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Player>() && !isLookPlayer)
        {
            isLookPlayer = true;
            target = collision.gameObject;

            if (this.transform.position.x > target.transform.position.x) 
            {
                SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
                sprite.flipX = true;
            }
            GameManager.Instance.StopPlayer();
            NowBattle();
        }
    }

}
