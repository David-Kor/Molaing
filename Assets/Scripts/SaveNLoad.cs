using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveNLoad : MonoBehaviour
{
    [System.Serializable]
    public class SaveData
    {
        //저장된 씬의 이름
        public string scene;

        //플레이어의 위치
        public float pX;
        public float pY;
        public float pZ;

        public int pLevel;
        public int pMaxHP;
        public int pCurHP;
    }

    void Awake()
    {
        StartCoroutine("Aasdf");
    }

    public void SaveGame()
    {
    }

    private IEnumerator Aasdf()
    {
        while (true)
        {
            if (Database.IsInitialized())
            {
                StopCoroutine("Aasdf");
            }
            yield return null;
        }
    }
}
