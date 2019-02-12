using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    public Inventory_Slot[] eSlots;
    private bool bRemove;

    void Start()
    {
        eSlots = new Inventory_Slot[5];

        for (int i = 0; i < 5; i++)
        {
            eSlots[i] = gameObject.transform.GetChild(0).GetChild(i).GetComponentInChildren<Inventory_Slot>();
            transform.GetChild(0).GetChild(i).GetChild(1).gameObject.SetActive(false);
        }
    }

    public void RemoveSlot()
    {
        for(int i = 0; i < eSlots.Length; i++)
        {
            eSlots[i].RemoveItem();
        }
    }
}
