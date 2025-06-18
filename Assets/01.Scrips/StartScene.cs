using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] Button StartButton;
    [SerializeField] Button LoadButton;
    //[SerializeField] Image LoadSlot;
    [SerializeField] CinemachineVirtualCamera startCam;
    [SerializeField] Vector2 startPosition;
    [SerializeField] Vector2 stagePosition;
    [SerializeField] SaveLoad saveLoad;
    void Start()
    {
        startCam.Priority = 20;
        player.transform.position = startPosition;
    }

    

    public void OnStart()
    {
        startCam.Priority = 0;
        player.transform.position = stagePosition;
        this.gameObject.SetActive(false);
        AudioManager.Instance.PlayBackGroundAudioOnStart(0);
    }

    public void OnLoad()
    {
        //LoadSlot.gameObject.SetActive(true);
        saveLoad.Load();
        OnStart();
    }
}
