using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
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

        //플레이어 스탯
        public string p_Name;
        public int p_LV;
        public int p_MaxHP;
        public int p_CurHP;
        public int p_STR;   //힘
        public int p_AGI;   //민
        public int p_INT;   //지
        public int p_ATK;  //공격력
        public float p_ASP;     //공속
        public float p_MSP;    //이속
        public float p_JMP;     //점프력
        public float p_KBP;     //넉백력
        public int p_KBR;       //넉백저항
        public int p_HSR;       //경직저항
        public float p_GPT;     //무적시간
        public float p_CurEXP; //현재경험치
        public float p_ReqEXP; //필요경험치
        public int p_SP;          //스탯포인트
    }

    public SaveData saveData;
    private PlayerStatus playerStatus;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
            Debug.Log("F5!");
        }
    }

    public void SaveGame()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
        saveData.scene = SceneManager.GetActiveScene().name;
        saveData.p_X = playerStatus.transform.position.x;
        saveData.p_Y = playerStatus.transform.position.y;
        saveData.p_Z = playerStatus.transform.position.z;
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

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/SaveFile.dat");

        bf.Serialize(file, saveData);
        file.Close();
        Debug.Log(Application.dataPath + "의 위치에 저장했습니다.");
    }
}
