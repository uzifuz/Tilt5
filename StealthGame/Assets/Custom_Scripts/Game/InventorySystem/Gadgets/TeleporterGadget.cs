using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class TeleporterGadget : Gadget
{
    [SerializeField]
    private GameObject cursorMarker;
    public override void UseGadget()
    {
        base.UseGadget();
        //Instantiate(cursorMarker, cursorMarker.transform, false);
        /*
        while (!Input.GetKeyDown(KeyCode.Escape))
        {
            Thief.Instance.CanMove = false;
            if(Input.GetMouseButtonDown(0))
            {
                Vector3 mouse = Input.mousePosition;
                Ray castPoint = Camera.main.ScreenPointToRay(mouse);
                RaycastHit hit;
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                {
                    Thief.Instance.transform.position = hit.point;
                }
                return;
            }
            Thief.Instance.CanMove = true;
        }
        */
    }
}
