using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

// 대사출력에 필요한 데이터
public class DialogueData
{
    public int talkingCharacterID;
    public string characterLine;
}

// DB연결용 클레스
public class DialogueNetConnectManager : MonoBehaviour
{
    private static DialogueNetConnectManager instance = null;
    // 대화록
    static List<DialogueData> dialogList = new List<DialogueData>();
    
    
    //싱글톤 제작함수
    public static DialogueNetConnectManager Instance {
        get{
            if(instance == null){
                var obj = FindObjectOfType<DialogueNetConnectManager>();
                if(obj != null) {
                    instance = obj;
                } else {
                    var newObj = new GameObject().AddComponent<DialogueNetConnectManager>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }

    private void Awake() 
    {
        var objs = FindObjectsOfType<DialogueNetConnectManager>();
        if(objs.Length != 1) {
            Debug.Log("DialogueNetConnectManager 여러개 찾음");
            Debug.Log(gameObject.name);
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Debug.Log(gameObject.name);
    }

    // 코루틴 연결 시작하는 함수
    public void StartDialogNetConnect()
    {
        ChatUIManager.Instance.runningCorutine = StartCoroutine("DialogNetConnect");
    }
    // 코루틴 연결 종료하는 함수
    public void StopDialogNetConnect()
    {
        StopAllCoroutines();
    }

    // DB에서 대화록 정보 들고오는 함수
    public IEnumerator DialogNetConnect()
    {
        int characterID = InvestigationManager.Instance.clueInfoData.characterID;
        int choiceNum = ChatUIManager.Instance.choiceNum;

        string dialogueDBLink = ChatData.Instance.dialogueDBLink;
        string sheetNum = ChatData.Instance.requiredDatas[characterID][choiceNum].sheetNum;
        string range = ChatData.Instance.requiredDatas[characterID][choiceNum].range;

        string URL = dialogueDBLink + "/export?format=tsv" + "&gid=" + sheetNum + "&range=" + range;
        Debug.Log(characterID);
        Debug.Log(choiceNum);
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();
        
        string data = www.downloadHandler.text; 
        DialogParsing(data);           
    }


    // DB에서 받은 데이터 파싱하는 함수
    public void DialogParsing(string data)
    {
        string[] split_text = data.Split('\n');
       
        string characterName;   // 인물 이름
        string dialogTxt;       // 대사
        
        dialogList.Clear();     // 대사리스트 초기화

        for (int i = 0; i < split_text.Length; i++)
        {
            string tmp = split_text[i];

            characterName = tmp.Split('\t')[0];
            dialogTxt = tmp.Split('\t')[1];
            AddDialogueList(characterName, dialogTxt);
        }
    }   

    //  파싱한 데이터를 가공하여 리스트에 추가하는 함수
    public void AddDialogueList(string characterName, string dialogTxt)
    {
        int characterID = 0;

        switch (characterName)
        {            
            case "박석지": characterID = 0; break;
            case "구나미": characterID = 1; break;
            case "구말종": characterID = 2; break;
            case "강건오": characterID = 3; break;
            case "윤가람": characterID = 4; break;
        }

        dialogList.Add(new DialogueData(){talkingCharacterID = characterID, characterLine = dialogTxt});
    }

    // 대화록 정보 얻을 때 사용하는 함수
    public List<DialogueData> GetDialogueList()
    {
        return dialogList;
    }

}
