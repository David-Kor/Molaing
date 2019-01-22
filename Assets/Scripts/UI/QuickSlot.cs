using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour {
    public Text ItemCount;
    public Image icon;
    public int itemID;
    public int Amount;
    public int ItemType;

    // Use this for initialization
    void Start()
    {
        icon = gameObject.transform.GetChild(0).GetComponent<Image>();
        ItemCount = gameObject.transform.GetChild(2).GetComponent<Text>();
    }
    
    void RemoveSlot()
    {
        
    }
}
