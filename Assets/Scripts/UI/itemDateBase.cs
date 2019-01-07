using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDateBase : MonoBehaviour
{
    public List<Item> item = new List<Item>();

    void Start()
    {
        //==========================아래와 같은 형식으로 추가하면 됨.=========================
        //item.Add(new Item("Weapon", 1000, "무기 ID는 1000번 부터.", Item.ItemType.Weapon));
        //item.Add(new Item("Shield", 2000, "방패 ID는 2000번 부터.", Item.ItemType.Weapon));
        //====================================================================================
        item.Add(new Item("7_0", "주먹", 1000, "무기 ID는 1000번 부터.", Item.ItemType.Weapon, 1, 1));
        item.Add(new Item("8_2", "창", 1001, "무기 ID는 1000번 부터.", Item.ItemType.Weapon, 9, 8));
    }

}
