using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSpecification : MonoBehaviour
{
    public string PartName = "";
    public float SpeedModPercent = 0f, DamageTakenPercent = 0f, CooldownPercent = 0f, NoiseModifierPercent = 0f;
    public CustomCharacterSettings.CharacterClass[] ExclusiveTo;
    
    public bool CheckExclusivity(CustomCharacterSettings.CharacterClass thisClass)
    {
        if(ExclusiveTo.Length == 0)
        {
            return true;
        }
        else
        {
            for (int i = 0; i < ExclusiveTo.Length; i++)
            {
                if (ExclusiveTo[i] == thisClass)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
