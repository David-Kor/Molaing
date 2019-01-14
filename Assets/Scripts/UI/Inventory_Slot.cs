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
    public GUISkin skin;
    public bool showTooltip;
    private bool bDragItem;
    private GameObject thisSlot;
    private Image image;


    RaycastHit hit;
    GameObject target = null;
    GameObject mainCamera;
    Image thisImage;
    Text thisText;

    private string tooltip;
    private bool itemOption;
    void Start()
    {
        showTooltip = false;
        bDragItem = false;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").gameObject;
        image = mainCamera.transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<Image>();
        takeObject();
    }


    void CastRay()
    {
        target = null;

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null)
        {
            target = hit.collider.gameObject;  //히트 된 게임 오브젝트를 타겟으로 지정
        }
    }

    public void takeObject()
    {
        if (icon == null) { icon = transform.GetChild(1).GetComponent<Image>(); }
        if (itemCount == null) { itemCount = transform.GetChild(0).GetComponent<Text>(); }
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
        Amount = i;
        if (item.itemType ==  5 || item.itemType == 6)
        {
            if (item.itemAmount > 0) { itemCount.text = i.ToString(); }
            else { RemoveItem(); }      //아이템의 갯수가 0보다 작으면 RemoveItem()을 이용하여 슬롯을 비움.
        }
        else { itemCount.text = ""; }   //소비템이나 재료템이 아니면 아이템 갯수 표시 안함. 장비는 어차피 한 슬롯에 1개가 최대.
    }
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        GUI.skin = skin;
        if (showTooltip && !mainCamera.GetComponent<UI_Controller>().bMouse0Down)
        {
            style.richText = true;
            GUI.Box(new Rect(Event.current.mousePosition.x + 5, Event.current.mousePosition.y + 2, 200, 200), tooltip, skin.GetStyle("tooltip"));
        }
        //showTooltip이 true가 되면 마우스를 따라다니는 툴팁틀 생성한다.
        if (mainCamera.GetComponent<UI_Controller>().bMouse0Down) { GUI.DrawTexture(new Rect(Input.mousePosition.x, Event.current.mousePosition.y, 50, 50), image.mainTexture); }

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
            thisImage = this.transform.GetChild(1).GetComponent<Image>();
            image.sprite = thisImage.sprite;
            image.transform.position = Input.mousePosition;
            mainCamera.GetComponent<UI_Controller>().bMouse0Down = true;
        }
    }

    public void OnMouseUp()
    {
        if (this.itemID == 0)
        {
            return;
        }
        else
        {
            CastRay();
            if (target == null)
            {
                return;
            }
            else if (target.CompareTag("ItemSlot"))
            {
                if (target.GetComponent<Inventory_Slot>().itemID == 0)
                {
                    if (target.CompareTag("Weapon") || (thisSlot.GetComponent<Inventory_Slot>().itemID >= 1001 && thisSlot.GetComponent<Inventory_Slot>().itemID <= 1999))
                    {
                        MoveSlot(target);
                        RemoveItem();
                    }
                    else if (target.CompareTag("Head") || (thisSlot.GetComponent<Inventory_Slot>().itemID >= 2001 && thisSlot.GetComponent<Inventory_Slot>().itemID <= 2999))
                    {
                        MoveSlot(target);
                        RemoveItem();
                    }
                    else if (target.CompareTag("Body") || (thisSlot.GetComponent<Inventory_Slot>().itemID >= 3001 && thisSlot.GetComponent<Inventory_Slot>().itemID <= 3999))
                    {
                        MoveSlot(target);
                        RemoveItem();
                    }
                    else if (target.CompareTag("Hand") || (thisSlot.GetComponent<Inventory_Slot>().itemID >= 4001 && thisSlot.GetComponent<Inventory_Slot>().itemID <= 4999))
                    {
                        MoveSlot(target);
                        RemoveItem();
                    }
                    else if (target.CompareTag("Foot") || (thisSlot.GetComponent<Inventory_Slot>().itemID >= 5001 && thisSlot.GetComponent<Inventory_Slot>().itemID <= 5999))
                    {
                        MoveSlot(target);
                        RemoveItem();
                    }
                    else if (target.CompareTag("ItemSlot"))
                    {
                        MoveSlot(target);
                        RemoveItem();
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                SwapSlot();
            }
            bDragItem = false;
            mainCamera.GetComponent<UI_Controller>().bMouse0Down = false;
        }
    }

    void MoveSlot(GameObject gameSlot)
    {
        if (gameSlot.CompareTag("ItemSlot"))
        {
            gameSlot.GetComponent<Inventory_Slot>().itemID = thisSlot.GetComponent<Inventory_Slot>().itemID;
            gameSlot.GetComponent<Inventory_Slot>().Amount = thisSlot.GetComponent<Inventory_Slot>().Amount;
        }
        else
        {
            gameSlot.GetComponent<Inventory_Slot>().itemID = thisSlot.GetComponent<Inventory_Slot>().itemID;
            gameSlot.GetComponent<Inventory_Slot>().Amount = thisSlot.GetComponent<Inventory_Slot>().Amount;
        }

        gameSlot.transform.GetChild(0).GetComponent<Text>().text = thisText.text;
        gameSlot.transform.GetChild(1).GetComponent<Image>().sprite = thisImage.sprite;
        gameSlot.transform.GetChild(1).gameObject.SetActive(true);
    }
    void SwapSlot()
    {
        int i;
        Sprite sprite;
        i = this.gameObject.GetComponent<Inventory_Slot>().itemID;
        this.gameObject.GetComponent<Inventory_Slot>().itemID = target.GetComponent<Inventory_Slot>().itemID;
        target.GetComponent<Inventory_Slot>().itemID = i;

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
}