using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class TxtFileReader : MonoBehaviour
{
    private static TxtFileReader instance = null; 

    // ANCHOR 싱글톤 제작
    public static TxtFileReader Instance {
        get{
            if(instance == null){
                var obj = FindObjectOfType<TxtFileReader>();
                if(obj != null) {
                    instance = obj;
                } else {
                    var newObj = new GameObject().AddComponent<TxtFileReader>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }

    private void Awake() 
    {
        /*var objs = FindObjectsOfType<TxtFileReader>();
        if(objs.Length != 1) {
            Debug.Log("TxtFileReader 여러개 찾음");
            Debug.Log(gameObject.name);
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Debug.Log(gameObject.name);*/
    }
    
    // ANCHOR GetData
    /// <summary>
    /// fileName에 해당하는 파일에서 데이터를 읽어오는 함수
    /// </summary>
    /// <param string="fileName">
    /// 데이터를 얻을 파일명
    /// </param>
    /// <returns>
    ///  리스트[튜플[캐릭터명,대사]]
    /// </returns>
    public List<Tuple<string,string>> GetData(string fileName)
    {
        string filePath = "Assets/DialogueData/TrialData/";
        string[] data = File.ReadAllText(filePath + fileName + ".txt").Split('\n');

        List<Tuple<string,string>> result = new List<Tuple<string,string>>();

        for(int i = 0; i < data.Length; i++)
        {
            string[] line = data[i].Split('\t');

            if(line.Length > 1)
            {
                string characterName = data[i].Split('\t')[0];
                string dialogue = data[i].Split('\t')[2];

                //Debug.Log("TxtFileReader에서 읽은 이름: " + characterName);
                //Debug.Log("TxtFileReader에서 읽은 대사: " + dialogue);

                result.Add(Tuple.Create(characterName, dialogue));
            }
        }
        
        return result;
    }
}
