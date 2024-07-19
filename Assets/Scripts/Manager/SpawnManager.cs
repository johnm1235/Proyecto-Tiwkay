using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;

    public static SpawnManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Transform GetSpawnPoint(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < spawnPoints.Length)
        {
            return spawnPoints[levelIndex];
        }
        return spawnPoints[0]; // Default to the first spawn point if index is out of range
    }
}

