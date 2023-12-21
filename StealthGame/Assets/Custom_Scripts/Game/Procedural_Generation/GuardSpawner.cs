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
        int spawned = 0;
        for (int i = allSpawnPoints.Length - 1; i >= 0; i--)
        {
            spawned++;
            var obj = Instantiate(PrefabsOrderedByDifficulty[Random.Range(0, Mathf.Clamp(difficulty, 0, PrefabsOrderedByDifficulty.Length))], allSpawnPoints[i].transform.position, allSpawnPoints[i].transform.rotation);
            if(Random.Range(0f, 1f) <= 0.4f)
            {
                obj.GetComponent<Guard>().GuardingPosition = CollectibleMaster.Instance.mandatoryLocations[Random.Range(0, CollectibleMaster.Instance.mandatoryLocations.Length)].transform;

            }

            if (spawned >= count)
                break;
        }
    }
}
