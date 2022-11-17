using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectibles : MonoBehaviour
{
    public GameObject collectibleToSpawn;
    public GameObject[] collectibleSpawnLocation;
    public int spawnLocationCount = 0; // wieviele SpawnLocations es für die Collectibles gibt
    public int collectibleAmount = 0; // wieviel Collectibles man spawnen möchte 

    private int randomPosition;
    private int[] tmpList;
    private int tmpValue;

    void Awake()
    {
        collectibleSpawnLocation = GameObject.FindGameObjectsWithTag("CollectibleSpawnLocation");
        spawnLocationCount = (int)collectibleSpawnLocation.Length;
        int[] tmpList = new int[spawnLocationCount];

        for(int x = 0; x < spawnLocationCount; x++) // Array befüllen mit allen möglichen SpawnLocations
        {
            tmpList[x] = x;
            //Debug.Log(tmpList[x]);
        }

        for(int y = 0; y < tmpList.Length; y++) // shuffle array 
        {
            int random = Random.Range(y, tmpList.Length);
            tmpValue = tmpList[random];
            tmpList[random] = tmpList[y];
            tmpList[y] = tmpValue;
            //Debug.Log(tmpList[y]);
        }


        if (spawnLocationCount > 0)
        {
            for(int i = 0; i < spawnLocationCount && collectibleAmount > 0; i++) // Collectibles spawnen
            {
                randomPosition = tmpList[i];

                Instantiate(collectibleToSpawn, collectibleSpawnLocation[randomPosition].transform.position, collectibleSpawnLocation[randomPosition].transform.rotation);
                collectibleAmount--;
            }

            for (int j = 0; j < spawnLocationCount; j++) // SpawnLocations deaktivieren
            {
               collectibleSpawnLocation[j].SetActive(false);
            }

        }
        else
        {
            Debug.Log("No SpawnLocation found");
        }
          
    }
}
