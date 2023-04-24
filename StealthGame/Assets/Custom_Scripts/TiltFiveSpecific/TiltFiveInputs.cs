using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TiltFiveInputs : MonoBehaviour
{
    public static TiltFiveInputs Instance;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public bool one = false, two = false, a = false, b = false, x = false, y = false, trigger = false;
    public float stickX, stickY;

    private void Update()
    {
        CheckInputs();
    }

    public void CheckInputs()
    {
        if (TiltFive.Wand.TryGetWandDevice(TiltFive.PlayerIndex.One, TiltFive.ControllerIndex.Right, out TiltFive.WandDevice wandDevice))
        {
            if (wandDevice.One.wasPressedThisFrame)
            {
                one = true;
            }
            else
            {
                one = false;
            }

            if (wandDevice.Two.wasPressedThisFrame)
            {
                two = true;
            }
            else
            {
                two = false;
            }

            if (wandDevice.A.wasPressedThisFrame)
            {
                a = true;
            }
            else
            {
                a = false;
            }

            if (wandDevice.B.wasPressedThisFrame)
            {
                b = true;
            }
            else
            {
                b = false;
            }

            if (wandDevice.X.wasPressedThisFrame)
            {
                x = true;
            }
            else
            {
                x = false;
            }

            if (wandDevice.Y.wasPressedThisFrame)
            {
                y = true;
            }
            else
            {
                y = false;
            }

            if(wandDevice.Trigger.IsPressed(0.5f))
            {
                trigger = true;
            }
            else if (!wandDevice.Trigger.IsPressed(0.5f))
            {
                trigger = false;
            }

            stickX = wandDevice.Stick.ReadValue().x;
            stickY = wandDevice.Stick.ReadValue().y;
        }
    }
}
