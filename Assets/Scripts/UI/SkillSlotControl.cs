using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlotControl : MonoBehaviour
{
    private SkillSlot[] slots;
    private PlayerSkill p_skill;

    void Start()
    {
        p_skill = FindObjectOfType<PlayerSkill>();
        slots = GetComponentsInChildren<SkillSlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            if (p_skill.skill_List[i + 2] != null)
            {
                slots[i].SetSkillImage(p_skill.skill_List[i + 2].GetComponent<Skill>().skill_IMG);
            }
        }
    }

    public void UpdateSlot(int slot_num, GameObject skillObj)
    {
        Sprite img;
        if (skillObj != null)
        {
            img = skillObj.GetComponent<Skill>().skill_IMG;
        }
        else
        {
            img = null;
        }
        transform.GetChild(0).GetChild(slot_num).GetComponent<SkillSlot>().SetSkillImage(img);
        p_skill.skill_List[slot_num + 2] = skillObj;
    }
}
