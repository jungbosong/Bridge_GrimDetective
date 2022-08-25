using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

/*
public class Question
{
    public int questionNum;
    public List<Choice> choiceList;
}

public class Choice
{
    public int choiceNum;
    public List<Answer> answerList;
}

public class Answer
{
    public string key;
    public string contents;
}
*/
public class BattleManager : MonoBehaviour
{
    private static BattleManager instance = null;   
    [SerializeField]  
    GameObject selectOptionCanvasObj;
    SelectOptionCanvas selectOptionCanvas;

    // 전체 공방 질문 데이터
    List<string> questionData = new List<string>();
    // 현재 질문하는 데이터
    public List<List<string>> nowQuestionData = new List<List<string>>();
    int clickCount = -1;
    int answerCount = 0;
    // 현재 보고있는 질문 종류 확인하는 변수
    public List<bool> isChoiceQuestion = new List<bool>();
    // 현재 진행중인 질문 번호 확인하는 변수
    int questionNum;
    // 현재 진행중인 선택형 질문의 선택지를 저장하는 변수
    List<List<string>> choiceQuestionData = new List<List<string>>();
    // 현재 진행중인 증거 제시형 질문의 선택지를 저장하는 변수
    List<List<string>> evidenceQuestionData = new List<List<string>>();
    // 선택형 질문의 반응을 저장하는 변수
    List<List<string>> choiceAnswerData = new List<List<string>>();
    // 증거 제시 시 반응을 저장하는 변수
    List<List<string>> evidenceAnswerData = new List<List<string>>();
    
    public bool isChoiceButtonSelected = false;

    public Coroutine runningCorutine = null;   

     //싱글톤 제작함수
    public static BattleManager Instance {
        get{
            if(instance == null){
                var obj = FindObjectOfType<BattleManager>();
                if(obj != null) {
                    instance = obj;
                } else {
                    var newObj = new GameObject().AddComponent<BattleManager>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }

    private void Awake() 
    {
        selectOptionCanvas = selectOptionCanvasObj.GetComponent<SelectOptionCanvas>();
        selectOptionCanvas.HideSelectOptionCanvas();
        var objs = FindObjectsOfType<BattleManager>();
        if(objs.Length != 1) {
            Debug.Log("BattleManager 여러개 찾음");
            Debug.Log(gameObject.name);
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Debug.Log(gameObject.name);
    }
    

    public void ConnectData()
    {
         // 실행중인 코루틴이 있으면 멈추고 다시 실행
        if(runningCorutine!=null){
            StopAllCoroutines();
            runningCorutine = null;
        } 
        // 실행중인 코루틴이 없으면 대화 진행
        else {
            BattleConnectData.Instance.StartDialogNetConnect();
        }
    }

    public void ClickedChatScreen()
    {        
        Debug.Log("CleckedChatScreen");

        if(clickCount == -1)
        {
            questionNum = 0;
            questionData = BattleConnectData.Instance.GetQuestionData();
            ParsingGuestionData();
        }

        if(questionData == null)
        {
            clickCount = -1;
            ClickedChatScreen();
        }   
        
        if(isChoiceButtonSelected)
        {
            answerCount++;
            Debug.Log("Add AnswerCount");
            Debug.Log(answerCount);
            if(answerCount >= 4)
            {
                Debug.Log("End Answer");
                Debug.Log(answerCount);
                answerCount = 0;
                clickCount = nowQuestionData[questionNum].Count;
                isChoiceButtonSelected = false;
            }
        }
        else
        {
            // 대사 한 줄씩 출력
            clickCount++;
            Debug.Log("Added Click Couunt");
            Debug.Log(clickCount);
        }
       
        if(clickCount >= nowQuestionData[questionNum].Count)
        {
            Debug.Log("질문 업데이트");
            questionNum += 1;
            clickCount = 0;
            Debug.Log("questionNum");
            Debug.Log(questionNum);
            if(questionNum >= questionData.Count)
            {
                Debug.Log("마지막 질문이였음");
                questionNum = 0;
            }
            // 선택지 버튼 띄우기
            if(clickCount == 0 && isChoiceQuestion[questionNum])
            {
                Debug.Log("choiceOptionCount");
                Debug.Log(choiceQuestionData[questionNum].Count-1);

                // 질문 내용 변경
                selectOptionCanvas.SetQuestionText(choiceQuestionData[questionNum][0]);

                for(int i = 0; i < choiceQuestionData[questionNum].Count-1; i++)
                {
                    // 선택지 내용 변경
                    selectOptionCanvas.ShowSelectButton(i);
                    selectOptionCanvas.SetSelectOptionButton(i, choiceQuestionData[questionNum][i+1]);
                }
                selectOptionCanvas.ShowSelectOptionCanvas();
                Debug.Log("ShowSelectOptionCanvas");
            }
        }   
        
        Debug.Log("questionNum");
        Debug.Log(questionNum);
        Debug.Log("clickCount");
        Debug.Log(clickCount);
        Debug.Log("대사 출력");
        //Debug.Log(nowQuestionData[questionNum][clickCount]);
    }

    public void AddClickCount(int increaseNum)
    {
        clickCount += increaseNum;
    }

    public void ShowAC(int selectedNum)
    {
        for(int i = selectedNum; i < selectedNum + 4; i++)
        {
            Debug.Log(choiceAnswerData[questionNum][i]);
        }
    }

    void ParsingGuestionData()
    {
        for(int i = 0; i < questionData.Count; i++)             // 몇번째 질문을 볼건지에 대한 for문
        {
            List<string> tmpList = new List<string>();          // 대사를 저장하는 배열
            List<string> tmpChoiceList = new List<string>();    // 선택지 지문에 대한 정보
            List<string> tmpSelectedAnswerList = new List<string>();    // 각 선택지에 대한 대답
            

            for(int j = 0; j < questionData[i].Split('\n').Length-1; j++)     // 각 질문을 한줄씩 쪼개서 보는 for문
            {
                string key = questionData[i].Split('\n')[j].Split('\t')[0]; // Keyword만 저장해두는 변수
                string contents = questionData[i].Split('\n')[j];           // 대사를 한 줄씩 저장해두는 변수
                tmpList.Add(contents);

                CheckQuestionType(key);   // QC / QP 질문 타입 구분              

                // 선택지 질문 임시 저장  
                if (key.Contains("QC"))
                {
                    tmpChoiceList.Add(contents.Split('\t')[1]);
                }

                // 선택지별 답변 임시 저장
                if (key.Contains("AC")) 
                {
                    tmpSelectedAnswerList.Add(contents);
                }
            }
            nowQuestionData.Add(tmpList);
            // 문제별 선택지 질문 저장  
            choiceQuestionData.Add(tmpChoiceList);
            // 문제별 선택지에 대한 답 저장
            choiceAnswerData.Add(tmpSelectedAnswerList);    
        }     
    }

    void CheckQuestionType(string key)
    {
        // 각 질문의 유형 확인
        if(key == "QC")
        {
            isChoiceQuestion.Add(true);
        }
        else if(key == "QP")
        {
            isChoiceQuestion.Add(false);
        }
    }

}
