using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveNLoad : MonoBehaviour
{
    [System.Serializable]
    public class SaveData
    {
        //저장된 씬의 이름
        public string scene;

        //플레이어 위치
        public float p_X;
        public float p_Y;
        public float p_Z;

        //헬퍼 위치
        public float h_X;
        public float h_Y;
        public float h_Z;

        //플레이어 스탯
        public string p_Name;
        public int p_LV;
        public int p_MaxHP;
        public int p_CurHP;
        public int p_STR;   //힘
        public int p_AGI;   //민
        public int p_INT;   //지
        public int p_ATK;  //공격력
        public float p_ASP;      //공속
        public float p_MSP;     //이속
        public float p_JMP;      //점프력
        public float p_KBP;      //넉백력
        public int p_KBR;        //넉백저항
        public int p_HSR;        //경직저항
        public float p_GPT;      //무적시간
        public float p_CurEXP;  //현재경험치
        public float p_ReqEXP; //필요경험치
        public int p_SP;          //스탯포인트


        public List<string> p_SkillList;        //스킬 리스트(스킬명)
        public float[] p_CoolDownList;        //스킬 쿨타임

        //인벤토리 내용
        public List<int> p_invenItemID;         //아이템 ID
        public List<int> p_invenItemAmount;  //아이템 갯수
        public List<int> p_equipItemID;
    }

    public SaveData saveData;
    public static bool actLoad;
    public static int loadNum;

    private PlayerStatus playerStatus;
    private PlayerSkill playerSkill;
    private PlayerControl playerControl;
    private Helper helper;
    private Inventory inventory;
    private UI_Controller ui;


    void Start()
    {
        if (actLoad)
        {
            StartCoroutine("LoadingWork");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            GameSave();
            Debug.Log("F5!");
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            GameLoad();
            Debug.Log("F6!");
        }
    }


    private void InventoryVisible(bool _set)
    {
        int i;
        Image[] i_imgs = inventory.GetComponentsInChildren<Image>();
        RawImage[] i_rimgs = inventory.GetComponentsInChildren<RawImage>();
        Text[] i_txt = inventory.GetComponentsInChildren<Text>();

        for (i = 0; i < i_imgs.Length; i++)
        {
            i_imgs[i].enabled = _set;
        }
        for (i = 0; i < i_rimgs.Length; i++)
        {
            i_rimgs[i].enabled = _set;
        }
        for (i = 0; i < i_txt.Length; i++)
        {
            i_txt[i].enabled = _set;
        }
    }


    /* 현재 상태를 세이브 파일로 기록하는 Coroutine */
    private IEnumerator SavingWork()
    {
        yield return null;
        int i = 0;

        playerStatus = FindObjectOfType<PlayerStatus>();
        playerSkill = FindObjectOfType<PlayerSkill>();
        helper = FindObjectOfType<Helper>();
        ui = FindObjectOfType<UI_Controller>();
        playerControl = FindObjectOfType<PlayerControl>();
        //인벤토리 컴포넌트를 받기 위해 일시적 활성화
        ui.Control_Inventory(true);
        inventory = FindObjectOfType<Inventory>();
        //인벤토리의 모습을 잠시 숨기고 초기화를 기다림
        InventoryVisible(false);
        yield return null;
        InventoryVisible(true);
        ui.Control_Inventory(playerControl.GetInventoryActive());
        saveData.scene = SceneManager.GetActiveScene().name;
        saveData.p_X = playerStatus.transform.position.x;
        saveData.p_Y = playerStatus.transform.position.y;
        saveData.p_Z = playerStatus.transform.position.z;
        saveData.h_X = helper.transform.position.x;
        saveData.h_Y = helper.transform.position.y;
        saveData.h_Z = helper.transform.position.z;
        saveData.p_Name = playerStatus.objName;
        saveData.p_LV = playerStatus.level;
        saveData.p_MaxHP = playerStatus.maxHP;
        saveData.p_CurHP = playerStatus.currentHP;
        saveData.p_STR = playerStatus.strength;
        saveData.p_AGI = playerStatus.agility;
        saveData.p_INT = playerStatus.intelligence;
        saveData.p_ATK = playerStatus.attackDamage;
        saveData.p_ASP = playerStatus.attackSpeed;
        saveData.p_MSP = playerStatus.moveSpeed;
        saveData.p_JMP = playerStatus.jumpPower;
        saveData.p_KBP = playerStatus.knockBackPower;
        saveData.p_KBR = playerStatus.knockBackResistance;
        saveData.p_HSR = playerStatus.hitStunResistance;
        saveData.p_GPT = playerStatus.gracePeriodTime;
        saveData.p_CurEXP = playerStatus.currentEXP;
        saveData.p_ReqEXP = playerStatus.requireEXP;
        saveData.p_SP = playerStatus.statusPoint;
        saveData.p_CoolDownList = playerSkill.coolTimerList;

        saveData.p_SkillList = new List<string>();
        for (i = 0; i < playerSkill.skill_List.Length; i++)
        {
            //스킬 슬롯이 비어있으면 무시
            if (playerSkill.skill_List[i] == null) { continue; }
            saveData.p_SkillList.Add(playerSkill.skill_List[i].GetComponent<Skill>().skillName);
        }

        //인벤토리의 각 슬롯들 저장
        saveData.p_invenItemID = new List<int>();
        saveData.p_invenItemAmount = new List<int>();
        for (i = 0; i < inventory.slots.Length; i++)
        {
            //해당 슬롯의 아이템 ID와 개수를 저장
            saveData.p_invenItemID.Add(inventory.slots[i].itemID);
            saveData.p_invenItemAmount.Add(inventory.slots[i].Amount);
        }



        //장착된 장비 슬롯 저장
        saveData.p_equipItemID = new List<int>();
        for (i = 0; i < inventory.eSlots.Length; i++)
        {
            saveData.p_equipItemID.Add(inventory.eSlots[i].itemID);
        }

        BinaryFormatter bf = new BinaryFormatter();

        //"Save"디렉토리가 존재하지 않는 경우 새 디렉토리 생성
        if (!Directory.Exists(Application.dataPath+ "/Save"))
        {
            Directory.CreateDirectory(Application.dataPath+ "/Save");
        }

        FileStream file = File.Create(Application.dataPath + "/Save/SaveFile.dat");

        bf.Serialize(file, saveData);
        file.Close();
    }

    
    /* 세이브 파일을 읽어서 적용시키는 Coroutine */
    private IEnumerator LoadingWork()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/Save/SaveFile.dat", FileMode.Open);

        if (file != null && file.Length > 0)
        {
            int i = 0;

            saveData = bf.Deserialize(file) as SaveData;
            playerStatus = FindObjectOfType<PlayerStatus>();
            playerSkill = FindObjectOfType<PlayerSkill>();
            helper = FindObjectOfType<Helper>();
            ui = FindObjectOfType<UI_Controller>();
            playerControl = FindObjectOfType<PlayerControl>();
            //인벤토리 컴포넌트를 받기 위해 일시적 활성화
            ui.Control_Inventory(true);
            inventory = FindObjectOfType<Inventory>();
            //인벤토리의 모습을 잠시 숨기고 초기화를 기다림
            InventoryVisible(false);
            yield return null;
            InventoryVisible(true);
            ui.Control_Inventory(playerControl.GetInventoryActive());
            playerStatus.transform.position = new Vector3(saveData.p_X, saveData.p_Y, saveData.p_Z);
            helper.transform.position = new Vector3(saveData.h_X, saveData.h_Y, saveData.h_Z);
            playerStatus.objName = saveData.p_Name;
            playerStatus.level = saveData.p_LV;
            playerStatus.maxHP = saveData.p_MaxHP;
            playerStatus.currentHP = saveData.p_CurHP;
            playerStatus.strength = saveData.p_STR;
            playerStatus.agility = saveData.p_AGI;
            playerStatus.intelligence = saveData.p_INT;
            playerStatus.attackDamage = saveData.p_ATK;
            playerStatus.attackSpeed = saveData.p_ASP;
            playerStatus.moveSpeed = saveData.p_MSP;
            playerStatus.jumpPower = saveData.p_JMP;
            playerStatus.knockBackPower = saveData.p_KBP;
            playerStatus.knockBackResistance = saveData.p_KBR;
            playerStatus.hitStunResistance = saveData.p_HSR;
            playerStatus.gracePeriodTime = saveData.p_GPT;
            playerStatus.currentEXP = saveData.p_CurEXP;
            playerStatus.requireEXP = saveData.p_ReqEXP;
            playerStatus.statusPoint = saveData.p_SP;

            playerSkill.coolTimerList = saveData.p_CoolDownList;

            //장착된 스킬 로드
            for (i = 0; i < saveData.p_SkillList.Count; i++)
            {
                playerSkill.skill_List[i] = Database.SkillNameToObject(saveData.p_SkillList[i]);
            }

            //인벤토리의 각 슬롯별 아이템 불러오기
            for (i = 0; i < inventory.slots.Length; i++)
            {
                inventory.GetItem(saveData.p_invenItemID[i], i, saveData.p_invenItemAmount[i]);
            }

            //장비 장착슬롯별 아이템 불러오기
            for (i = 0; i < inventory.eSlots.Length; i++)
            {
                inventory.GetEItem(saveData.p_equipItemID[i], i);
            }

        }
        else
        {
            Debug.Log("세이브 파일을 찾을 수 없습니다.");
        }
        file.Close();
        actLoad = false;
    }


    /* 게임 세이브를 원할 때 호출 */
    public void GameSave()
    {
        StartCoroutine("SavingWork");
    }


    /* 게임을 로드할 때 호출 */
    public void GameLoad()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/Save/SaveFile.dat", FileMode.Open);

        if (file != null && file.Length > 0)
        {
            //actLoad를 활성화시키고 씬을 불러옴
            actLoad = true;
            SceneManager.LoadScene((bf.Deserialize(file) as SaveData).scene);
        }
        file.Close();
    }
}
