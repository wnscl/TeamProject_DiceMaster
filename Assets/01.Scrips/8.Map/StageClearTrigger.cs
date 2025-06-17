using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StageManager.Instance.LoadNextStage(); // 로딩 씬으로 이동
        }
    }
}
