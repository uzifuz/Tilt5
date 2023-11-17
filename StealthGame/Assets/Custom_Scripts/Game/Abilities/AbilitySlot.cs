using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{

    private Sprite _abilityIcon;
    
    public Sprite AbilityIcon
    {
        get { return _abilityIcon; }
        set
        {
            _abilityIcon = value;
            if(_abilityIcon == null)
            {
                image.enabled = false;
            }
            else
            {
                image.sprite = _abilityIcon;
                image.enabled = true;
            }
        }
    }

    private Image image;

    private void OnValidate()
    {
        if(image == null)
        {
            image = GetComponent<Image>();
        }
    }
}
