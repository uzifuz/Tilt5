using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{
    public List<Ability> Abilities = new List<Ability>();

    private Ability _ability;
    [SerializeField]
    public Ability Ability
    {
        get { return _ability; }
        set
        {
            _ability = value;
            if(_ability == null)
            {
                image.enabled = false;
            }
            else
            {
                image.sprite = _ability.Icon;
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
