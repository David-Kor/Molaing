using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollPractice : MonoBehaviour {

    ScrollRect scrollRect;
    GameObject content;
    SkillWindowSlot SkillWindow;
    
    private int count = 0;
	// Use this for initialization
	void Start ()
    {
        scrollRect = GetComponent<ScrollRect>();
        content = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        SkillWindow = content.transform.GetChild(0).GetComponent<SkillWindowSlot>();

        for (int i = 0; i < Database.skill_list.Count; i++)
        {
            AddSkill(Database.skill_list[i], i);
        }
	}
    public void AddSkill(GameObject skill_prefab, int num)
    {
        SkillWindow.Start();
        Skill skill = skill_prefab.GetComponent<Skill>();
        SkillWindow.skillName.text = skill.name;
        SkillWindow.skillDes.text = skill.detail;
        
        gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite = Database.skill_list[num].GetComponent<Skill>().skill_IMG;

        Instantiate(SkillWindow, SkillWindow.transform.parent).GetComponent<SkillWindowSlot>().SetSkillObject(skill_prefab);
        content.transform.GetChild(content.transform.childCount - 1).gameObject.SetActive(true);
        SkillWindow.SetContentSize();
    }
}
