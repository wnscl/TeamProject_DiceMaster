using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject[] camAncher;
    int camAncherIndex = 1;

    private void Awake()
    {
        ChangeCam();
    }
    private void Start()
    {
        GameManager.Instance.battleEvent += ChangeCam;
    }

    [Button]
    private void ChangeCam()
    {
        if (camAncherIndex == 0) camAncherIndex = 1;
        else camAncherIndex = 0;

        this.transform.parent = camAncher[camAncherIndex].transform;
        this.transform.position = camAncher[camAncherIndex].transform.position;
    }
}
