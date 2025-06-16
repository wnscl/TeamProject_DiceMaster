using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FieldEnemyNpc : MonoBehaviour
{

    private bool isLookPlayer = false;
    [SerializeField] private GameObject target;
    private Coroutine myCor;
    public float moveDuration;

    private void NowBattle()
    {
        if(myCor != null) StopCoroutine(myCor);
        myCor = StartCoroutine(RunToPlayer());
    }
    private IEnumerator RunToPlayer()
    {
        float timer = 0;
        float t = 0;

        Vector2 startPos = transform.position;

        while (timer < moveDuration)
        {
            float distance = target.transform.position.GetDistance(this.transform.position, Axis.Z);
            if (distance <= 1.5f)
            {
                myCor = null;
                yield break;
            }

            t = timer / moveDuration;
            transform.position = Vector2.Lerp(startPos, target.transform.position, t);
            timer += Time.deltaTime;
            yield return null;  
        }
        myCor = null;
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.GetComponent<Player>() && !isLookPlayer)
        {
            isLookPlayer = true;
            target = collision.gameObject;

            if (this.transform.position.x > target.transform.position.x) 
            {
                SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
                sprite.flipX = true;
            } 

            NowBattle();
        }
    }

}
