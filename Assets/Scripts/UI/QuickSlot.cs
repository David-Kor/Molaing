using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : Inventory_Slot {
    public Text ItemCount;

    // Use this for initialization
    void Start()
    {
        icon = gameObject.transform.GetChild(0).GetComponent<Image>();
        ItemCount = gameObject.transform.GetChild(2).GetComponent<Text>();
    }
    public new void OnMouseDown()
    {

    }

    public new void OnMouseUp()
    {
        CastRay();
        if(targetObject.CompareTag("QuickSlot"))
        {
            if(targetObject.itemID == 0) { MoveSlot(); }
            else { SwapSlot(); }
        }
        else
        {
            RemoveItem();
        }
    }

    public void UseItem()
    {
        if(CheckSlot(itemID) == true)
        {
            Amount--;
            if(Amount <= 0) { RemoveItem(); }
        }
    }
}
