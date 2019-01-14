using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNLoad : MonoBehaviour
{
    void Start()
    {
        GameObject[] obj = Resources.LoadAll<GameObject>("Skills");
        //Debug.Log(obj.Length);
        for (int i = 0; i < obj.Length; i++)
        {
            //Debug.Log(obj[i].GetComponent<Skill>().skillName);
        }
    }
}
