using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCount : MonoBehaviour
{
    TMPro.TMP_Text text;
    public int count;
    public bool winCondition = false;

    void Start()
    {
        text = GetComponent<TMPro.TMP_Text>();
    }

    public void UpdateCount(string updateTextTo)
    {
        text.text = $"{updateTextTo}";
        if(CollectibleMaster.Instance.mandatoriesClaimed >= CollectibleMaster.Instance.mandatoryCollectibles.Length)
        {
            text.text = "You collected all Gems :)";
            winCondition = true;
        }
    }

    public void WinConditionMet()
    {
        winCondition = true;
    }
}
