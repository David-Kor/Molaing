using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour {

    public int itemID;
    public int count;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        Inventory.instance.GetItem(itemID, count);
        Destroy(this.gameObject);
    }
}
