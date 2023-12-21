using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    string entityName;
    public float MaxHealth = 100, CurHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        InheritStart();
    }

    // Update is called once per frame
    void Update()
    {
        InheritUpdate();
    }

    protected virtual void InheritStart()
    {

    }

    protected virtual void InheritUpdate()
    {

    }

    /// <summary>
    /// Modify CurHealth value of target by the amount.
    /// </summary>
    /// <returns>True if target is still alive, false if dead!</returns>
    public virtual bool ModifyHealth(float amount)
    {
        CurHealth += amount * (this is Thief ? (100f + PlayerPrefs.GetFloat("DamageMod")) / 100f : 1f);
        //Only do this when script is attached to Thief
        //TODO: Menu should not be switched on more than once
        if(CurHealth <= 0f)
        {
            if(this is Thief)
            {
                GameHandler.Instance.GameOver(GameHandler.GameOutcome.ThiefLose);
            }
            return false;
        }
        return true;
    }

    public virtual void Death(Vector3 deathDirection)
    {

    }
}
