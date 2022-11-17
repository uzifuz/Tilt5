using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;
using static UnityEngine.Rendering.DebugUI;

public class WinCondition : InteractableObject
{
    [SerializeField]
    GameObject WinMenuPrefab;
    GameObject WinMenu;
    // Start is called before the first frame update
    void Start()
    {
        WinMenu = Object.Instantiate(WinMenuPrefab);
        WinMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        base.Interact();

        //TODO: Win Logic mit Collectibles
        if(CollectibleCount.winCondition)
        {
            WinMenu.SetActive(true);
            GameObject.Find("Thief").GetComponent<ControllableEntity>().CanMove = false;
        }

    }
}
