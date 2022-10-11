using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BattleManager : MonoBehaviour
{
    [SerializeField]  
    int suspect, weapon, motive;    // 테스트용 범인, 흉기, 동기번호
    int questionCnt;                // 질문 개수
    string questionType;            // 질문 종류
    string correctProof;            // 정답 증거
    string choosedProof;             // 사용자가 선택한 증거
    int choosedNum = 1;                 // 사용자가 선택한 선택지
    string path = "Assets/DialogueData/TrialData/BattleData/Battle";

    // STUB
    void Start() 
    {
        StartBattle();
    }

    // 공방 시작 함수
    public void StartBattle()
    {
        // TODO Trialmng에서 엔딩 종류 받아오기
        
        path += "_" + suspect + "_" + weapon + "_" + motive;
        // Battle_엔딩번호 폴더에서 질문 몇개인지 정보 저장
        questionCnt = GetQuestionCnt(path);
        
        int questionNum = 0;    // 질문 번호
        while(questionNum++ < questionCnt)
        {
            Debug.Log("질문 번호: " + questionNum);

            // questionNum번재 질문의 종류 확인
            string tmpPath = "/" + questionNum + "_Question";
            GetQuestionInfo(path + tmpPath + "/QuestionInfo.txt");
            Debug.Log("질문 종류: " + questionType);

            // TODO 질문 대사 출력
            ShowQuestionData(path + tmpPath + "/Question.txt");
            
            // 선택지 질문일 경우
            if(questionType == "QC\r") 
            {
                Debug.Log("선택지 질문 입니다.");
                // TODO 선택지 팝업창 출력

                // TODO 선택지 선택에 대한 답변 출력
                ShowAnswerData(path + tmpPath + "/" + choosedNum +"_Answer.txt");
            }
            // 증거제시 질문일 경우
            if(questionType == "QP\r")
            {
                Debug.Log("증거 제시 질문 입니다.");
                Debug.Log("정답 증거: " + correctProof);
                // TODO 증거제시 팝업창 출력

                // TODO 정답 증거 제시에 대한 반응 출력
                if(choosedProof == correctProof)
                {
                    ShowAnswerData(path + tmpPath + "/1_Answer.txt");
                }
                // TODO 오답 증거 제시에 대한 반응 출력
                else 
                {
                    ShowAnswerData(path + tmpPath + "/2_Answer.txt");
                }           
            }
        }
    }
    
    // 질문 개수 얻는 함수
    private int GetQuestionCnt(string path)
    {
        int questionCnt = 0;
        DirectoryInfo di = new DirectoryInfo(path);
        foreach(DirectoryInfo Dir in di.GetDirectories())
        {
            questionCnt++;
        }
        return questionCnt;
    }

    // 질문 정보(질문 종류, 정답 증거) 얻는 함수
    private void GetQuestionInfo(string path)
    {
        string[] data = File.ReadAllText(path).Split('\n');

        questionType = data[0];
    
        if(data.Length > 1)
        {
            correctProof = data[1];
        }
    }

    // TODO 질문 대사 출력하는 함수
    private void ShowQuestionData(string path)
    {
        Debug.Log("질문 대사 출력");
        string[] data = File.ReadAllText(path).Split('\n');

        for(int i = 0; i < data.Length; i++)
        {
            Debug.Log(data[i]);
        }

        Debug.Log("질문 대사 출력 완료");
        Debug.Log("-----------------------");
    }
    
    // TODO 반응 출력하는 함수
    private void ShowAnswerData(string path)
    {
        Debug.Log("답변 출력");
        string[] data = File.ReadAllText(path).Split('\n');

        for(int i = 0; i < data.Length; i++)
        {
            Debug.Log(data[i]);
        }
    }
}
