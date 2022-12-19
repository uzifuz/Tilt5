using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectibles : MonoBehaviour
{
    public GameObject [] collectibleToSpawn;
    private GameObject[] collectibleSpawnLocation;
    private int spawnLocationCount = 0; // wieviele SpawnLocations es für die Collectibles gibt
    public int mandatoryAmount = 0; // wieviel Mandatory Collectibles man spawnen möchte 
    public int moneyAmount = 0; // optional 
    private int wholeAmount = 0;

    private int randomPosition;
    private int[] tmpList;
    private int tmpValue;
    private int mandatoryCollectible;

    void Awake()
    {
        wholeAmount = mandatoryAmount + moneyAmount;
        collectibleSpawnLocation = GameObject.FindGameObjectsWithTag("CollectibleSpawnLocation");
        spawnLocationCount = (int)collectibleSpawnLocation.Length;
        int[] tmpList = new int[spawnLocationCount];

        //Wichtig, nicht mehr Collectables spawnen, als es überhaupt locations gibt!!!
        if(mandatoryAmount > collectibleSpawnLocation.Length)
        {
            mandatoryAmount = collectibleSpawnLocation.Length;
        }

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

        for(int z = 0; z < collectibleToSpawn.Length; z++) // get mandatory collectible
        {
             if(collectibleToSpawn[z].name == "Gem") // get mandatory index
             {
                mandatoryCollectible = z;
             }
        }


        if (spawnLocationCount > 0)
        {
            for(int i = 0; i < spawnLocationCount && wholeAmount > 0; i++) // Collectibles spawnen
            {
                randomPosition = tmpList[i];
                if (mandatoryAmount > 0) // zuerst Mandatory Collectibles spawnen
                {
                    
                    Instantiate(collectibleToSpawn[mandatoryCollectible], collectibleSpawnLocation[randomPosition].transform.position, collectibleSpawnLocation[randomPosition].transform.rotation);
                    mandatoryAmount--;
                    wholeAmount--;
                }
                else // dann erst den Rest (optional)
                {
                    int randomIndexNumber = Random.Range(0, collectibleToSpawn.Length);
                    while(randomIndexNumber == mandatoryAmount)
                    {
                        randomIndexNumber = Random.Range(0, collectibleToSpawn.Length);
                    }
                    Instantiate(collectibleToSpawn[randomIndexNumber], collectibleSpawnLocation[randomPosition].transform.position, collectibleSpawnLocation[randomPosition].transform.rotation);
                    moneyAmount--;
                    wholeAmount--;
                }

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
