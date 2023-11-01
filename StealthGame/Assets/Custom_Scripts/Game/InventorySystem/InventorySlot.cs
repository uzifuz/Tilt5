using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Gadget _gadget;
    public Gadget Gadget
    {
        get { return _gadget; }
        set
        {
            _gadget = value;
            if(_gadget == null)
            {
                image.enabled = false;
            }
            else
            {
                image.sprite = _gadget.Icon;
                image.enabled = true;
            }
        }
    }
    [SerializeField]
    public Image image;
    

    private void OnValidate()
    {
        if(image == null)
        {
            image = GetComponent<Image>();
        }
    }
}
