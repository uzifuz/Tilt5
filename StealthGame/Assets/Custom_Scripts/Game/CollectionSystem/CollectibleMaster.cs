using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CollectibleMaster : MonoBehaviour
{
    public static CollectibleMaster Instance;
    public GameObject[] optionalLocations, mandatoryLocations;
    public GameObject[] mandatoryCollectibles;
    public GameObject[] optionalCollectibles;
    [SerializeField] bool randomizeOptionalCollectibles = true;
    public int mandatoriesSet = 0, mandatoriesClaimed = 0, collectedValue = 0, currentPrefValue;
    [HideInInspector] public CollectionUI uiCounter;
    [HideInInspector] public bool allCollectiblesFound = false;

    [HideInInspector]
    public ExitPointer exitPointer;
    public GameObject messagePrefab;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = FindObjectOfType<CollectibleMaster>();
        }
        uiCounter = FindObjectOfType<CollectionUI>();
        exitPointer = FindObjectOfType<ExitPointer>();
    }

    public void SetupCollectionSystem()
    {
        SetMandatoryLocations();
        SetMandatoryCollectibles();
        SetOptionalCollectibles();
        CheckCollection();
    }

    private void Update()
    {
        currentPrefValue = PlayerPrefs.GetInt("TotalPlayerMoney");//TODO: Don't check each frame!!!
    }

    void SetMandatoryLocations()
    {
        mandatoryLocations = GameObject.FindGameObjectsWithTag("CollectibleSpawnLocation");
    }

    public void SetMandatoryCollectibles()
    {
        //TODO: Randomization of array!!!
        //TODO: This is really strange, the number of collectibles created is equal to the array length
        //If this remains, the possibly spawned stuff is severly limited, which would not be ideal
        for (int i = 0; i < mandatoryLocations.Length; i++)
        {
            if(Random.Range(0f, 1f) >= 0.4f)
            {
                var obj = Instantiate(mandatoryCollectibles[Random.Range(0, mandatoryCollectibles.Length)], mandatoryLocations[i].transform.position, mandatoryLocations[i].transform.rotation);
                obj.GetComponent<ChestCollectible>().mandatory = true;
                mandatoriesSet++;
            }
        }
    }

    public void SetOptionalCollectibles()
    {
        for (int i = 0; i < optionalLocations.Length; i++)
        {
            if(randomizeOptionalCollectibles)
            {
                //Instantiate(optionalCollectibles[Random.Range(0, optionalCollectibles.Length)], optionalLocations[i].transform.position, optionalLocations[i].transform.rotation);
            }
            else
            {
                //Instantiate(optionalCollectibles[i], optionalLocations[i].transform.position, optionalLocations[i].transform.rotation);
            }
        }
    }

    public void CheckCollection()
    {
        if (mandatoriesClaimed >= mandatoriesSet)
        {
            allCollectiblesFound = true;
            uiCounter.UpdateCount($"Get to the exit point!");
            exitPointer.UpdateRotation();

            FindObjectOfType<ExitPointInteractable>().ShowExitHighlight(true);
        }
        else
        {
            //TODO: Check counter of actual items that were instantiated!!!
            uiCounter.UpdateCount($"{mandatoriesClaimed} of {mandatoriesSet} items");
        }
    }
}
