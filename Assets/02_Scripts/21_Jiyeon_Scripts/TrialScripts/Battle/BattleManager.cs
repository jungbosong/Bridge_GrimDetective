using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using QuestionData;
using TMPro;

public class BattleManager : MonoBehaviour
{ 
    public int suspect, weapon, motive;    // 테스트용 범인, 흉기, 동기번호
    int questionCnt;                // 질문 개수
    List<string> questionType = new List<string>();            // 질문 종류
    int choiceCnt;                   // 선택지 개수
    string correctProof ="";            // 정답 증거 "종류_번호"
    string path = "Assets/DialogueData/TrialData/BattleData/Battle";
    List<QC> choiceQuestions = new List<QC>();
    List<QP> proofQuestions = new List<QP>();
    QP nowQP;
    int QCNum = 0, QPNum = 0;   // 현재 진행중인 QC, QP 번호
    int questionNum = -1;    // 현재 진행중인 질문 번호
    int correctCnt = 0;     // 정답 맞춘 공방 개수

    int cnt = -1;        // 현재 진행 중인 대화 번호
    ChatManager chatManager;
    [SerializeField] MysteryPresentationMng mysteryPresentationMng;
    [SerializeField] Image leftImg;
    [SerializeField] Image rightImg;
    [SerializeField] GameObject conversationBtn;
    [SerializeField] GameObject choiceBtnArea;
    [SerializeField] GameObject investigationLogArea;
    [SerializeField] GameObject actionBtn;
    [SerializeField] GameObject goodEndingCanvas;
    [SerializeField] GameObject badEndingCanvas;
    [SerializeField] TextMeshProUGUI suspectTabTxt;
    [SerializeField] TextMeshProUGUI toolTabTxt;
    [SerializeField] TextMeshProUGUI motiveTabTxt;
    Color greyColor, whiteColor;
    //CombinationGraph combinationGraph;
    TabContentArea tabContentArea;
    TabButtonArea tabButtonArea;

    void Awake() 
    {
        chatManager = this.gameObject.GetComponent<ChatManager>();   
        tabContentArea = investigationLogArea.transform.GetChild(1).GetComponent<TabContentArea>();
        tabButtonArea = investigationLogArea.transform.GetChild(0).GetComponent<TabButtonArea>();
        suspectTabTxt = suspectTabTxt.GetComponent<TextMeshProUGUI>();
        toolTabTxt = toolTabTxt.GetComponent<TextMeshProUGUI>();
        motiveTabTxt = motiveTabTxt.GetComponent<TextMeshProUGUI>();
        //combinationGraph = this.gameObject.GetComponent<CombinationGraph>();
        mysteryPresentationMng = mysteryPresentationMng.GetComponent<MysteryPresentationMng>();
        leftImg = leftImg.transform.GetComponent<Image>();
        rightImg = rightImg.transform.GetComponent<Image>();
        ColorUtility.TryParseHtmlString("#484848", out greyColor);
        ColorUtility.TryParseHtmlString("#FFFFFF", out whiteColor);
        choiceBtnArea.SetActive(false);
        investigationLogArea.SetActive(false);
        actionBtn.SetActive(false);
        this.gameObject.SetActive(false);
    }

    void SetClueNum()
    {
        suspect = mysteryPresentationMng.suspectNum;
        weapon = mysteryPresentationMng.weaponNum;
        motive = mysteryPresentationMng.motiveNum;
    }

    // 질문 정보 저장
    public void SetQeustionData() 
    {
        SetClueNum();

        path += "_" + suspect + "_" + weapon + "_" + motive;
        questionCnt = GetQuestionCnt(path);

        for(int questionNum = 1; questionNum <= questionCnt; questionNum++)
        {
            // questionNum번째 질문의 종류 확인
            string tmpPath = "/" + questionNum + "_Question";
            GetQuestionInfo(path + tmpPath + "/QuestionInfo.txt");
            
            // 선택지 질문일 경우
            if(questionType[questionNum-1] == "QC\r") 
            {
                //Debug.Log("선택지 질문 입니다.");
                choiceQuestions.Add(new QC(path + tmpPath, choiceCnt));
                
            }
            // 증거제시 질문일 경우
            if(questionType[questionNum-1] == "QP\r")
            {
                //Debug.Log("증거 제시 질문 입니다.");
                proofQuestions.Add(new QP(path + tmpPath, correctProof));
                
            }
        }
    }

    // 질문 개수 얻는 함수
    private int GetQuestionCnt(string path)
    {
        int cnt = 0;
        DirectoryInfo di = new DirectoryInfo(path);
        foreach(DirectoryInfo Dir in di.GetDirectories())
        {
            cnt++;
        }
        return cnt;
    }

      // 질문 정보(질문 종류, 정답 증거) 얻는 함수
    private void GetQuestionInfo(string path)
    {
        string[] data = File.ReadAllText(path).Split('\n');
        questionType.Add(data[0]);
        string type = data[0];
    
        if(type == "QC\r")
        {
            choiceCnt = int.Parse(data[1]);
            
        }
        else    // QP
        {
            correctProof = data[1];
        }
    }

    // 질문 진행
    public void ProcessBattle()
    {
        questionNum++;
        if(questionNum < questionType.Count)
        {
            int remainQuestions = questionCnt - questionNum;
            int expectedCorrectNum = remainQuestions+correctCnt;
            double expectedCorrectRate = (double)expectedCorrectNum/questionCnt * 100;
            Debug.Log("현재 질문 번호: " + questionNum);
            Debug.Log("남은 질문 개수: " + remainQuestions);
            Debug.Log("현재 정답률: " + ((double)correctCnt/questionCnt * 100) + "%");
            Debug.Log("예상 정답률: " + expectedCorrectRate + "%");

            if(expectedCorrectRate < 50.0) 
            {
                Debug.Log("정답률 50%이하. 실패.");
                badEndingCanvas.SetActive(true);
                badEndingCanvas.GetComponent<BadEndingCanvas>().StartBadEnding();
                this.gameObject.SetActive(false);
                return;
            }
            if(questionType[questionNum] == "QC\r")
            {
                Debug.Log("현재 질문 종류: QC");
                Debug.Log("현재 QC 번호: " + QCNum);
                ShowQCData(choiceQuestions[QCNum++]);
                return;
            }
            if(questionType[questionNum] == "QP\r")
            {
                Debug.Log("현재 질문 종류: QP");
                Debug.Log("현재 QP 번호: " + QPNum);
                ShowQPData(proofQuestions[QPNum++]);
                return;
            }
        }
        else
        {
            Debug.Log("모든 질문 완료");
            goodEndingCanvas.SetActive(true);
            goodEndingCanvas.GetComponent<GoodEndingCanvas>().StartGoodEnding();
            this.gameObject.SetActive(false);
        }
    }

    void ShowLine(string name, string dialogue)
    {
        // 주인공 대사 출력
        if(name == "주인공")
        {
            Debug.Log(dialogue);
            //rightImg.color = whiteColor;
            rightImg.color = whiteColor;
            leftImg.color = greyColor;
            chatManager.Chat(true, dialogue);
        }
        // 주인공 외 캐릭터 대사 출력
        else
        {
            Debug.Log(dialogue);
            rightImg.color = greyColor;
            leftImg.color = whiteColor;
            chatManager.Chat(false, dialogue);
    
        }
    }

    void ShowQCData(QC qc)
    {
        cnt = -1;
        conversationBtn.SetActive(true);
        conversationBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        conversationBtn.GetComponent<Button>().onClick.AddListener(() => {OnClickedQCBtn(qc, 2);});
    }

    void OnClickedQCBtn(QC qc, int lastCnt)
    {
        Debug.Log("대화 버튼이 눌러졌습니다..");
        cnt++;
        Debug.Log("현재 cnt: " + cnt + "\tmaxCnt: " + lastCnt);

        if(cnt < lastCnt-1)
        {
            Debug.Log("선택 질문 대사 출력");
            Debug.Log(qc.question.character + ": " + qc.question.dialogue);
            ShowLine(qc.question.character, qc.question.dialogue);
        }
        else
        {
            conversationBtn.SetActive(false);
            //chatManager.DestroyAllBoxes();
            rightImg.color = whiteColor;
            leftImg.color = greyColor;
            ShowChoiceBtns(qc);
        }
    }

    // 선택지 팝업창 출력
    void ShowChoiceBtns(QC qc)
    {
        choiceBtnArea.SetActive(true);
        
        for(int i = 0; i < qc.choices.Count; i++)
        {
            Debug.Log("선택지" + i + ": " + qc.choices[i]);
            Button btn = choiceBtnArea.transform.GetChild(i).GetComponent<Button>();
            btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = qc.choices[i];
            int tmp = i;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => {ShowChoiceAction(qc, tmp);});
        }
    }

    // 선택지 선택에 대한 반응 출력
    void ShowChoiceAction(QC qc, int num)
    {
        choiceBtnArea.SetActive(false);
        cnt = -1;
        actionBtn.SetActive(true);
        actionBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        actionBtn.GetComponent<Button>().onClick.AddListener(() => {OnClickedQCActionBtn(qc, num);});
    }

     public void OnClickedQCActionBtn(QC qc, int num)
    { 
        Debug.Log("액션 버튼이 눌러졌습니다..");
        cnt++;
        int lastCnt = qc.actions[num].Count;
        Debug.Log("현재 cnt: " + cnt + "\tmaxCnt: " + lastCnt);

        if(cnt < lastCnt)
        {
           ShowLine(qc.actions[num][cnt].character, qc.actions[num][cnt].dialogue);
           // 정답인 선택지 골랐을 때 반응 (정답률++)
           if(qc.actions[num][cnt].dialogue.Equals("(제법 긍정적인 반응 같다.)")) 
           {
                correctCnt++;
                Debug.Log("정답을 골랐습니다! 현재 맞춘 정답 개수: " + correctCnt);
           }
        }
        else
        {
            actionBtn.SetActive(false);
            chatManager.DestroyAllBoxes();
            ProcessBattle();
        }
    }

    // 증거제시 질문 출력

    public void ShowQPData(QP qp)
    {
        nowQP = qp;
        cnt = -1;
        conversationBtn.SetActive(true);
        conversationBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        conversationBtn.GetComponent<Button>().onClick.AddListener(() => {OnClickedQPBtn(qp, qp.question.Count);});
    }

    void OnClickedQPBtn(QP qp, int lastCnt)
    {
        Debug.Log("대화 버튼이 눌러졌습니다..");
        cnt++;
        Debug.Log("현재 cnt: " + cnt + "\tmaxCnt: " + lastCnt);

        if(cnt < lastCnt)
        {
            Debug.Log("증거제시 질문 대사 출력");
            Debug.Log(qp.question[cnt].character + ": " + qp.question[cnt].dialogue);
            ShowLine(qp.question[cnt].character, qp.question[cnt].dialogue);
        }
        else
        {
            Debug.Log("증거제시 질문 대사 출력 완료");
            conversationBtn.SetActive(false);
            rightImg.color = whiteColor;
            leftImg.color = greyColor;
            ShowInventoryLogs(qp);
        }
    }

    void ShowInventoryLogs(QP qp)
    {
        Debug.Log("증거제시 창 띄우기");
        tabButtonArea.SetBattleTab();
        OnClickedSuspectTab();
        investigationLogArea.SetActive(true);
    }

    // 증거 제시에 대한 반응 출력
    public void ShowProofAction()
    {
        investigationLogArea.SetActive(false);
        cnt = -1;
        actionBtn.SetActive(true);
        actionBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        actionBtn.GetComponent<Button>().onClick.AddListener(() => {OnClickedQPActionBtn(nowQP);});
    }

    void OnClickedQPActionBtn(QP qp) 
    {
        string proof = tabContentArea.proof;
        if(correctProof == proof)
        {
            Debug.Log("정답반응");
            cnt++;
            int lastCnt = qp.correctAction.Count;
            Debug.Log("현재 cnt: " + cnt + "\tmaxCnt: " + lastCnt);

            if(cnt < lastCnt)
            {
                ShowLine(qp.correctAction[cnt].character, qp.correctAction[cnt].dialogue);
            }
            else
            {
                correctCnt++;
                Debug.Log("정답을 골랐습니다! 현재 맞춘 정답 개수: " + correctCnt);
                actionBtn.SetActive(false);
                chatManager.DestroyAllBoxes();
                ProcessBattle();
            }
        }
        else
        {
            Debug.Log("오답반응");
            cnt++;
            int lastCnt = qp.nCorrectAction.Count;
            Debug.Log("현재 cnt: " + cnt + "\tmaxCnt: " + lastCnt);

            if(cnt < lastCnt)
            {
                ShowLine(qp.nCorrectAction[cnt].character, qp.nCorrectAction[cnt].dialogue);
            }
            else
            {
                actionBtn.SetActive(false);
                chatManager.DestroyAllBoxes();
                ProcessBattle();
            }
        }
    }

    public void OnClickedSuspectTab()
    {
        tabContentArea.SetTabTitleTxt("SUSPECT");
        tabContentArea.SetBattleBtn("SUSPECT");
        suspectTabTxt.color = whiteColor;
        toolTabTxt.color = greyColor;
        motiveTabTxt.color = greyColor;
    }

    public void OnClickedToolTap()
    {
        tabContentArea.SetTabTitleTxt("TOOL");
        tabContentArea.SetBattleBtn("TOOL");
        suspectTabTxt.color = greyColor;
        toolTabTxt.color = whiteColor;
        motiveTabTxt.color = greyColor;
    }

    public void OnClickedMotiveTap()
    {
        tabContentArea.SetTabTitleTxt("MOTIVE");
        tabContentArea.SetBattleBtn("MOTIVE");
        suspectTabTxt.color = greyColor;
        toolTabTxt.color = greyColor;
        motiveTabTxt.color = whiteColor;
    }
    

    // 저장된 질문 데이터 확인
    /*void CheckQCData()
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
    }*/
}
