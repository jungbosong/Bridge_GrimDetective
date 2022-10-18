using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class ChatStartSetData : MonoBehaviour
{
    ChoiceButtonData ChoiceButtonData;
    ChoiceButtonManager ChoiceButtonManager;
    Characterid Characterid;
    public GameObject ChoiceButton1;
    public GameObject ChoiceButton2;
    public GameObject ChoiceButton3;
    public GameObject ChoiceButton4;

    public RectTransform ChoiceButtonText1;
    public RectTransform ChoiceButtonText2;
    public RectTransform ChoiceButtonText3;
    public RectTransform ChoiceButtonText4;
    public Sprite Clicked;
    public List<Tuple<int,string,string,string>> ChatTupleList = new List<Tuple<int,string,string,string>>();
    public List <string> ChoiceNameList = new List<string>();
    void Start() {
        ChatDataListSet();
    }
    void Awake()
    {
        ChoiceButtonData = this.GetComponent<ChoiceButtonData>();
        ChoiceButtonManager = this.GetComponent<ChoiceButtonManager>();
    }
    public void ChatDataListSet(){
        List<Tuple<int,string,string,string>> ChatTupleListList = new List<Tuple<int,string,string,string>>();
        ChatTupleListList.Add(new Tuple<int,string,string,string>(0, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "870001219", "B2:C18"));
        ChatTupleListList.Add(new Tuple<int,string,string,string>(1, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "431152849", "B2:C17"));
        ChatTupleListList.Add(new Tuple<int,string,string,string>(2, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "119535550", "B2:C24"));
        ChatTupleListList.Add(new Tuple<int, string, string,string>(3, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "1132799246", "B2:C18"));
        ChatTupleListList.Add(new Tuple<int, string, string,string>(4, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "345242520", "B2:C16"));
        ChatTupleListList.Add(new Tuple<int, string, string,string>(5, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "940357806", "B2:C22"));        
        ChatTupleListList.Add(new Tuple<int, string, string,string>(6, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "787803976", "B2:C12"));
        ChatTupleListList.Add(new Tuple<int, string, string,string>(7, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "1708266863", "B2:C16"));
        ChatTupleListList.Add(new Tuple<int, string, string,string>(8, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "468436259", "B2:C16"));
        ChatTupleListList.Add(new Tuple<int, string, string,string>(9, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "1929215288", "B2:C11"));
        ChatTupleListList.Add(new Tuple<int, string, string,string>(10, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "1278423613", "B2:C19"));
        ChatTupleListList.Add(new Tuple<int, string, string,string>(11, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "692650022", "B2:C20"));
        ChatTupleListList.Add(new Tuple<int, string, string,string>(12, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "384804189", "B2:C8"));
        ChatTupleList=ChatTupleListList;

        ChoiceNameList.Add("남편");
        ChoiceNameList.Add("근거없는 의심");
        ChoiceNameList.Add("재떨이");
        ChoiceNameList.Add("딸");
        ChoiceNameList.Add("인간말종");
        ChoiceNameList.Add("나미의 꿈");
        ChoiceNameList.Add("조카");
        ChoiceNameList.Add("말종의 큰 그림");
        ChoiceNameList.Add("나 바쁘다");
        ChoiceNameList.Add("...애인");
        ChoiceNameList.Add("아몬드 쿠키");
        ChoiceNameList.Add("깔끔");
        ChoiceNameList.Add("스파이");
        
        Debug.Log("리스트 촤라랍 성공");

        int character = Characterid.CharacterId;
        List<bool> ChoiceOpen = new List<bool>();
        ChoiceOpen.Add(false);
        ChoiceOpen.Add(false);
        ChoiceOpen.Add(false);
        ChoiceOpen.Add(false);

        //2차대화
        if(CheckInventoryData.Instance.GetClueISAcquired(3,0)==true&CheckInventoryData.Instance.GetClueISAcquired(3,3)==true&CheckInventoryData.Instance.GetClueISAcquired(3,6)==true&CheckInventoryData.Instance.GetClueISAcquired(3,9)==true) ChoiceOpen[0]=true;     
        if(ChoiceOpen[0]==false) Destroy(ChoiceButton2);
        //단서대화

        //누른 후 이미지
        if(CheckInventoryData.Instance.GetClueISAcquired(3,3*character)==true) ChoiceButton1.GetComponent<Image>().sprite = Clicked;
        if(CheckInventoryData.Instance.GetClueISAcquired(3,3*character+1)==true) ChoiceButton2.GetComponent<Image>().sprite = Clicked;
        if(CheckInventoryData.Instance.GetClueISAcquired(3,3*character+2)==true) ChoiceButton3.GetComponent<Image>().sprite = Clicked;

        ChoiceButtonText1.GetComponent<Text>().text = ChoiceNameList[3*character];
        Debug.Log("선택지1: "+ChoiceNameList[3*character]);
        ChoiceButtonText2.GetComponent<Text>().text = ChoiceNameList[3*character+1];
        Debug.Log("선택지2: "+ChoiceNameList[3*character+1]);
        ChoiceButtonText3.GetComponent<Text>().text = ChoiceNameList[3*character+2];
        Debug.Log("선택지3: "+ChoiceNameList[3*character+2]);
        
        if(character==3){
            ChoiceButtonText4.GetComponent<Text>().text = ChoiceNameList[3*character+3];
            if(CheckInventoryData.Instance.GetClueISAcquired(3,3*character+3)==true) ChoiceButton4.GetComponent<Image>().sprite = Clicked;
            Debug.Log("선택지4: "+ChoiceNameList[3*character+3]);
        }
        else{
            Destroy(ChoiceButton4);
        }
    }
}