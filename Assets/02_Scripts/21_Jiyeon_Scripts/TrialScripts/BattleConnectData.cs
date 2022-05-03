using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BattleConnectData : MonoBehaviour
{
    [SerializeField] TrialMng trialMng;
    public string battleDBAddress = "https://docs.google.com/spreadsheets/d/1f80PNut0CeaJHCDIuZUyTsneKhBrZGmXk9PDZ2_R71Q";
    public Dictionary<string, string> battleSheetNum = new Dictionary<string, string>();
    /* 엔딩분류별 재판질문 시트번호
    030 - 345101118
    000
    111 - 254121563
    121 - 1458266636
    202 - 1773751119
    211 - 2052236787
    313
    323
    333
    */

    public string battleRange = "D3:E";
    public List<string> questionData = new List<string>();


    void Awake()
    {
        battleSheetNum.Add("030", "345101118");
        battleSheetNum.Add("111", "254121563");
        battleSheetNum.Add("121", "1458266636");
        battleSheetNum.Add("202", "1773751119");
        battleSheetNum.Add("211", "2052236787");
        trialMng.GetComponent<TrialMng>();
    }

    public void OnclickTestButton()
    {
        int weapon = trialMng.weapon % 4;
        int motive = trialMng.motive % 4;
        string combination  = trialMng.suspect.ToString() + weapon.ToString() + motive.ToString();
        
        StartCoroutine(BattleDialogNetConnect(combination));
    }

    // 대화록 출력 관련 함수
    IEnumerator BattleDialogNetConnect(string combination) 
    {      
        string sheetNum = "";
        battleSheetNum.TryGetValue(combination, out sheetNum);
        string URL = battleDBAddress + "/export?format=tsv&gid=" + sheetNum + "&range=" + battleRange;

        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;

        ParsingData(data);
    }

    /*
    // TODO
    1. (완) QC, QP확인 후 저장
    2. (ing) 질문이 QC면 선택지 개수 파악, 개수만큼 선택지 버튼 동적 생성, AC_N으로 대답 나눠서 할당
    3. 질문이 QP면 AP_Y/N내용 나눠서 각각 넣어줘야 함
    */
    
    void ParsingData(string data)
    {
       // 1. QC, QP확인 후 저장
        int count = -1;
       
        for(int i = 0; i < data.Split('\n').Length; i++)
        {
            string line = data.Split('\n')[i];
            string num = line.Split('\t')[0];
            
            if(num == "QC") {
                count++;
                questionData.Add(line + "\n");
            }
            if (num == "QP") {
                count++;
                questionData.Add(line + "\n");
            } else {
                questionData[count] += line +"\n";
            }
        }  

        // 출력해보자
        for(int i = 0; i<questionData.Count; i++) {
            Debug.Log(i);
            Debug.Log(questionData[i]);
        }
    }

}
