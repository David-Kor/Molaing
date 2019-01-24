using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Database : MonoBehaviour
{
    //아이템 클래스 리스트
    public static List<Item> item = new List<Item>();
    public static List<GameObject> skill_list = new List<GameObject>();
    private static bool isInit = false;

    private const string ID = "id";
    private const string NAME = "name";
    private const string TYPE = "type";
    private const string DETAIL = "detail";
    private const string MAX = "max amount";

    void Awake()
    {
        isInit = false;
        /* 아이템 불러오기
         */
        //파싱 완료된 아이템 정보 리스트
        //(파싱된 결과값)
        Dictionary<string, List<string>> itemInfo = new Dictionary<string, List<string>>();
        int itemCount = ParsingItem(itemInfo);
        AddNewItem(itemInfo, itemCount);

        /* 스킬 불러오기
         */
        GetAllSkillInfo();
        isInit = true;
    }

    
    /* 리소스 폴더의 아이템 csv파일을 읽어서 정보를 추출 */
    private int ParsingItem(Dictionary<string, List<string>> _item_info)
    {
        //아이템 리스트 파일 불러오기
        TextAsset txt = Resources.Load("ItemList") as TextAsset;
        if (txt == null)
        {
            Debug.Log("데이터베이스에서 아이템 목록을 불러오는 데 실패했습니다.");
            return -1;
        }
        
        //텍스트파일의 문자열을 읽기 위한 객체
        StringReader sr = new StringReader(txt.text);
        //<속성,  순서>쌍
        Dictionary<string, int> _item_index = new Dictionary<string, int>();
        string line = " ";          //한 줄씩 읽은 문자열
        string[] attr;               //파싱된 속성값들
        string[] tuple;             //파싱된 value들
        bool startReadTuple = false;
        int i, count = 0;

        while (line != null)
        {
            //한 줄씩 읽는다
            line = sr.ReadLine();
            
            //전부 읽었으면 끝냄
            if (line == null) { break; }

            //주석(##로 시작하는 행)은 무시
            if (line.Substring(0, 2).Equals("##")) { continue; }

            //파싱 시작
            if (line[0] == '#')
            {
                //#attribute 다음 줄에 있는 속성들을 itemInfo의 Key로 추가
                if (line.Substring(1).ToLower().StartsWith("attribute"))
                {
                    startReadTuple = false;
                    line = sr.ReadLine();
                    attr = line.Split(',');
                    for (i = 0; i < attr.Length; i++)
                    {
                        _item_info.Add(attr[i].ToLower(), new List<string>());
                        _item_index.Add(attr[i].ToLower(), i);
                    }
                    continue;
                }
                else if (line.Substring(1).ToLower().StartsWith("tuple"))
                {
                    startReadTuple = true;
                    continue;
                }
            }

            //아이템 정보를 읽기 시작
            if (startReadTuple)
            {
                tuple = line.Split(',');
                for (i = 0; i < tuple.Length; i++)
                {
                    //속성별로 정보 추가
                    if (i == _item_index[ID]) { _item_info[ID].Add(tuple[i]); }
                    if (i == _item_index[NAME]) { _item_info[NAME].Add(tuple[i]); }
                    if (i == _item_index[TYPE]) { _item_info[TYPE].Add(tuple[i]); }
                    if (i == _item_index[DETAIL]) { _item_info[DETAIL].Add(tuple[i]); }
                    if (i == _item_index[MAX]) { _item_info[MAX].Add(tuple[i]); }
                }
                //아이템 1개 정보 추가완료
                count++;
            }
        }

        return count;
    }


    /* 파싱된 정보들을 아이템 클래스로 변환 */
    private void AddNewItem(Dictionary<string, List<string>> _item_info, int _item_count)
    {
        for (int i = 0; i < _item_count; i++)
        {
        item.Add(
                new Item(_item_info[NAME][i],
                Convert.ToInt32(_item_info[ID][i]),
                _item_info[DETAIL][i],
                Convert.ToInt32(_item_info[TYPE][i]),
                Convert.ToInt32(_item_info[MAX][i])
                )
            );
        }
    }


    /* 리소스 폴더의 모든 스킬을 받아와 skill_list에 추가 */
    private void GetAllSkillInfo()
    {
        GameObject[] getSkills = Resources.LoadAll<GameObject>("Skills");
        for (int i = 0; i < getSkills.Length; i++)
        {
            skill_list.Add(getSkills[i]);
        }
    }


    /* DB의 초기화가 끝났는지를 반환 */
    public static bool IsInitialized() { return isInit; }


    /* 스킬 리스트에서 이름으로 검색하여 프리팹반환 */
    public static GameObject SkillNameToObject(string _name)
    {
        string sName = "";
        for (int i = 0; i < skill_list.Count; i++)
        {
            sName = skill_list[i].GetComponent<Skill>().skillName;
            if (sName == _name)
            {
                return skill_list[i];
            }
        }

        return null;
    }
}
