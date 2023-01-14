using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GoodEndingCanvas : MonoBehaviour
{
    List<Tuple<string,string>> dialogueData = new List<Tuple<string,string>>();
    int cnt = -1;        // 현재 진행 중인 대화 번호
    int maxCnt = 0;
    bool isStarted = false;
    ChatManager chatManager;
    [SerializeField] Image leftImg;
    [SerializeField] Image rightImg;
    Color greyColor, whiteColor;
    string path = "Assets/DialogueData/TrialData/";
    string now = "BattleCompleted";

    void Awake() 
    {
        chatManager = this.gameObject.GetComponent<ChatManager>();  
        leftImg = leftImg.transform.GetComponent<Image>();
        rightImg = rightImg.transform.GetComponent<Image>();
        ColorUtility.TryParseHtmlString("#484848", out greyColor);
        ColorUtility.TryParseHtmlString("#FFFFFF", out whiteColor);
        this.gameObject.SetActive(false);
    }

    void SetData(string fileName)
    {
        dialogueData.Clear();
        dialogueData = TxtFileReader.Instance.GetData(fileName);
        Debug.Log("굿 엔딩 대사를 불러온 내용을 출력합니다.");
        foreach(var data in dialogueData)
        {
            Debug.Log(data.Item1 + "\t" + data.Item2);
        }
        maxCnt = dialogueData.Count;
    }
    
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

    public void StartGoodEnding()
    {
        SetData("9_BattleCompleted");
        cnt = -1;
    }

    public void OnClickedDialogueBtn()
    { 
        Debug.Log("굿 엔딩 버튼이 눌러졌습니다..");
        cnt++;
        Debug.Log("현재 cnt: " + cnt + "\tmaxCnt: " + maxCnt);
        if(cnt < maxCnt-1)
        {
            ShowLine(dialogueData[cnt]);
        }
        else
        {
            chatManager.DestroyAllBoxes();
            switch(now)
            {
                case "BattleCompleted": 
                {
                    now = "JurySentece";
                    SetData("10_JurySentence");
                    cnt = -1;
                    //chatManager.DestroyAllBoxes();
                    break;
                }
                case "JurySentece": 
                {
                    now = "ResultWin";
                    SetData("11_ResultWin");
                    cnt = -1;
                    //chatManager.DestroyAllBoxes();
                    break;
                }
                case "ResultWin": 
                {
                    now = "ResultWinEnd";
                    SetData("13_ResultWinEnd");
                    cnt = -1;
                    //chatManager.DestroyAllBoxes();
                    break;
                }
                case "ResultWinEnd":
                {
                    this.gameObject.SetActive(false);
                    //chatManager.DestroyAllBoxes();
                    break;
                }
            }
        }
    }
}
