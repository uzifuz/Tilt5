using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCreationPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI headerText, descriptionText;

    public void UpdateTexts(string newHeader, string newDescription)
    {
        headerText.text = newHeader;
        descriptionText.text = newDescription;
    }
}
