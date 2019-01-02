using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public List<Item> itemSlot = new List<Item>();

    public int slotX, slotY;    //저장공간의 가로 세로 지정. 총 slotX * slotY의 공간이 생성됨.

    public itemDateBase db;
    public GUISkin skin;
    public List<Item> slots = new List<Item>();
    
    void Start()
    {
        for (int i = 0; i < slotX * slotY; i++)
        {
            itemSlot.Add(new Item());       //아이템 저장공간 생성.
            slots.Add(new Item());          //생성된 저장공간 비워줌.
        }
        db = GameObject.FindGameObjectWithTag("Item DataBase").GetComponent<itemDateBase>();
        itemSlot.Add(db.item[0]);
    }

    void OnGUI()
    {
        GUI.skin = skin;
        if(gameObject.activeSelf == true) { DrawItemslot(); }
    }

    void DrawItemslot()
    //인벤토리 슬롯을 그려주는 함수.
    //변수 잘못 건드리면 미세조정 다시 해야됨
    {
        for (int i = 0; i < slotX; i++)
        {
            for(int j = 0; j < slotY; j++)
            {
                GUI.Box(new Rect(i * 55 + 466, -j * 55 + 439, 50, 50), "", skin.GetStyle("box"));
            }
        }
    }
}
