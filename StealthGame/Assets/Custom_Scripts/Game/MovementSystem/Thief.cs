
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : ControllableEntity
{
    public static Thief Instance { get; private set; }
    public bool IsHidden
    {
        get;
        set;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Debug.LogWarning("more than two tieves detected!");
            gameObject.SetActive(false);
        }
        IsHidden = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
