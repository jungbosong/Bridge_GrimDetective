using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCanvas : MonoBehaviour
{
    //List<Tuple<string,string>> dialogueData = new List<Tuple<string,string>>();
    int cnt = -1;        // 현재 진행 중인 대화 번호
    int maxCnt = 0;      // 현재 진행 중인 질문의 마지막 번호
    bool isStarted = false;
    ChatManager chatManager;
    [SerializeField] Image leftImg;
    [SerializeField] Image rightImg;
    [SerializeField] GameObject conversationBtn;
    Color greyColor, whiteColor;
    CombinationGraph combinationGraph;
    public int suspect, weapon, motive;
    string result;
    string questionType; // 현재 진행중인 질문 종류

    void Awake() 
    {
        chatManager = this.gameObject.GetComponent<ChatManager>();   
        combinationGraph = this.gameObject.GetComponent<CombinationGraph>();
        leftImg = leftImg.transform.GetComponent<Image>();
        rightImg = rightImg.transform.GetComponent<Image>();
        ColorUtility.TryParseHtmlString("#484848", out greyColor);
        ColorUtility.TryParseHtmlString("#FFFFFF", out whiteColor);
        //this.gameObject.SetActive(false);
    }

    /*void SetData(string fileName)
    {
        dialogueData.Clear();
        dialogueData = TxtFileReader.Instance.GetData(fileName);
        Debug.Log("대사를 불러온 내용을 출력합니다.");
        foreach(var data in dialogueData)
        {
            Debug.Log(data.Item1 + "\t" + data.Item2);
        }
        maxCnt = dialogueData.Count;
    }*/
    
    void ShowLine(Tuple<string, string> line)
    {
        // 주인공 대사 출력
        if(line.Item1 == "주인공")
        {
            Debug.Log(line.Item2);
            rightImg.color = whiteColor;
            rightImg.color = whiteColor;
            leftImg.color = greyColor;
            chatManager.Chat(true, line.Item2);
        }
        // 주인공 외 캐릭터 대사 출력
        else
        {
            Debug.Log(line.Item2);
            rightImg.color = greyColor;
            leftImg.color = whiteColor;
            chatManager.Chat(false, line.Item2);
    
        }
    }

    /*public void StartPreJudgment()
    {
        SetData("1_PreJudgment");
        cnt = -1;
    }*/

    public void OnClickedDialogueBtn()
    { 
        Debug.Log("공방 버튼이 눌러졌습니다..");
        cnt++;
        Debug.Log("현재 cnt: " + cnt + "\tmaxCnt: " + maxCnt);
        if(cnt < maxCnt-1)
        {
           //ShowLine(dialogueData[cnt]);
        }
        else
        {
            //this.gameObject.SetActive(false);
            conversationBtn.SetActive(false);
            chatManager.DestroyAllBoxes();
        }
    }

    // TODO 질문 대사 출력
    // string tmpPath = "/" + questionNum + "_Question";
    // ShowQuestionData(path + tmpPath + "/Question.txt");
    public void ShowQuestionData(string path)
    {
        Debug.Log("질문 대사 출력");

        cnt = -1;
        conversationBtn.SetActive(true);

        Debug.Log("질문 대사 출력 완료");
        Debug.Log("-----------------------");
    }

    // TODO 선택지 팝업창 출력

    // TODO 선택지 선택에 대한 답변 출력
    // TODO 증거제시 팝업창 출력

    // TODO 정답 증거 제시에 대한 반응 출력

}
