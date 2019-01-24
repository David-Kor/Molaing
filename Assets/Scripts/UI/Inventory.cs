using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public Inventory_Slot[] slots;                  //아이템 슬롯
    public Inventory_Slot[] eSlots;
    bool bRemove = true;

    


    private int prevIndex;
    private string tooltip;
    void Start()
    {
        AddObject();
        instance = this;
        for (int i = 0; i < 49; i++)
        {
            slots[i] = gameObject.transform.GetChild(1).GetChild(0).GetChild(i).GetComponentInChildren<Inventory_Slot>();
        }
        for (int i = 0; i < 5; i++)
        {
            eSlots[i] = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetComponent<Inventory_Slot>();
        }
        RemoveSlot();       //처음에 인벤토리 슬롯들의 이미지가 전부 하얀 사각형이니 일괄적으로 비활성화.

        //=====아래는 테스트를 위한 내용.
        //아이템 획득하는 함수를 실행시키려면 UI/PickupItem.cs 참조.
        //만약 스타팅 아이템을 넣어주고 싶으면 이런 식으로 입력.
        GetItem(9001);
        GetItem(9002);
        GetItem(9001);
        GetItem(9001);
        GetItem(9001);
        GetItem(9001);
        GetItem(1001);
        GetItem(9001);
        GetItem(1001);
        GetItem(1001);
        GetItem(1001);
        GetItem(1001);
        GetItem(1001);
        GetItem(1001);
        GetItem(1001);
        GetItem(1001);
        GetItem(1001);
        GetItem(1001);
        GetItem(1001);
        GetItem(1001);
        //====테스트 내용 끝.
    }

    public void RemoveSlot()
    {
        if (bRemove == true)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].RemoveItem();       //쓰레기값을 방지하기 위해 슬롯 일괄 초기화.
                slots[i].transform.GetChild(1).gameObject.SetActive(false);
            }
            for (int i = 0; i < eSlots.Length; i++)
            {
                eSlots[i].RemoveItem();
                eSlots[i].transform.GetChild(1).gameObject.SetActive(false);
            }
            bRemove = false;
        }
    }

    public void GetItem(int itemID, int count = 1)
    {
        for (int i = 0; i < Database.item.Count; i++)
        {
            if (itemID == Database.item[i].itemID)        //Database에 등록된 아이템 중 전달받은 itemID를 가진 아이템이 있는지 확인.
            {
                for (int j = 0; j < slots.Length; j++)
                {
                    if (slots[j].itemID == itemID)  //슬롯에 해당 itemID에 해당하는 아이템이 들어있는 슬롯을 확인.
                    {
                        if (slots[j].Amount == Database.item[i].maxAmount)    //해당 슬롯에 들어있는 아이템의 갯수가 Max면 다음 슬롯으로 넘어감.
                        {
                            continue;
                        }
                        slots[j].Amount += count;                       //슬롯에 아이템의 갯수를 +1해줌.
                        slots[j].GetComponent<Inventory_Slot>().AddItem(Database.item[i], slots[j].Amount);
                        return;
                    }
                    if (slots[j].itemID != 0)        //전달받은 itemID와 다른 ID를 슬롯이 가지고 있으면 다음 슬롯을 검색.
                    {
                        continue;
                    }
                    slots[j].transform.GetChild(1).gameObject.SetActive(true);
                    slots[j].GetComponent<Inventory_Slot>().AddItem(Database.item[i], count);
                    return;     //슬롯에 아이템을 1개 추가하고 슬롯의 아이템 이미지를 활성화.
                }
            }
        }
    }

    public void GetItem(int itemID, int slotNumber, int count = 1)
    {
        if(slotNumber < 0) { return; }
        else if(count < 0) { return; }

        for(int i = 0; i < Database.item.Count; i++)
        {
            if (itemID == Database.item[i].itemID)
            {
                slots[slotNumber].itemID = itemID;
                slots[slotNumber].Amount = count;
                slots[slotNumber].transform.GetChild(1).gameObject.SetActive(true);
                slots[slotNumber].GetComponent<Inventory_Slot>().AddItem(Database.item[i], count);
            }
        }
    }

    void AddObject()
    { 
        slots = new Inventory_Slot[49];
        eSlots = new Inventory_Slot[5];
    }
    
}