using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<Gadget> gadgets;
    public List<Gadget> Gadgets
    {
        get { return gadgets; }
    }
    [SerializeField]
    private Transform gadgetsParent;
    [SerializeField]
    private InventorySlot[] inventorySlots;
    private int selectedGadgetIndex = 0;

    private void OnValidate()
    {
        if (gadgetsParent != null)
            inventorySlots = gadgetsParent.GetComponentsInChildren<InventorySlot>();

        RefreshUI();
    }

    private void RefreshUI()
    {
        int i = 0;

        for (; i < gadgets.Count; i++)
        {
            inventorySlots[i].Gadget = gadgets[i];
        }

        for (; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].Gadget = null;
        }
    }

    public void SelectNextGadget()
    {
        if (gadgets != null && gadgets.Count > 0 && !gadgets.All(gadget => gadget == null))
        {
            ++selectedGadgetIndex;

            if (selectedGadgetIndex >= gadgets.Count || selectedGadgetIndex >= 4)
            {
                selectedGadgetIndex = 0;
            }

            if (gadgets[selectedGadgetIndex] == null)
            {
                SelectNextGadget();
            }

            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot != null)
                {
                    slot.image.color = Color.white;
                }
            }

            //change selected gadget icon color to green
            if (inventorySlots != null && inventorySlots[selectedGadgetIndex] != null)
            {
                inventorySlots[selectedGadgetIndex].image.color = Color.green;
            }

            Debug.Log("Selected index: " + selectedGadgetIndex);
        }
        else
        {
            Debug.Log("No Gadgets in inventory");
        }
    }

    public void UseSelectedGadget()
    {
        if (gadgets != null && gadgets.Count > 0)
        {
            if (gadgets[selectedGadgetIndex] != null)
            {
                gadgets[selectedGadgetIndex].UseGadget();
                gadgets[selectedGadgetIndex] = null;
                inventorySlots[selectedGadgetIndex].image.enabled = false;
                inventorySlots[selectedGadgetIndex] = null;
                SelectNextGadget();
            }
            else
            {
                Debug.Log("ERR: No gadget selected");
            }
            
        }
        else
        {
            Debug.Log("No Gadgets in inventory");
        }
    }
}
