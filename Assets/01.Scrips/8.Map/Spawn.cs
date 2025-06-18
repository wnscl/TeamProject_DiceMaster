using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;

    void Start()
    {
        Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
    }
}
