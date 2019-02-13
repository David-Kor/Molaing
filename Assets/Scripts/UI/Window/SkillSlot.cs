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
    GameObject target;
    LayerMask layerMask;
    // Use this for initialization
    void Start()
    {
        skillIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
        mainCamera = Camera.main.gameObject;
    }
    public void CastRay()
    {
        target = null;

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        layerMask = 1 << 5;
        Ray2D ray = new Ray2D(pos, Vector2.zero);

        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 1000, layerMask);
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("SkillSlot"))
            {
                target = hit.collider.gameObject;
            }

        }
    }
    void OnGUI()
    {
        //if (mainCamera.GetComponent<UI_Controller>().bMouse0Down 
        //    && !mainCamera.GetComponent<UI_Controller>().bInventory
        //    && skillIcon.mainTexture != null)
        //{
        //    Debug.Log("스킬 슬롯 이미지 활성화됨");
        //    GUI.DrawTexture(new Rect(Input.mousePosition.x, Event.current.mousePosition.y, 32, 32), target.GetComponent<SkillSlot>().skillIcon.mainTexture);
        //}
    }
    public void MouseDown()
    {
        CastRay();
        if(target == null) { return; }
        if (enterSkillSlot == true)
        {
            mainCamera.GetComponent<UI_Controller>().bMouse0Down = true;
        }
    }
    public void MouseUp()
    {
        CastRay();
        mainCamera.GetComponent<UI_Controller>().bMouse0Down = false;
        if(target == null) { RemoveSkillSlot(); }
        else
        {
            if (target.GetComponent<SkillSlot>().skillName.Equals(null))
            {
                target.GetComponent<SkillSlot>().skillName = this.skillName;
                target.GetComponent<SkillSlot>().skillIcon = this.skillIcon;
                this.skillName = null;
                this.skillIcon = null;
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                string dummyString;
                Image dummyImage;
                dummyString = this.skillName;
                dummyImage = this.skillIcon;
                this.skillName = target.GetComponent<SkillSlot>().skillName;
                this.skillIcon = target.GetComponent<SkillSlot>().skillIcon;
                target.GetComponent<SkillSlot>().skillName = dummyString;
                target.GetComponent<SkillSlot>().skillIcon = dummyImage;
            }
        }
    }
    public void MouseEnter()
    {
        if(!mainCamera.GetComponent<UI_Controller>().bMouse0Down) { enterSkillSlot = true; }
    }
    public void MouseExit()
    {
        if (!mainCamera.GetComponent<UI_Controller>().bMouse0Down) { enterSkillSlot = false; }
    }
    public void RemoveSkillSlot()
    {
        skillIcon = null;
        skillName = null;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
