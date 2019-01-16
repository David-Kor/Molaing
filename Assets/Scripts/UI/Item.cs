using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]

public class Item
{
    public string itemName;         // 아이템의 이름
    public int itemID;              // 아이템의 고유번호
    public string itemDes;          // 아이템의 설명
    public int itemType;       // 아이템의 속성 설정
    public Sprite itemIcon;      // 아이템의 아이콘(2D)
    public int maxAmount;
    public int itemAmount;
    public int attackPoint;
    public int healthPoint;
    public int strengthPoint;
    public int agilityPoint;
    public int intelligencePoint;
    public int healthHeal;
    // 아이템의 속성 설정에 대한 갯수.
    // 추후 추가 가능
    public enum ItemType            
    {
        Weapon,     //무기 장비의 ID는 1001~1999
        Head,       //머리 장비의 ID는 2001~2999
        Body,       //몸 장비의 ID는 3001~3999
        Hand,       //손 장비의 ID는 4001~4999
        Foot,       //발 장비의 ID는 5001~5999
        Use,        //사용 아이템의 ID는 9001~9999
        Material    //재료 아이템의 ID는 0001~0999
    }

    public Item()   //빈 아이템 슬롯 객체를 생성할 때 사용
    {

    }

    public Item(string name, int id, string desc, int type, int Max, int amount = 1)
    // 아이템의 필요한 속성을 모두 위에 적을 것.
    // 아이템 추가는 itemDataBase에서 할 것.
    {
        itemName = name;
        itemID = id;
        itemDes = desc;
        itemType = type;
        maxAmount = Max;
        itemAmount = amount;
        itemIcon = Resources.Load("ItemIcon/" + itemName, typeof(Sprite)) as Sprite;
    }
}
