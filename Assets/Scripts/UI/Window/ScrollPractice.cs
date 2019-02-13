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
        AddSkill("강타", "");
        AddSkill("이기어검", "");
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
    public void AddSkill(string name, string des)
    {
        Debug.Log("스킬 추가 시작");
        SkillWindow.Start();
        SkillWindow.skillName.text = name;
        SkillWindow.skillDes.text = des;
        Debug.Log(gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1));
        for(int i = 0; i<Database.skill_list.Count; i++)
        {
            if(Database.skill_list[i].name.Equals(name))
            {
                gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite = Database.skill_list[i].GetComponent<Skill>().skill_IMG;
            }
        }
        Instantiate(SkillWindow, SkillWindow.transform.parent);
        content.transform.GetChild(content.transform.childCount - 1).gameObject.SetActive(true);
        SkillWindow.SetContentSize();
        SetContentSize();
    }
}
