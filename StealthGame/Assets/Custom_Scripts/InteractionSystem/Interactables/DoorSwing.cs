using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwing : InteractableObject
{
    float targetYRotation;

    public float smooth;
    public bool autoClose;
    [SerializeField]
    Transform player;

    float defaultYRotation = 0f;
    float timer = 0f;

    public Transform pivot;

    bool isOpen;

    protected override void InheritStart()
    {
        defaultYRotation = transform.eulerAngles.y;
    }

    protected override void InheritUpdate()
    {
        pivot.rotation = Quaternion.Lerp(pivot.rotation, Quaternion.Euler(0f, defaultYRotation + targetYRotation, 0f), smooth * Time.deltaTime);

        timer -= Time.deltaTime;

        if (timer <= 0f && isOpen && autoClose)
        {
            ToggleDoor(player.position);
        }
    }

    public void ToggleDoor(Vector3 pos)
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            Vector3 dir = (pos - transform.position);
            targetYRotation = -Mathf.Sign(Vector3.Dot(transform.forward, dir)) * 90f;//default was -transform.right
            timer = 5f;
        }
        else
        {
            targetYRotation = 0f;
        }
    }

    public void Open(Vector3 pos)
    {
        if (!isOpen)
        {
            ToggleDoor(pos);
        }
    }
    public void Close(Vector3 pos)
    {
        if (isOpen)
        {
            ToggleDoor(pos);
        }
    }

    public override void Interact()
    {
        base.Interact();
        ToggleDoor(player.position);
    }

    public string GetDescription()
    {
        if (isOpen) return "Close the door";
        return "Open the door";
    }
}
