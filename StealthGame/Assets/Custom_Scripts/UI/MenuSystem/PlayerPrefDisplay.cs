using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPrefDisplay : MonoBehaviour
{
    public string displayThisPlayerPref;
    public TextMeshProUGUI textDisp;

    private void Update()
    {
        textDisp.text = "" + PlayerPrefs.GetInt(displayThisPlayerPref).ToString() + " $";
    }

}
