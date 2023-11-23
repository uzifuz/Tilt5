using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSpawner : MonoBehaviour
{
    public static GuardSpawner Instance;

    public GameObject[] PrefabsOrderedByDifficulty;

    private void Start()
    {
        if(Instance == null)
            Instance = this;

    }

    public void SpawnGuards(int count = 1, int difficulty = 1)
    {
        GameObject[] allSpawnPoints = GameObject.FindGameObjectsWithTag("GuardSpawnLocation");
        for (int i = 0; i < count; i++)
        {
            Instantiate(PrefabsOrderedByDifficulty[Random.Range(0, Mathf.Clamp(difficulty, 0, PrefabsOrderedByDifficulty.Length))], allSpawnPoints[Random.Range(0, allSpawnPoints.Length)].transform.position, transform.rotation);
        }
    }
}
