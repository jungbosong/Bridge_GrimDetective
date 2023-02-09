using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatCanvas : MonoBehaviour
{
   List<Tuple<string,string>> dialogueData = new List<Tuple<string,string>>();
    int cnt = -1;        // 현재 진행 중인 대화 번호
    int maxCnt = 0;
    bool isStarted = false;
    ChatManager chatManager;
    [SerializeField] Image leftImg;
    [SerializeField] Image rightImg;
    Color greyColor, whiteColor;

    void Awake() 
    {
        chatManager = this.gameObject.GetComponent<ChatManager>();    
        leftImg = leftImg.transform.GetComponent<Image>();
        rightImg = rightImg.transform.GetComponent<Image>();
        ColorUtility.TryParseHtmlString("#484848", out greyColor);
        ColorUtility.TryParseHtmlString("#FFFFFF", out whiteColor);
        //this.gameObject.SetActive(false);
        StartBasicConversation();
    }

    void SetData(string fileName)
    {
        dialogueData.Clear();
        dialogueData = TxtFileReader.Instance.GetData(fileName);
        Debug.Log("기본 대사를 불러온 내용을 출력합니다.");
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

    public void StartBasicConversation()
    {
        SetData("0_CourtEntry");
        cnt = -1;
    }

    public void OnClickedDialogueBtn()
    { 
        Debug.Log("대화 버튼이 눌러졌습니다..");
        cnt++;
        Debug.Log("현재 cnt: " + cnt + "\tmaxCnt: " + maxCnt);
        if(cnt < maxCnt-1)
        {
            ShowLine(dialogueData[cnt]);
        }
        else
        {
            //StartMysteryPresentatioin();
            this.gameObject.SetActive(false);
            chatManager.DestroyAllBoxes();
            Debug.Log("done");
            
        }
    }
}
