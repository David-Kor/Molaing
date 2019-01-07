using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public GameObject[] slots;
    public List<Item> item;
    public itemDateBase db;
    public GUISkin skin;
    private Item draggedItem;

    private bool show_tool_tip;
    private bool dragItem;
    private int prevIndex;
    private string tooltip;
    
    void Start()
    {

        slots = new GameObject[49];
        item = new List<Item>();
        for(int i = 0; i < 49; i++)
        {
            slots[i] = gameObject.transform.GetChild(0).GetChild(i).gameObject;
            item.Add(new Item());
        }
        dragItem = false;
        show_tool_tip = false;
        db = GameObject.FindGameObjectWithTag("Item DataBase").GetComponent<itemDateBase>();
    }

    void OnGUI()
    {
        GUI.skin = skin;
        if (gameObject.activeSelf == true)
        {
            DrawItemslot();
        }
        if (show_tool_tip == true)
        {
            GUI.Box(new Rect(Event.current.mousePosition.x + 5, Event.current.mousePosition.y + 2, 200, 200), tooltip, skin.GetStyle("tooltip"));
        }
        if (dragItem)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x - 5, Event.current.mousePosition.y - 5, 50, 50), draggedItem.itemIcon);
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
    
    void AddItem(int i)
    {

    }
    
}