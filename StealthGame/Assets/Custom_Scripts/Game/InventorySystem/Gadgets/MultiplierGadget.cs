using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MultiplierGadget : Gadget
{
    private float _multiplier = 2.0f;
    private float _duration = 10.0f;
    public override void UseGadget()
    {
        base.UseGadget();
        CollectibleMaster.Instance.SetMultiplierForSeconds(_multiplier, _duration);
    }

}
