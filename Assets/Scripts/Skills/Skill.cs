using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 모든 스킬들의 최상위 가상 클래스 */
public class Skill
{
    public string skillName;
    public GameObject[] target;

    /* 생성자 */
    public Skill(string _name)
    {
        skillName = _name;
    }

}
