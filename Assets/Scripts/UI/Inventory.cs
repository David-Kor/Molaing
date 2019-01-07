using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory: MonoBehaviour
{
    public Inventory_Slot[] slots;                  //아이템 슬롯
    public List<Item> item = new List<Item>();  //소지한 아이템 리스트
    private itemDateBase db;
    public GUISkin skin;
    private Item draggedItem;


    private bool show_tool_tip;
    private bool dragItem;
    private int prevIndex;
    private string tooltip;
    
    void Start()
    {
        db = GameObject.FindGameObjectWithTag("Item DataBase").GetComponent<itemDateBase>();
            slots = GetComponentsInChildren<Inventory_Slot>();
        RemoveSlot();
        GetItem(9001);
        dragItem = false;
        show_tool_tip = false;
        Debug.Log(item[0].itemName);
    }

    public void RemoveSlot()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void DrawItem()
    {
        for(int i = 0; i < item.Count; i++)
        {
            if(item[i].itemName == "") { return; }
            else
            {
                Debug.Log(slots[i].name + " " + slots[i].itemCount);
                slots[i].transform.GetChild(1).gameObject.SetActive(true);
                slots[i].icon = item[i].itemIcon;
                slots[i].itemCount.text = item[i].itemAmount.ToString();
            }
        }
    }
    public void GetItem(int itemID, int count = 1)
    {
        for(int i = 0; i < db.item.Count; i++)
        {
            if(itemID == db.item[i].itemID)
            {
                for(int j = 0; j < item.Count; j++)
                {
                    if(item[j].itemID ==itemID)
                    {
                        if(item[j].itemAmount == item[j].maxAmount) { continue; }
                        item[j].itemAmount += count;
                        return;
                    }
                }
                item.Add(db.item[i]);
                return;
            }
        }
    }
    void OnGUI()
    {
        GUI.skin = skin;
        if (gameObject.activeSelf == true)
        {
            DrawItemslot();
            //RemoveSlot();
        }
        if (show_tool_tip == true)
        {
            GUI.Box(new Rect(Event.current.mousePosition.x + 5, Event.current.mousePosition.y + 2, 200, 200), tooltip, skin.GetStyle("tooltip"));
        }
        if (dragItem)
        {
  //          GUI.DrawTexture(new Rect(Event.current.mousePosition.x - 5, Event.current.mousePosition.y - 5, 50, 50), draggedItem.itemIcon);
        }
    }
    void DrawItemslot()
    //인벤토리 슬롯을 그려주는 함수.
    //변수 잘못 건드리면 미세조정 다시 해야됨
    {
        Event e = Event.current;
        
    }
    string CreateTooltip(Item item)
    {
        tooltip = "Item name: <color=#a10000><b>" + item.itemName + "</b></color>";
        return tooltip;
    }
    
}