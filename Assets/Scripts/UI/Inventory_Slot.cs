using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Slot : MonoBehaviour {
    public Text itemCount;
    public Sprite icon;
    Item.ItemType itemType;

    bool showTooltip = false;
    // Use this for initialization

    void Start()
    {
        itemCount = gameObject.transform.GetChild(0).GetComponent<Text>();
        icon = gameObject.transform.GetChild(1).GetComponent<Sprite>();
    }
    public void AddItem(Item item)
    {
        icon = item.itemIcon;
        itemCount.text = item.itemAmount.ToString();
        if(item.itemType == Item.ItemType.Use|| item.itemType == Item.ItemType.Material)
        {
            if (item.itemAmount > 0) { itemCount.text = item.itemAmount.ToString(); }
            else { itemCount.text = ""; }
        }
    }

    public void RemoveItem()
    {
        itemCount.text = "";
        icon = null;
    }

    void Tooltip(Item item)
    {
        if(item.itemName == "")
        {
            showTooltip = false;
        }
        else
        {

        }
    }
}
