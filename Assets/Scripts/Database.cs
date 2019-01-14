using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Database : MonoBehaviour
{
    //아이템 클래스 리스트
    public static List<Item> item = new List<Item>();

    //파싱 완료된 아이템 정보 리스트
    //(파싱된 결과값)
    private Dictionary<string, List<string>> itemInfo;
    //<속성,  순서>쌍
    private Dictionary<string, int> itemIndex;
    private int itemCount;
    private const string ID = "id";
    private const string NAME = "name";
    private const string TYPE = "type";
    private const string DETAIL = "detail";
    private const string MAX = "max amount";

    void Awake()
    {
        itemInfo = new Dictionary<string, List<string>>();
        itemIndex = new Dictionary<string, int>();
        itemCount = 0;
        ParsingItem();
        AddNewItem();
    }

    private void ParsingItem()
    {
        //아이템 리스트 파일 불러오기
        TextAsset txt = Resources.Load("ItemList") as TextAsset;
        if (txt == null)
        {
            Debug.Log("데이터베이스에서 아이템 목록을 불러오는 데 실패했습니다.");
            return;
        }

        StringReader sr = new StringReader(txt.text);        //문자열을 읽기 위한 객체sr
        string line = " ";          //한 줄씩 읽은 문자열
        string[] attr;               //파싱된 속성값들
        string[] tuple;             //파싱된 value들
        bool startReadTuple = false;
        int i;

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
                        itemInfo.Add(attr[i].ToLower(), new List<string>());
                        itemIndex.Add(attr[i].ToLower(), i);
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
                    if (i == itemIndex[ID]) { itemInfo[ID].Add(tuple[i]); }
                    if (i == itemIndex[NAME]) { itemInfo[NAME].Add(tuple[i]); }
                    if (i == itemIndex[TYPE]) { itemInfo[TYPE].Add(tuple[i]); }
                    if (i == itemIndex[DETAIL]) { itemInfo[DETAIL].Add(tuple[i]); }
                    if (i == itemIndex[MAX]) { itemInfo[MAX].Add(tuple[i]); }
                }
                //아이템 1개 정보 추가완료
                itemCount++;
            }
        }

    }

    private void AddNewItem()
    {
        for (int i = 0; i < itemCount; i++)
        {
        item.Add(
                new Item(itemInfo[NAME][i],
                Convert.ToInt32(itemInfo[ID][i]),
                itemInfo[DETAIL][i],
                Convert.ToInt32(itemInfo[TYPE][i]),
                Convert.ToInt32(itemInfo[MAX][i])
                )
            );
        }
    }
}
