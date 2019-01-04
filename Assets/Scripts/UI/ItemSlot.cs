using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public List<Item> itemSlot = new List<Item>();
    public List<Item> slots = new List<Item>();

    public int slotX, slotY;    //저장공간의 가로 세로 지정. 총 slotX * slotY의 공간이 생성됨.

    public itemDateBase db;

    public GUISkin skin;

    private bool show_tool_tip;
    private bool dragItem;
    private int prevIndex;
    private string tooltip;
    private Item draggedItem;

    Rect slotRect;
    Rect textRect;
    void Start()
    {
        dragItem = false;
        show_tool_tip = false;
        for (int i = 0; i < slotX * slotY; i++)
        {
            itemSlot.Add(new Item());       //아이템 저장공간 생성.
            slots.Add(new Item());          //생성된 저장공간 비워줌.
        }
        db = GameObject.FindGameObjectWithTag("Item DataBase").GetComponent<itemDateBase>();
        AddItem(1000);
        AddItem(1001);
        AddItem(1000);
        AddItem(1001);
        AddItem(1000);
        AddItem(1001);
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
        int k = 0;
        int count = 0;

        int xMin = 575;
        int yMin = 170;

        int xMax = xMin + 55 * 6;
        int yMax = yMin + 55 * 6;

        int slotSize = 50;
        int textSlotSize = 16;

        for (int j = 0; j < slotY; j++)
        {
            for (int i = 0; i < slotX; i++)
            {
                slotRect = new Rect(i * 55 + xMin, j * 55 + yMin, slotSize, slotSize);
                textRect = new Rect(i * 55 + xMin+34, j * 55 + yMin+34, textSlotSize, textSlotSize);
                GUI.Box(slotRect, "", skin.GetStyle("slot background"));
                slots[k] = itemSlot[k];
                if (slots[k].itemName != null)
                {
                    GUI.DrawTexture(slotRect, slots[k].itemIcon);
                    if (slots[k].itemAmount > 1)
                    {
                        GUI.TextArea(textRect, itemSlot[k].itemAmount.ToString());
                    }

                    if ((e.mousePosition.x < xMin || e.mousePosition.x > xMax || e.mousePosition.y < yMin || e.mousePosition.y > yMax) && !slotRect.Contains(e.mousePosition)) { show_tool_tip = false; }

                    else if (e.mousePosition.x >= i * 55 + xMin && e.mousePosition.x <= i * 55 + xMin+slotSize && e.mousePosition.y >= j * 55 + yMin && e.mousePosition.y <= j * 55 + yMin + slotSize)
                    {
                        if (slotRect.Contains(e.mousePosition)&&Input.GetMouseButton(1))
                        {
                            tooltip = CreateTooltip(slots[k]);
                            //count번째 슬롯의 툴팁을 생성
                            if (tooltip == null) { show_tool_tip = false; }
                            else { show_tool_tip = true; }
                            //if (Input.GetMouseButtonDown(0) && Event.current.type == EventType.MouseDrag && dragItem == false)
                            //// 마우스 상태가 0이면서 동시에 '마우스 드래그' 상태인 조건
                            //{
                            //    dragItem = true;
                            //    prevIndex = count;
                            //    // 선택했던 위치를 저장함
                            //    draggedItem = slots[count];
                            //    // 아이템변수에 현재 슬롯 아이템을 저장함
                            //    itemSlot[count] = new Item();
                            //    Debug.Log(draggedItem.itemName);
                            //}
                            //if (Event.current.type == EventType.MouseDrag && dragItem)
                            //// 마우스 업 하고 드래그 하고 있는 아이템이 존재한다면,
                            //{
                            //    itemSlot[prevIndex] = itemSlot[count];
                            //    // 아이템의 전 위치에 현재 아이템을 놓고
                            //    itemSlot[count] = draggedItem;
                            //    // 현재 아이템의 위치에 드래그하고 있는 아이템을 놓고
                            //    dragItem = false;
                            //    // 드래그 옵션은 false로 종료하고
                            //    draggedItem = null;
                            //    // 드래그 중인 아이템은 없는걸로 하고
                            //}
                        }
                        else { show_tool_tip = false; }
                    }
                }
                if(tooltip == "") { show_tool_tip = false; }
                else
                {
                    count++;

                    //if (slotRect.Contains(Event.current.mousePosition))
                    //{
                    //    if (Event.current.type == EventType.MouseUp && dragItem)
                    //    {
                    //        //빈 공간으로의 아이템 옮기기 기능
                    //        itemSlot[count] = draggedItem;
                    //        dragItem = false;
                    //        draggedItem = null;
                    //    }
                    //}
                }
                k++;
            }
        }
    }
    string CreateTooltip(Item item)
    {
        tooltip = "Item name: <color=#a10000><b>" + item.itemName + "</b></color>";
        return tooltip;
    }
    void AddItem(int id)
    {
        for (int i = 0; i < itemSlot.Count; i++)        // 전체 인벤토리를 모두 검색
        {
            if (itemSlot[i].itemName == null)           // 인벤토리가 빈자리면 
            {
                for (int j = 0; j < db.item.Count; j++) // 추가한 값까지 모두 검색
                {
                    if (db.item[j].itemID == id)        // 디비의 아이템의 ID와 입력한 ID가 같다면
                    {
                        itemSlot[i] = db.item[j];       // 빈 인벤토리에 db에 저장된 아이템 적용
                        return;                         // 함수를 마무리.
                    }
                }
            }
        }
    }
    void RemoveItem(int id)
    {
        int i;
        for (i = 0; i < itemSlot.Count; i++)
        {
            if (itemSlot[i].itemID == id)
            {
                itemSlot[i] = new Item();
                return;
            }
            return;
        }
    }
}