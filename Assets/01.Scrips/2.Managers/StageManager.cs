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

    // �������� Ŭ���� �� ȣ��: �ε� ������ �̵�
    public void LoadNextStage()
    {
        if (currentStage < stageSceneNames.Length)
        {
            isLoadingNextStage = true;            
            SceneManager.LoadScene("LoadingScene");
        }
        else
        {
            Debug.Log("��� �������� �Ϸ�");
        }
    }

    // �ε� ������ Ʈ���� ���� �� ȣ��
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
