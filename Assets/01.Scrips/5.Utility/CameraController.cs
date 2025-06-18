using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using NaughtyAttributes;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cams;
    [SerializeField] private int activedCamPriority;
    [SerializeField] private int unactiveCamPriority;
    [SerializeField] private bool isMainScene;

    private void Start()
    {
        GameManager.Instance.battleEvent += ChangeScreen;
    }

    [Button]
    private void ChangeScreen()
    {
        int playerCamIndex = 0;
        int battleCamIndex = 1;

        if (isMainScene) isMainScene = false;
        else
        {
            playerCamIndex = 1;
            battleCamIndex = 0;
            isMainScene = true;
        }

        cams[playerCamIndex].Priority = unactiveCamPriority;
        cams[battleCamIndex].Priority = activedCamPriority;
    }

}
