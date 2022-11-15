using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ChoiceButtonManager : MonoBehaviour
{
    ChatManager chatmanager;
    ChoiceButtonData ChoiceButtonData;
    CheckInventoryData CheckInventoryData;
    ChatStartSetData ChatStartSetData;

    static List<Dictionary<int, string>> dialogList = new List<Dictionary<int, string>>();
    void Awake() {
        ChoiceButtonData = this.GetComponent<ChoiceButtonData>();
        ChatStartSetData = this.GetComponent<ChatStartSetData>();
        CheckInventoryData = this.GetComponent<CheckInventoryData>();
      
    }
    public IEnumerator DialogNetConnect()
    {
        List<Tuple<int,string,string,string>> TupleList = new List<Tuple<int,string,string,string>>();
        TupleList.Add(new Tuple<int,string,string,string>(0, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "870001219", "B2:C18"));
        TupleList.Add(new Tuple<int,string,string,string>(1, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "431152849", "B2:C17"));
        TupleList.Add(new Tuple<int,string,string,string>(2, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "119535550", "B2:C24"));
        TupleList.Add(new Tuple<int, string, string,string>(3, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "1132799246", "B2:C18"));
        TupleList.Add(new Tuple<int, string, string,string>(4, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "345242520", "B2:C16"));
        TupleList.Add(new Tuple<int, string, string,string>(5, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "940357806", "B2:C22"));        
        TupleList.Add(new Tuple<int, string, string,string>(6, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "787803976", "B2:C12"));
        TupleList.Add(new Tuple<int, string, string,string>(7, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "1708266863", "B2:C16"));
        TupleList.Add(new Tuple<int, string, string,string>(8, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "468436259", "B2:C16"));
        TupleList.Add(new Tuple<int, string, string,string>(9, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "1929215288", "B2:C11"));
        TupleList.Add(new Tuple<int, string, string,string>(10, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "1278423613", "B2:C19"));
        TupleList.Add(new Tuple<int, string, string,string>(11, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "692650022", "B2:C20"));
        TupleList.Add(new Tuple<int, string, string,string>(12, "https://docs.google.com/spreadsheets/d/1USMqRrMSynRCGEwqugoCnpavtdFoiYva", "384804189", "B2:C8"));
        
        int ChoiceIndex = ChoiceButtonData.ChoiceNumberIndex;
        string DialogTableName = TupleList[ChoiceIndex].Item2;
        string SheetRange = TupleList[ChoiceIndex].Item3;
        string DialogRange = TupleList[ChoiceIndex].Item4;
    
        string URL = DialogTableName + "/export?format=tsv" + "&gid=" + SheetRange + "&range=" + DialogRange;
        UnityWebRequest www = UnityWebRequest.Get(URL); 
        yield return www.SendWebRequest(); 

        string data = www.downloadHandler.text; 
        DialogParsing(data);    
    }

    public void DialogParsing(string data)
    {
        string[] split_text = data.Split('\n');
       
        string characterName;
        string dialogTxt;
        Dictionary<int, string> tmpData = new Dictionary<int, string>();
        dialogList.Clear();

        for (int i = 0; i < split_text.Length; i++)
        {
            string tmp = split_text[i];

            characterName = tmp.Split('\t')[0];
            dialogTxt = tmp.Split('\t')[1];
            MoveToDialog(characterName, dialogTxt);
        }
    
    }   
    public Dictionary<int, string> MoveToDialog(string characterName, string dialogTxt)
    {
        Dictionary<int, string> tmpData = new Dictionary<int, string>();
        int characterID = 0;

        switch (characterName)
        {
            case "윤가람": characterID = 4; break;
            case "박석지": characterID = 0; break;
            case "강건오": characterID = 1; break;
            case "구나미": characterID = 2; break;
            case "구말종": characterID = 3; break;
        }

        tmpData.Add(characterID, dialogTxt);
        dialogList.Add(tmpData);

        return tmpData;
    }

    public IEnumerator PrintDialog(int dialogID, int characterId)
    {
        yield return new WaitForSeconds(1.0f);
        Dictionary<int, string> tmpDic = new Dictionary<int, string>();
        tmpDic = dialogList[dialogID];
        if (tmpDic.TryGetValue(characterId, out string discription))
            Debug.Log(discription);

    }

    bool IsBool;
    public int click = 0;
    Dictionary<int, string> ChatDict = new Dictionary<int, string>();
    
    public void Click() {//ChatPanel에 Button을 씌운 다음 온클릭함수에 넣음.
        Debug.Log(click+"/"+(dialogList.Count-1));
        ChatDict=dialogList[click];
        click++;

        List<int> ChatKey = new List<int>(ChatDict.Keys);
        Debug.Log(ChatDict[ChatKey[0]]);

        //bool IsBool;
        if (ChatKey[0]==0) IsBool=true;
        else IsBool=false;

        ChatSend(ChatDict[ChatKey[0]], IsBool);

        if(click==dialogList.Count) {
            Debug.Log("대화 끝");
            string temp ="3 "+ChoiceButtonData.ChoiceNumberIndex;
            CheckInventoryData.Instance.ChangeClueIsAcquired(temp);
        }
    }

   
     public void ChatSend(string chattext, bool isBool)
     {
        chatmanager = GameObject.Find("ChatContent").GetComponent<ChatManager>();
        // Debug.Log("ChatSend함수 시작");
         if (IsBool == true && chattext.Trim() != "")
         {
             chatmanager.Chat(true, chattext); 
             chattext = "";
         }

         if (IsBool == false && chattext.Trim() != "")
         {
             chatmanager.Chat(false, chattext); 
             chattext = "";
         }
     }
}