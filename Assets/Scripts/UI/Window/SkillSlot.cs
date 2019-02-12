using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public Image skillIcon;
    bool enterSkillSlot;
    public string skillName;
    GameObject mainCamera;

    // Use this for initialization
    void Start()
    {
        skillIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
        mainCamera = Camera.main.gameObject;
    }

    void OnGUI()
    {
        if (mainCamera.GetComponent<UI_Controller>().bMouse0Down && mainCamera.GetComponent<UI_Controller>().bInventory == false)
        {
            GUI.DrawTexture(new Rect(Input.mousePosition.x, Event.current.mousePosition.y, 32, 32), skillIcon.mainTexture);
        }
    }
    public void OnMouseDown()
    {
        if (enterSkillSlot == true)
        {
            mainCamera.GetComponent<UI_Controller>().bMouse0Down = true;
        }
    }
    public void OnMouseUp()
    {
        mainCamera.GetComponent<UI_Controller>().bMouse0Down = false;
    }
    public void OnMouseEnter()
    {
        enterSkillSlot = true;
    }
    public void OnMouseExit()
    {
        enterSkillSlot = false;
    }
}
