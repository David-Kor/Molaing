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
    }

}
