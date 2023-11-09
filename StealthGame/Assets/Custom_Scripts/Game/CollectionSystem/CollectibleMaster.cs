using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CollectibleMaster : MonoBehaviour
{
    public static CollectibleMaster Instance;
    [SerializeField]
    GameObject[] optionalLocations, mandatoryLocations;
    public GameObject[] mandatoryCollectibles;
    public GameObject[] optionalCollectibles;
    [SerializeField]
    bool randomizeOptionalCollectibles = true;
    public int mandatoriesSet = 0, mandatoriesClaimed = 0, collectedValue = 0, currentPrefValue;
    [HideInInspector]
    public CollectionUI uiCounter;
    [HideInInspector]
    public bool allCollectiblesFound = false;

    [HideInInspector]
    public ExitPointer exitPointer;

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
        currentPrefValue = PlayerPrefs.GetInt("TotalPlayerMoney");
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetInt("TotalPlayerMoney", 0);
        }
    }

    void SetMandatoryLocations()
    {
        mandatoryLocations = GameObject.FindGameObjectsWithTag("CollectibleSpawnLocation");
    }

    public void SetMandatoryCollectibles()
    {
        //TODO: Randomization of array!!!
        for (int i = 0; i < mandatoryLocations.Length; i++)
        {
            if (i >= mandatoryCollectibles.Length)
            {
                break;
            }
            var obj = Instantiate(mandatoryCollectibles[i], mandatoryLocations[i].transform.position, mandatoryLocations[i].transform.rotation);
            obj.GetComponent<ChestCollectible>().mandatory = true;
            mandatoriesSet++;
        }
    }

    public void SetOptionalCollectibles()
    {
        for (int i = 0; i < optionalLocations.Length; i++)
        {
            if(randomizeOptionalCollectibles)
            {
                Instantiate(optionalCollectibles[Random.Range(0, optionalCollectibles.Length)], optionalLocations[i].transform.position, optionalLocations[i].transform.rotation);
            }
            else
            {
                Instantiate(optionalCollectibles[i], optionalLocations[i].transform.position, optionalLocations[i].transform.rotation);
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
