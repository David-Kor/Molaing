using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory_Slot : MonoBehaviour
{
    public Text itemCount;
    public Image icon;
    public int itemID;
    public int Amount;
    public int nstSlot;
    public int itemType;
    public LayerMask layerMask;
    public GUISkin skin;
    public bool showTooltip;
    private bool bDragItem;
    GameObject thisSlot;
    private Image image;

    public Inventory_Slot thisObject;
    public Inventory_Slot targetObject;


    RaycastHit hit;
    GameObject target = null;
    GameObject mainCamera;
    Image thisImage;
    Text thisText;
    float floatx;
    float floaty;

    private string tooltip;
    private bool itemOption;

    void Start()
    {
        mainCamera = Camera.main.gameObject;
        Debug.Log(mainCamera);
        takeObject();
        showTooltip = false;
        bDragItem = false;
    }
    void CastRay()
    {
        target = null;

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        layerMask = (1 << 0) | (1 << 1) | (1 << 2) | (1 << 3) | (1 << 4) | (1 << 6) | (1 << 7) | (1 << 8) | (1 << 9) | ( 1 << 10) | ( 1 << 11) | (1 << 12) | (1 << 13);
        Ray2D ray = new Ray2D(pos, Vector2.zero);

        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 14, ~layerMask);
        foreach(var hit in hits)
        {
            if ( hit.collider.gameObject.CompareTag("ItemSlot")
                || hit.collider.gameObject.CompareTag("QuickSlot")
                || hit.collider.gameObject.CompareTag("SkillSlot")
                || hit.collider.gameObject.CompareTag("Weapon")
                || hit.collider.gameObject.CompareTag("Head")
                || hit.collider.gameObject.CompareTag("Body")
                || hit.collider.gameObject.CompareTag("Hand")
                || hit.collider.gameObject.CompareTag("Foot"))
            {
                target = hit.collider.gameObject;
                targetObject = target.GetComponent<Inventory_Slot>();
            }

        }
    }

    public void takeObject()
    {
        mainCamera = Camera.main.gameObject;
        image = mainCamera.transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<Image>();
        thisObject = this.GetComponent<Inventory_Slot>();
        icon = transform.GetChild(1).GetComponent<Image>();
        itemCount = transform.GetChild(0).GetComponent<Text>();
    }
    string CreateToolTip(int itemID)
    {
        int num = 0;
        for (int i = 0; i < Database.item.Count; i++)
        {
            if (itemID == Database.item[i].itemID)
            {
                num = i;
                break;
            }
        }
        itemOption = false;
        tooltip = Database.item[num].itemName;
        tooltip += "\n" + Database.item[num].itemDes;
        tooltip += "\n\n효과";
        if (Database.item[num].attackPoint != 0)
        {
            tooltip += "\n공격력: " + Database.item[num].attackPoint;
            itemOption = true;
        }
        if (Database.item[num].healthPoint != 0)
        {
            tooltip += "\nHP: " + Database.item[num].healthPoint;
            itemOption = true;
        }
        if (Database.item[num].strengthPoint != 0)
        {
            tooltip += "\nSTR: " + Database.item[num].strengthPoint;
            itemOption = true;
        }
        if (Database.item[num].agilityPoint != 0)
        {
            tooltip += "\nAGI: " + Database.item[num].agilityPoint;
            itemOption = true;
        }
        if (Database.item[num].intelligencePoint != 0)
        {
            tooltip += "\nINT: " + Database.item[num].intelligencePoint;
            itemOption = true;
        }
        if (Database.item[num].healthHeal != 0)
        {
            tooltip += "\nHP " + Database.item[num].healthHeal;
            itemOption = true;
        }
        if (itemOption == false)
        {
            tooltip += "\n없음";
        }

        itemOption = false;
        return tooltip;
    }
    public void Tooltip()
    {
        if (itemID > 0 && !mainCamera.GetComponent<UI_Controller>().bMouse0Down)
        {
            tooltip = CreateToolTip(itemID);
            showTooltip = true;
        }
    }
    public void AddItem(Item item, int i)   //아이템 추가 함수
    {
        takeObject();
        icon.sprite = item.itemIcon;    //
        itemID = item.itemID;
        itemType = item.itemType;
        Amount = i;
        if (item.itemType == 5 || item.itemType == 6)
        {
            if (item.itemAmount > 0) { itemCount.text = i.ToString(); }
            else { RemoveItem(); }      //아이템의 갯수가 0보다 작으면 RemoveItem()을 이용하여 슬롯을 비움.
        }
        else { itemCount.text = ""; }   //소비템이나 재료템이 아니면 아이템 갯수 표시 안함. 장비는 어차피 한 슬롯에 1개가 최대.
    }
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.richText = true;
        GUI.skin.button.wordWrap = true;
        GUI.skin = skin;
        if (showTooltip)
        {
            GUI.Box(new Rect(Event.current.mousePosition.x + 5, Event.current.mousePosition.y + 5, 200, 200), tooltip, skin.GetStyle("tooltip"));
        }
        //showTooltip이 true가 되면 마우스를 따라다니는 툴팁틀 생성한다.

        if (mainCamera.GetComponent<UI_Controller>().bMouse0Down) { GUI.DrawTexture(new Rect(Input.mousePosition.x, Event.current.mousePosition.y, 32, 32), image.mainTexture); }

    }
    public void RemoveItem()
    {
        itemCount.text = "";    //인벤토리 창에서 보여지는 아이템 수량을 지워준다.
        icon = null;            //인벤토리 창에서 보여지는 아이콘을 지워준다.
        itemID = 0;             //슬롯에 저장된 itemID를 초기화시킨다.
        Amount = 0;             //슬롯에 저장된 아이템 갯수를 초기화시킨다.
        gameObject.transform.GetChild(1).gameObject.SetActive(false);       //아이콘일 표시하는 오브젝트를 비활성화 시켜준다. 안하면 슬롯에 하얀 사각형이 남음.
    }
    public void OnMouseEnter()
    {
        if (mainCamera.GetComponent<UI_Controller>().bMouse0Down == false)       //좌클릭을 안했을 때 레이캐스트 발생. 저렇게 설정한 이유는 아이템을 드래그 할 때 방해되기 때문.
        {
            CastRay();
            if (target == this.gameObject)
            {
                Tooltip();
            }
            else { showTooltip = false; }
        }
        else
        {
            showTooltip = false;
            return;
        }
    }
    public void OnMouseExit()
    {
        showTooltip = false;
    }
    public void OnMouseDown()
    {
        if (this.GetComponent<Inventory_Slot>().itemID == 0)
        {
            return;
        }
        else
        {
            thisSlot = this.GetComponent<Inventory_Slot>().gameObject;
            thisText = this.transform.GetChild(0).GetComponent<Text>();
            if (thisObject.CompareTag("QuickSlot"))
            {
                thisImage = this.transform.GetChild(0).GetComponent<Image>();
            }
            else
            {
                thisImage = this.transform.GetChild(1).GetComponent<Image>();
            }
            image.sprite = thisImage.sprite;
            image.transform.position = Input.mousePosition;
            mainCamera.GetComponent<UI_Controller>().bMouse0Down = true;
        }
        if(Input.GetMouseButtonUp(0)) { OnMouseUp(); }
    }
    public void OnMouseUp()
    {
        Debug.Log("Drop!");
        CastRay();
        Debug.Log(target);
        if (this.itemID == 0)
        {
            Debug.Log("here");
            mainCamera.GetComponent<UI_Controller>().bMouse0Down = false;
            return;
        }
        else if (target == null)
        {
            mainCamera.GetComponent<UI_Controller>().bMouse0Down = false;
            return;
        }
        else if (!thisObject.CompareTag("ItemSlot"))
        {
            if (targetObject.CompareTag("ItemSlot") && targetObject.itemID != 0)
            {
                thisSlot = null;
                thisText = null;
                thisImage = null;
                image.sprite = null;
                mainCamera.GetComponent<UI_Controller>().bMouse0Down = false;
                showTooltip = false;
                return;
            }
            else if (targetObject.CompareTag("ItemSlot") && targetObject.itemID == 0)
            {
                MoveSlot();
                this.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        else
        {
            if (targetObject.CompareTag("ItemSlot"))
            {
                if (targetObject.itemID == 0)
                {
                    MoveSlot();
                }
                else
                {
                    SwapSlot();
                }
            }
            else if(targetObject.CompareTag("QuickSlot"))
            {
                EquipSlot();
            }
            else
            {
                if (targetObject.itemID == 0)
                {
                    if (target.CompareTag("Weapon") && thisObject.itemType == 0)
                    {
                        MoveSlot();
                    }
                    else if (target.CompareTag("Head") && thisObject.itemType == 1)
                    {
                        MoveSlot();
                    }
                    else if (target.CompareTag("Body") && thisObject.itemType == 2)
                    {
                        MoveSlot();
                    }
                    else if (target.CompareTag("Hand") && thisObject.itemType == 3)
                    {
                        MoveSlot();
                    }
                    else if (target.CompareTag("Foot") && thisObject.itemType == 4)
                    {
                        MoveSlot();
                    }
                    else
                    {
                        mainCamera.GetComponent<UI_Controller>().bMouse0Down = false;
                        showTooltip = false;
                        return;
                    }
                }
                else if (targetObject.itemID != 0)
                {
                    if (target.CompareTag("Weapon") && thisObject.itemType == 0)
                    {
                        SwapSlot();
                    }
                    else if (target.CompareTag("Head") && thisObject.itemType == 1)
                    {
                        SwapSlot();
                    }
                    else if (target.CompareTag("Body") && thisObject.itemType == 2)
                    {
                        SwapSlot();
                    }
                    else if (target.CompareTag("Hand") && thisObject.itemType == 3)
                    {
                        SwapSlot();
                    }
                    else if (target.CompareTag("Foot") && thisObject.itemType == 4)
                    {
                        SwapSlot();
                    }
                    else
                    {
                        mainCamera.GetComponent<UI_Controller>().bMouse0Down = false;
                        showTooltip = false;
                        return;
                    }
                }
                else if (target.CompareTag("QuickSlot"))
                {
                    if (thisObject.itemType == 5)
                    {

                    }
                }
                else
                {
                    SwapSlot();
                }
            }
            bDragItem = false;
        }
        mainCamera.GetComponent<UI_Controller>().bMouse0Down = false;
        showTooltip = false;
    }

    void MoveSlot()
    {
        if (targetObject.CompareTag("ItemSlot"))
        {
            targetObject.itemID = thisObject.itemID;
            targetObject.Amount = thisObject.Amount;
        }
        else
        {
            targetObject.itemID = thisObject.itemID;
            targetObject.Amount = thisObject.Amount;
        }

        target.transform.GetChild(0).GetComponent<Text>().text = thisText.text;
        target.transform.GetChild(1).GetComponent<Image>().sprite = thisImage.sprite;
        RemoveItem();
        target.transform.GetChild(1).gameObject.SetActive(true);
        this.transform.GetChild(1).gameObject.SetActive(false);
    }
    void SwapSlot()
    {
        int i;
        Sprite sprite;
        i = this.gameObject.GetComponent<Inventory_Slot>().itemID;
        this.gameObject.GetComponent<Inventory_Slot>().itemID = target.gameObject.GetComponent<Inventory_Slot>().itemID;
        target.gameObject.GetComponent<Inventory_Slot>().itemID = i;

        i = this.gameObject.GetComponent<Inventory_Slot>().Amount;
        this.gameObject.GetComponent<Inventory_Slot>().Amount = target.gameObject.transform.GetComponent<Inventory_Slot>().Amount;
        target.gameObject.transform.GetComponent<Inventory_Slot>().Amount = i;

        sprite = this.gameObject.transform.GetChild(1).GetComponent<Image>().sprite;
        this.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = target.transform.GetChild(1).GetComponent<Image>().sprite;
        target.transform.GetChild(1).GetComponent<Image>().sprite = sprite;

        if (this.gameObject.GetComponent<Inventory_Slot>().Amount > 1)
        {
            this.gameObject.transform.GetChild(0).GetComponent<Text>().text = this.gameObject.GetComponent<Inventory_Slot>().Amount.ToString();
        }
        else
        {
            this.gameObject.transform.GetChild(0).GetComponent<Text>().text = "";
        }
        if (target.GetComponent<Inventory_Slot>().Amount > 1)
        {
            target.transform.GetChild(0).GetComponent<Text>().text = target.GetComponent<Inventory_Slot>().Amount.ToString();
        }
        else
        {
            target.transform.GetChild(0).GetComponent<Text>().text = "";
        }
    }

    void EquipSlot()
    {
        int count = 0;
        for (int i = 0; i < 49; i++)
        {
            if (thisObject.transform.parent.GetChild(i).GetComponent<Inventory_Slot>().itemID == thisObject.itemID)
            {
                count += thisObject.transform.parent.GetChild(i).GetComponent<Inventory_Slot>().Amount;
            }
        }
        targetObject.itemID = thisObject.itemID;
        targetObject.Amount = count;
        target.transform.GetChild(0).GetComponent<Image>().sprite = thisImage.sprite;
        target.transform.GetChild(0).gameObject.SetActive(true);
        target.transform.GetChild(3).GetComponent<Text>().text = targetObject.Amount.ToString();
    }
}