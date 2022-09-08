using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class TxtFileMaker : MonoBehaviour
{
    //[SerializeField]
    string DBAddress = "https://docs.google.com/spreadsheets/d/1f80PNut0CeaJHCDIuZUyTsneKhBrZGmXk9PDZ2_R71Q";
    //[SerializeField]
    string sheetNum = "478020856";
    //[SerializeField]
    List<string> range = new List<string>(); // 테스트로 재판입장 데이터만 불러옴
    //[SerializeField]
    List<string> fileName = new List<string>();

    void Awake() 
    {
        AddRange();
        AddFileName();
    }

    void Start()
    {
        StartCoroutine(TrialDialogNetConnect());
    }

    void AddRange()
    {
        range.Add("C3:E14");    // 재판 입장
        range.Add("C15:E20");   // 선판결
        range.Add("C21:E24");   // 범인 선택
        range.Add("C25:E28");   // 흉기 선택
        range.Add("C29:E32");   // 동기 선택
        range.Add("C33:E34");   // 잘못된 선택 시(질서)
        range.Add("C35:E36");   // 잘못된 선택 시(혼돈)
        range.Add("C37:E38");   // 적확한 선택 시(질서)
        range.Add("C39:E40");   // 적확한 선택 시(혼돈)
        range.Add("C41:E45");   // 공방완료
        range.Add("C46:E48");   // 배심원 선고
        range.Add("C49:E52");   // 결과 승리
        range.Add("C53:E56");   // 결과 패배
        range.Add("C57:E70");   // 결과 승리 시 마무리
    }

    void AddFileName()
    {
        fileName.Add("CourtEntry");    // 재판 입장
        fileName.Add("PreJudgment");   // 선판결
        fileName.Add("CulpritSelection");   // 범인 선택
        fileName.Add("WeaponSelection");   // 흉기 선택
        fileName.Add("MotiveSelection");   // 동기 선택
        fileName.Add("WrongChoiceOrder");   // 잘못된 선택 시(질서)
        fileName.Add("WrongChoiceChaos");   // 잘못된 선택 시(혼돈)
        fileName.Add("CorrectChoiceOrder");   // 적확한 선택 시(질서)
        fileName.Add("CorrectChoiceChaos");   // 적확한 선택 시(혼돈)
        fileName.Add("BattleCompleted");   // 공방완료
        fileName.Add("JurySentence");   // 배심원 선고
        fileName.Add("ResultWin");   // 결과 승리
        fileName.Add("ResultDefeat");   // 결과 패배
        fileName.Add("ResultWinEnd");   // 결과 승리 시 마무리
    }

    // ANCHOR 구글 docs에서 데이터 읽기
    IEnumerator TrialDialogNetConnect() 
    {      
        for(int i = 0; i < range.Count; i++)
        {
            string URL = DBAddress + "/export?format=tsv&gid=" + sheetNum + "&range=" + range[i];
            Debug.Log(URL);

            UnityWebRequest www = UnityWebRequest.Get(URL);
            yield return www.SendWebRequest();

            string data = www.downloadHandler.text;
            Debug.Log(data);

            SaveData(data, i);
        }
        
    }

    // ANCHOR 데이터 파싱(임시)
    /// <summary>
    /// 데이터를 스트링으로 파싱하는 함수
    /// </summary>
    /// <param string="data">
    /// 파싱할 데이터
    /// </param>
    /// <returns>
    ///  파싱된 데이터
    /// </returns>
    string ParsingData(string data)
    {
        string characterName;
        string dialogue;
        string result = "";

        string[] lines = data.Split('\n');
        
        for(int i = 0; i < lines.Length; i++)
        {
            characterName = lines[i].Split('\t')[0];
            Debug.Log(characterName);
            dialogue = lines[i].Split('\t')[1];
            Debug.Log(dialogue);

            result += characterName + "\t" + dialogue +"\n";
        }

        return result;
    }
    
    // ANCHOR 파일로 저장
    /// <summary>
    /// 데이터를 파일에 저장하는 함수
    /// </summary>
    /// <param string="data">
    /// 파일에 저장할 데이터
    /// </param>
    void SaveData(string data, int i)
    {
        string filePath = "Assets/DialogueData/TrialData/";
        StreamWriter sw;

        if (!File.Exists(filePath))
        {
            sw = new StreamWriter(filePath + i + "_" + fileName[i] +".txt");
            sw.WriteLine(data);
            sw.Flush();
            sw.Close();
        }
        else if (File.Exists(filePath))
        {
            File.Delete(filePath);
            SaveData(data, i);
        }
    }
}
