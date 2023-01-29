using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleMaster : MonoBehaviour
{
    public static CollectibleMaster Instance;
    [SerializeField]
    Transform[] optionalLocations, mandatoryLocations;
    public GameObject[] mandatoryCollectibles;
    public GameObject[] optionalCollectibles;
    [SerializeField]
    bool randomizeOptionalCollectibles = true;
    public int mandatoriesClaimed = 0;
    [HideInInspector]
    public CollectibleCount uiCounter;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = FindObjectOfType<CollectibleMaster>();
        }
        uiCounter = FindObjectOfType<CollectibleCount>();
        SetMandatoryCollectibles();
        SetOptionalCollectibles();
        CheckCollection();
    }

    public void SetMandatoryCollectibles()
    {
        if(mandatoryLocations.Length == mandatoryCollectibles.Length)
        {
            for (int i = 0; i < mandatoryLocations.Length; i++)
            {
                if (i >= mandatoryCollectibles.Length)
                {
                    break;
                }
                var obj = Instantiate(mandatoryCollectibles[i], mandatoryLocations[i].position, mandatoryLocations[i].rotation);
                obj.GetComponent<GemCollectible>().mandatory = true;
            }
        }
        else if(mandatoryLocations.Length > mandatoryCollectibles.Length)
        {
            List<int> positionSave = new List<int>();
            int counter = 0;
            while(counter < mandatoryCollectibles.Length)
            {
                int randomNr = Random.Range(0, mandatoryLocations.Length);
                if(!positionSave.Contains(randomNr))
                {
                    var obj = Instantiate(mandatoryCollectibles[counter], mandatoryLocations[randomNr].position, mandatoryLocations[randomNr].rotation);
                    obj.GetComponent<GemCollectible>().mandatory = true;
                    positionSave.Add(randomNr);
                    counter++;
                }
            }
        }
    }

    public void SetOptionalCollectibles()
    {
        for (int i = 0; i < optionalLocations.Length; i++)
        {
            if(randomizeOptionalCollectibles)
            {
                Instantiate(optionalCollectibles[Random.Range(0, optionalCollectibles.Length)], optionalLocations[i].position, optionalLocations[i].rotation);
            }
            else
            {
                Instantiate(optionalCollectibles[i], optionalLocations[i].position, optionalLocations[i].rotation);
            }
        }
    }

    public void CheckCollection()
    {
        if (mandatoriesClaimed >= mandatoryCollectibles.Length)
        {
            uiCounter.UpdateCount($"You collected every objective!");
            uiCounter.WinConditionMet() ;
        }
        else
        {
            uiCounter.UpdateCount($"You found {mandatoriesClaimed} of {mandatoryCollectibles.Length}");
        }
    }
}
