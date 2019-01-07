using UnityEngine;
using System.Collections;

[System.Serializable]

public class Item
{
    public string itemName;         // 아이템의 이름
    public int itemID;              // 아이템의 고유번호
    public string itemDes;          // 아이템의 설명
    public ItemType itemType;       // 아이템의 속성 설정
    public Texture itemIcon;      // 아이템의 아이콘(2D)
    public int maxAmount;
    public int itemAmount;
    // 아이템의 속성 설정에 대한 갯수.
    // 추후 추가 가능
    public enum ItemType            
    {
        Weapon, 
        shield
    }

    public Item()   //빈 아이템 슬롯 객체를 생성할 때 사용
    {

    }

    public Item(string img, string name, int id, string desc, ItemType type, int Max, int amount)
    // 아이템의 필요한 속성을 모두 위에 적을 것.
    // 아이템 추가는 itemDataBase에서 할 것.
    {
        itemIcon = Resources.Load<Texture2D>("ItemIcons/34x34icons180709_"+img);
        itemName = name;
        itemID = id;
        itemDes = desc;
        itemType = type;
        maxAmount = Max;
        itemAmount = amount;
    }
}
