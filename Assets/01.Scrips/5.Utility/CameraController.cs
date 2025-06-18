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

    private void Start()
    {
        GameManager.Instance.battleEvent += ChangeScreen;
    }

    private void ChangeScreen(bool isBattleStart)
    {
        int playerCamIndex = 0;
        int battleCamIndex = 1;

        if (!isBattleStart)
        {
            playerCamIndex = 1;
            battleCamIndex = 0;
        }

        cams[playerCamIndex].Priority = unactiveCamPriority;
        cams[battleCamIndex].Priority = activedCamPriority;
    }

}
