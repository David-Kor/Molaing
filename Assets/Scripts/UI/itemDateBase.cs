using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDateBase : MonoBehaviour
{
    public List<Item> item = new List<Item>();

    void Start()
    {
        //item.Add(new Item("아이템 이름", 아이템 코드(중복 안되게 주의), "아이템 설명", 아이템 타입, 슬롯 당 최대 소지량)
        item.Add(new Item("소형HP포션", 9001, "가장 작은 크기의 HP포션.\nHP를 ???만큼 회복시킨다.", Item.ItemType.Use, 5));
        item.Add(new Item("중형HP포션", 9002, "중간 크기의 HP포션.\nHP를 ???만큼 회복시킨다.", Item.ItemType.Use, 5));
        item.Add(new Item("대거", 1001, "가장 흔하게 볼 수 있는 대거.", Item.ItemType.Weapon, 1));
        item.Add(new Item("롱소드", 1101, "가장 흔하게 볼 수 있는 장검.", Item.ItemType.Weapon, 1));
    }

}
