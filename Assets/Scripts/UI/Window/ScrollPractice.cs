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
        AddSkill("스킬이름", "설명", null);
	}
    public void SetContentSize()
    {
        RectTransform rt = (RectTransform)content.transform;
        float width = 400;
        float height;
        for(int i = 0; i < content.transform.childCount; i++)
        {
            if(content.transform.GetChild(i).gameObject.activeSelf == true)
            {
                count++;
            }
        }

        height = count * 100 + 20;
        rt.sizeDelta = new Vector2(width, height);
    }
    public void AddSkill(string name, string des, Image icon)
    {
        SkillWindow.Start();
        SkillWindow.skillDes.text = des;
        SkillWindow.skill_Icon = icon;
        Instantiate(SkillWindow, SkillWindow.transform.parent);
        content.transform.GetChild(content.transform.childCount - 1).gameObject.SetActive(true);
        SkillWindow.SetContentSize();
        SetContentSize();
    }
}
