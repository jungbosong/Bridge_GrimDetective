using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeChatUIMngr : MonoBehaviour
{
    private static ChatUIManager instance = null;                   // 싱글톤
    public GameObject chatContent, choiceContent, chatPanel;        // 채팅 스크롤뷰, 선택지 스크롤뷰, 채팅패널 오브젝트
    public List<GameObject> choiceBtns = new List<GameObject>();    // 선택지 버튼 오브젝트들 
    public int choiceNum;                                           // 선택지 번호
    public Coroutine runningCorutine = null;                        // 인터넷 연결 코루틴 관리 변수
    
    // 대화출력 관련 UI 관리 변수들
    public GameObject clickButtonObj;                               // 대화 넘기는 버튼
    int clickCount;                                                 // 클릭횟수
    public GameObject yellowAreaPrefab, whiteAreaPrefab;            // 프리팹
    public GameObject newYellowArea, newWhiteArea;                  // 동적생성 된 오브젝트
    public Scrollbar scrollBar;                                     // 대화 스크롤바
    AreaScript LastArea;
    AreaScript Area;
    public InvenDialogueUIManager invenDialogueUIManager;
    
     
    //싱글톤 제작함수
    public static ChatUIManager Instance {
        get{
            if(instance == null){
                var obj = FindObjectOfType<ChatUIManager>();
                if(obj != null) {
                    instance = obj;
                } else {
                    var newObj = new GameObject().AddComponent<ChatUIManager>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }

    private void Awake() 
    {
        var objs = FindObjectsOfType<ChatUIManager>();
        if(objs.Length != 1) {
            Debug.Log("DialogueNetConnectManager 여러개 찾음");
            Debug.Log(gameObject.name);
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Debug.Log(gameObject.name);
    }
    
    // 챗패널 모습 초기화시키는 함수
    public void InitChatPanel()
    {
      // ChatContent에 있는 대화 출력된 내용 다 지우고
       Transform[] childList = chatContent.GetComponentsInChildren<Transform>();
       if(childList != null) {
           for(int i = 1; i <childList.Length; i++) {
               if(childList[i] != transform)
                  Destroy(childList[i].gameObject);
           }
       }
       // 클릭한 횟수 초기화
       clickCount = 0;
       // 선택지 버튼 보여주기
       choiceContent.SetActive(true);
       clickButtonObj.SetActive(false);
    }

    // 뒤로가기 버튼 눌렀을 때 실행되는 함수
    public void OnClickedBackButton()
    {
        // 대화 진행 중 뒤로가기 버튼을 눌렀을 때 선택지 화면으로 가기
	    if(!choiceContent.activeSelf && chatContent.activeSelf){
		    DialogueNetConnectManager.Instance.StopDialogNetConnect();
		    InitChatPanel();
	    } // 선택지 선택 중 뒤로가기 버튼을 눌렀을 때 조사화면으로 가기
	    else if(choiceContent.activeSelf) {
		    chatContent.SetActive(true);
		    chatPanel.SetActive(false);
	    } 
    }
    
    // 조사씬에서 인물을 눌렀을 때 실행되는 함수(인물마다 인스펙터창에서  따로 추가해줘야 함)
    public void StartChoiceDialogue()
    {
        // 선택버튼 활성화 및 이름 변경
        SetChoiceBtns();
    }

    // 선택지 버튼 이름 변경 및 활성화하는 함수
    public void SetChoiceBtns()
    {
        // 기존의 하이어라키에 존재하는 선택버튼 모두 비활성화
        for(int i = 0; i<choiceBtns.Count; i++) {
            choiceBtns[i].SetActive(false);
        }

        // 클릭한 인물 ID번호, 해당 인물과 가능한 선택지 개수 받아오기
        int characterID = InvestigationManager.Instance.clueInfoData.characterID;
        int choiceCnt = ChatData.Instance.requiredDatas[characterID].Count;

        // 해당 인물과 대화가능한 선택지 버튼만 활성화 및 선택지 내용 변경
        for(int i = 0; i <choiceCnt; i++){
            choiceBtns[i].SetActive(true);
            choiceBtns[i].GetComponentInChildren<Text>().text = ChatData.Instance.requiredDatas[characterID][i].choiceTitle;
        } 
				
        // 클릭하면 대화가 진행되는 버튼 비활성화
		clickButtonObj.SetActive(false);
        ChatCharacterSprite(characterID);
    }

    // 선택지 버튼을 눌렀을 때 실행되는 함수 (선택지 버튼 번호를 인자로 필요로함)
     public void ClickedChoiceBtn(int chooseNum)
    {
        // 실행중인 코루틴이 있으면 멈추고 다시 실행
        if(runningCorutine!=null){
            StopAllCoroutines();
            runningCorutine = null;
            ClickedChoiceBtn(chooseNum);
        } 
        // 실행중인 코루틴이 없으면 대화 진행
        else {
            choiceContent.SetActive(false);
            choiceNum = chooseNum;
            clickButtonObj.SetActive(true);
            DialogueNetConnectManager.Instance.StartDialogNetConnect();
        }
    }

    // 대화화면을 클릭하면 실행되는 함수(대화를 하나씩 출력하는 함수)
    public void ClickedChatScreen() 
    {
        List<DialogueData> dialogueList = DialogueNetConnectManager.Instance.GetDialogueList(); // 대사리스트
        DialogueData chatData = new DialogueData(); // 대사
        chatData = DialogueNetConnectManager.Instance.GetDialogueList()[clickCount]; 
        
        int talkingCharacterID; // 말하는캐릭터 ID
        bool isMainCharacter = true;   // 현재 말하는 캐릭터가 주인공인지 확인하는 변수
        int dialogueLength = dialogueList.Count;    // 대화 길이
            
        // 클릭횟수 증가      
        clickCount++;

        // 현재 말하는 인물이 주인공인지 확인
        if(chatData.talkingCharacterID == 4){
            talkingCharacterID = 4;
            isMainCharacter=true;
        } else {
            talkingCharacterID = chatData.talkingCharacterID;
            isMainCharacter = false;
        }

        // 대화가 끝났는지 확인
        // 대화가 끝나면 대화록 획득했다고 변경     
        
        if(clickCount==dialogueLength) {
            Debug.Log("대화 끝");
            CheckInventoryData.Instance.ChangeClueIsAcquired("3 " + (InvestigationManager.Instance.clueInfoData.characterID*3 + choiceNum));
            invenDialogueUIManager.UpdataeDropDownOption(InvestigationManager.Instance.clueInfoData.characterID, choiceNum);
            InitChatPanel();
        } // 아직 대화가 남았다면 마저 진행
            else {
            SendChat(chatData.characterLine, isMainCharacter);
        }
    }

    // 대화를 한 줄 씩 보내는 함수(대사, 화자가 주인공인지 묻는 변수)
    public void SendChat(string characterLine, bool isMainCharacter)
    {
        ShowChat(isMainCharacter, characterLine); 
        characterLine = "";
    }

      // 대화를 채팅형태로 보여주는 함수
    public void ShowChat(bool isMainCharacter, string text) 
    {    
        //윤가람(True)은 우측 YellowArea, 타인(False)은 좌측 WhiteArea에 텍스트
        if(isMainCharacter) { // YelloArea생성
            newYellowArea = Instantiate(yellowAreaPrefab); 
            newYellowArea.transform.SetParent(chatContent.transform,false);
            Area = newYellowArea.GetComponent<AreaScript>();
            Area.BoxRect.sizeDelta = new Vector2(600, Area.BoxRect.sizeDelta.y);
            Area.TextRect.GetComponent<Text>().text = text;
            Fit(Area.BoxRect);
            }   
        else { // WhiteArea생성
            newWhiteArea = Instantiate(whiteAreaPrefab);
            newWhiteArea.transform.SetParent(chatContent.transform,false);
            Area = newWhiteArea.GetComponent<AreaScript>();
            Area.BoxRect.sizeDelta = new Vector2(600, Area.BoxRect.sizeDelta.y);
            Area.TextRect.GetComponent<Text>().text = text;
            Fit(Area.BoxRect);       
        }
       
        // 채팅의 줄 길이 조정   
        float X = Area.TextRect.sizeDelta.x + 42;
        float Y = Area.TextRect.sizeDelta.y;
        if (Y > 49)
        {
            for (int i = 0; i < 200; i++)
            {
                Area.BoxRect.sizeDelta = new Vector2(X - i * 2, Area.BoxRect.sizeDelta.y);
                Fit(Area.BoxRect);

                if (Y != Area.TextRect.sizeDelta.y) { Area.BoxRect.sizeDelta = new Vector2(X - (i * 2) + 2, Y); break; }
            }
        }
        else Area.BoxRect.sizeDelta = new Vector2(X, Y);

        //Area.User = user;

        Fit(Area.BoxRect);
        Fit(Area.AreaRect);
        Fit(chatContent.GetComponent<RectTransform>());
    }
    void Fit(RectTransform Rect) => LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);
    void ScrollDelay() => scrollBar.value = 0;
    public Image CharacterSprite;
    public Sprite Character0;
    public Sprite Character1;
    public Sprite Character2;
    public Sprite Character3;
    
    public void ChatCharacterSprite(int characterID){
        if(characterID==0) CharacterSprite.GetComponent<Image>().sprite = Character0;
        if(characterID==1) CharacterSprite.GetComponent<Image>().sprite = Character1;
        if(characterID==2) CharacterSprite.GetComponent<Image>().sprite = Character2;
        if(characterID==3) CharacterSprite.GetComponent<Image>().sprite = Character3;
        Debug.Log("이미지 변경");
    }
}
