using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ChoiceButtonData : MonoBehaviour
{
    ChatStartSetData ChatStartSetData;
    CheckInventoryData CheckInventoryData;
    ChoiceButtonManager ChoiceButtonManager;
    public GameObject ChoiceButton;
    public int ChoiceNumberIndex;
     void Awake() {
        ChatStartSetData = this.GetComponent<ChatStartSetData>();
        ChoiceButtonManager = this.GetComponent<ChoiceButtonManager>();
        CheckInventoryData = this.GetComponent<CheckInventoryData>();
    }

    void Start() {

    }
    public void ClickedChoice()
    {
        List<string> ChoiceList = new List<string>();
        ChoiceList.Add("남편");
        ChoiceList.Add("근거없는 의심");
        ChoiceList.Add("재떨이");
        ChoiceList.Add("딸");
        ChoiceList.Add("인간말종");
        ChoiceList.Add("나미의 꿈");
        ChoiceList.Add("조카");
        ChoiceList.Add("말종의 큰 그림");
        ChoiceList.Add("나 바쁘다");
        ChoiceList.Add("...애인");
        ChoiceList.Add("아몬드 쿠키");
        ChoiceList.Add("깔끔");
        ChoiceList.Add("스파이");

        string ButtonText = ChoiceButton.GetComponentInChildren<Text>().text;
        int N=0;
        foreach (var iter in ChoiceList)
        {
            if(iter == ButtonText)
            {
                ChoiceNumberIndex=N;
            }
            N++;
        }

        Debug.Log(ChoiceNumberIndex+"번째 선택지 클릭");
        StartCoroutine(ChoiceButtonManager.DialogNetConnect());
    }
}