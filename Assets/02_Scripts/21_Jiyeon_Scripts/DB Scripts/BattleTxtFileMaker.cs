using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class BattleTxtFileMaker : MonoBehaviour
{
    //[SerializeField]
    string DBAddress = "https://docs.google.com/spreadsheets/d/1f80PNut0CeaJHCDIuZUyTsneKhBrZGmXk9PDZ2_R71Q";
    //[SerializeField]
    List<string> sheetNum = new List<string>();
    //[SerializeField]
    string range = "C3:E";
    //[SerializeField]
    List<string> fileName = new List<string>();
    string[] folders = {"Battle_1_1_1/", "Battle_1_2_1/", "Battle_2_0_2", "Battle_2_1_2", "Battle_0_3_0"};

    void Awake() 
    {
        AddSheetNum();
    }

    void Start()
    {
        StartCoroutine(BattleDialogNetConnect());
    }

    void AddSheetNum()
    {
        sheetNum.Add("254121563");
        sheetNum.Add("1458266636");
        sheetNum.Add("1773751119");
        sheetNum.Add("2052236787");
        sheetNum.Add("345101118");
    }

    // ANCHOR 구글 docs에서 데이터 읽기
    IEnumerator BattleDialogNetConnect() 
    {      
        for(int i = 0; i < sheetNum.Count; i++)
        {
            string URL = DBAddress + "/export?format=tsv&gid=" + sheetNum[i] + "&range=" + range;
            Debug.Log(URL);

            UnityWebRequest www = UnityWebRequest.Get(URL);
            yield return www.SendWebRequest();

            string data = www.downloadHandler.text;
            Debug.Log(data);

            List<string> questionData = new List<string>();
            List<string> questionType = new List<string>();

            ParsingData(data, questionData, questionType);

            string filePath = "Assets/DialogueData/TrialData/BattleData/" + folders[i];

            // 각 엔딩 종류의 질문 개수 만큼 반복
            for(int j = 0; j < questionType.Count; j++)
            {
                string path = filePath + "/" + (j+1) + "_Question";

                StartCoroutine(MakeQuestionFolder(path));
                Debug.Log("questionNum: " + j);
                MakeQuestionFile(questionData[j], j, path, questionType[j]);
            }
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
    void ParsingData(string data, List<string> questionData, List<string> questionType)
    {
        // 1. QC, QP확인 후 저장
        int count = -1;
       
        for(int i = 0; i < data.Split('\n').Length; i++)
        {
            string line = data.Split('\n')[i];
            string num = line.Split('\t')[1];
            
            if(num == "QC") {
                count++;
                questionData.Add(line + "\n");
                questionType.Add("QC");
            }
            else if (num == "QP") {
                count++;
                questionData.Add(line + "\n");
                questionType.Add("QP");
            } else {
                questionData[count] += line +"\n";
            }
        }  
    }
    
    // ANCHOR 폴더 제작 함수
    /// <summary>
    /// 질문 폴더 만드는 함수
    /// </summary>
    /// <param string="filePath">
    /// 폴더 만들 경로
    /// </param>
    IEnumerator MakeQuestionFolder(string filePath)
    {
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        else if (Directory.Exists(filePath))
        {
            string[] allFiles = Directory.GetFiles(filePath);
            for(int i = 0; i< allFiles.Length; i++)
            {
                File.Delete(allFiles[i]);
            }
            
            yield return null;
            
            Directory.Delete(filePath);
            MakeQuestionFolder(filePath);
        }
    }

    // ANCHOR 공방 대사 데이터 파일로 저장하는 함수
    /// <summary>
    /// QC/QP 질문 관련 대사 파일 만드는 함수
    /// </summary>
    /// <param string="data">
    /// 파일에 넣을 대사 데이터
    /// </param>
    /// <param int ="questionNum">
    /// 질문 번호
    /// </param>
    /// <param string="filePath">
    /// 파일 만들 경로
    /// </param>
     /// <param string="questionType">
    /// 질문 종류
    /// </param>
    void MakeQuestionFile(string data, int questionNum, string filePath, string questionType)
    {
        
        if(questionType == "QC") Debug.Log("SaveQCData"); 
        if(questionType == "QP") Debug.Log("SaveQPData");
        
        // 1. 질문 종류,  정답 번호(임시: 수동) 저장
        SaveData(filePath  + "/QuestionInfo.txt", questionType);

        // 2. 질문 저장
        SaveQuestionData(data, filePath + "/Question.txt", questionType);

        // 3. 답 저장
        SaveAnswerData(data, filePath);
       
    }

     void SaveData(string filePath, string data)
    {
        StreamWriter sw;
        if(!File.Exists(filePath))
        {
            sw = new StreamWriter(filePath);
            sw.WriteLine(data);
            sw.Flush();
            sw.Close();
        }
        else if(File.Exists(filePath))
        {
            File.Delete(filePath);
            SaveData(filePath, data);
        }
    }

    void SaveQuestionData(string data, string filePath, string questionType)
    {
        string result = "";
        string num = "";
       
        for(int i = 0; i < data.Split('\n').Length; i++)
        {
            string line = data.Split('\n')[i];

            if (line.Split('\t').Length > 1)
            {
                num = line.Split('\t')[1];
            
                if(num.Contains(questionType)) {
                    result += line.Split('\t')[0] + "\t" + line.Split('\t')[2] + "\n";
                }
            }
        }  

        SaveData(filePath, result);
    }   

    void SaveAnswerData(string data, string filePath)
    {
        int count = 0;

        string result = "";
        string num = "";

        Debug.Log("data: " + data);
       
        for(int i = 0; i < data.Split('\n').Length; i++)
        {
            string line = data.Split('\n')[i];

            if (line.Split('\t').Length > 1)
            {
                num = line.Split('\t')[1];
            
                if(num.Contains("AC") || num.Contains("AP"))
                {
                    result += line.Split('\t')[0] + "\t" + line.Split('\t')[2] + "\n";
                    if(num.Contains("E"))
                    {
                        SaveData(filePath + "/" + (++count) +"_Answer.txt", result);
                        result = "";
                    }
                }
            }
        }  
    }
}
