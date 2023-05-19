using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectionUI : MonoBehaviour
{
    private TextMeshProUGUI collectionText;

    void Start()
    {
        collectionText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCount(string updateTextTo)
    {
        if(collectionText == null)
        {
            collectionText = GetComponent<TextMeshProUGUI>();
        }

        collectionText.text = updateTextTo;
    }
}
