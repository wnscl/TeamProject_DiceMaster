using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public int currentStage = 0; // 0: Level1Scene, 1: Level2Scene, ...
    private string[] stageSceneNames = { "Level1Scene", "Level2Scene", "Level3Scene" };

    public bool isLoadingNextStage = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    // 스테이지 클리어 시 호출: 로딩 씬으로 이동
    public void LoadNextStage()
    {
        if (currentStage < stageSceneNames.Length)
        {
            isLoadingNextStage = true;            
            SceneManager.LoadScene("LoadingScene");
        }
        else
        {
            Debug.Log("모든 스테이지 완료");
        }
    }

    // 로딩 씬에서 트리거 도달 시 호출
    public void LoadStageAfterLoading()
    {
        if (isLoadingNextStage && currentStage < stageSceneNames.Length)
        {
            SceneManager.LoadScene(stageSceneNames[currentStage]);
            AudioManager.Instance.PlayBackGroundAudioOnStart(currentStage);
            currentStage++;
            isLoadingNextStage = false;
        }
    }
}
