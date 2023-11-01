
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

    [SerializeField]
    private Inventory _inventory;

    [SerializeField]
    public GameObject multiplierText;

    private Gadget _selectedGadget;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Debug.LogWarning("more than one thief detected!");
            gameObject.SetActive(false);
        }
        IsHidden = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameHandler.Instance.GameIsOver)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                _inventory.SelectNextGadget();
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                _inventory.UseSelectedGadget();
            }
        }
        
    }

}
