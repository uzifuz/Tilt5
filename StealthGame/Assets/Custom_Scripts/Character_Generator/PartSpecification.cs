using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSpecification : MonoBehaviour
{
    public string PartName = "";
    public float SpeedMod = 0f, HealthMod = 0f, CooldownReduction = 0f, NoiseMod = 0f;
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
