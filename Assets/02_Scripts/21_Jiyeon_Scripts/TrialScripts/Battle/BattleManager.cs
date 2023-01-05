using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using QuestionData;

public class BattleManager : MonoBehaviour
{ 
    public int suspect, weapon, motive;    // 테스트용 범인, 흉기, 동기번호
    int questionCnt;                // 질문 개수
    string questionType;            // 질문 종류
    int choiceCnt;                   // 선택지 개수
    string correctProof;            // 정답 증거
    string path = "Assets/DialogueData/TrialData/BattleData/Battle";
    List<QC> choiceQuestions = new List<QC>();
    List<QP> proofQuestions = new List<QP>();
    int QCNum = 0, QPNum = 0;

    void Awake() 
    {
        SetQeustionData();
    }

    void SetClueNum()
    {
        /*suspect = mysteryPresentationMng.suspectNum;
        weapon = mysteryPresentationMng.weaponNum;
        motive = mysteryPresentationMng.motiveNum;*/
    }

    // 질문 정보 저장
    void SetQeustionData() 
    {
        SetClueNum();

        path += "_" + suspect + "_" + weapon + "_" + motive;
        questionCnt = GetQuestionCnt(path);

        for(int questionNum = 1; questionNum <= questionCnt; questionNum++)
        {
            // questionNum번재 질문의 종류 확인
            string tmpPath = "/" + questionNum + "_Question";
            GetQuestionInfo(path + tmpPath + "/QuestionInfo.txt");
            
            Debug.Log("선택지 개수" + choiceCnt);
            Debug.Log("정답번호" + correctProof);
            
            // 선택지 질문일 경우
            if(questionType == "QC\r") 
            {
                //Debug.Log("선택지 질문 입니다.");
                choiceQuestions.Add(new QC(path + tmpPath, choiceCnt));
                //CheckQCData();
            }
            // 증거제시 질문일 경우
            if(questionType == "QP\r")
            {
                //Debug.Log("증거 제시 질문 입니다.");
                proofQuestions.Add(new QP(path + tmpPath, correctProof));
                //CheckQPData();
            }
        }

        for(int i = 0; i <choiceQuestions.Count; i++)
        {
            CheckQCData();
        }
        for(int i = 0; i <proofQuestions.Count; i++)
        {
            CheckQPData();
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
    
        if(questionType == "QC\r")
        {
            choiceCnt = int.Parse(data[1]);
            
        }
        else    // QP
        {
            correctProof = data[1];
        }
        
    }

    // 질문 진행
    // 저장된 질문 데이터 확인
    void CheckQCData()
    {
        foreach(QC qc in choiceQuestions)
        {
            Debug.Log("질문");
            Debug.Log(qc.question.character + ": " + qc.question.dialogue);
            for(int i = 1; i <= qc.choices.Count; i++)
            {
                Debug.Log("선택지" + i + ": " + qc.choices[i-1]);    
            }
            for(int i = 1; i <= qc.actions.Count; i++)
            {
                Debug.Log("반응" + i);
                for(int j =0; j < qc.actions[i-1].Count; j++)
                {
                    Debug.Log(qc.actions[i-1][j].character + ": " + qc.actions[i-1][j].dialogue);
                }
            }
        }
    }

    void CheckQPData()
    {
        foreach(QP qp in proofQuestions)
        {
            Debug.Log("정답번호: " + qp.correctTypeNum + "_" + qp.corrrectNum);
            Debug.Log("질문");
            for(int i = 0; i < qp.question.Count; i++)
            {
                Debug.Log(qp.question[i].character + ": " + qp.question[i].dialogue);
            }
            Debug.Log("정답반응");
            for(int i = 0; i < qp.correctAction.Count; i++)
            {
                Debug.Log(qp.correctAction[i].character + ": " + qp.correctAction[i].dialogue);
            }
            Debug.Log("오답반응");
            for(int i = 0; i < qp.nCorrectAction.Count; i++)
            {
                Debug.Log(qp.nCorrectAction[i].character + ": " + qp.nCorrectAction[i].dialogue);
            }
        }
    }
}
