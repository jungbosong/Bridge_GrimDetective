using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
    private static BattleManager instance = null;     
    int clickCount = -1;
    // 현재 보고있는 질문 종류 확인하는 변수
    public bool isChoiceQuestion;
    // 현재 진행중인 질문 번호 확인하는 변수
    int questionNum;
    List<string> questionData = new List<string>();
    public List<List<string>> nowQuestionData = new List<List<string>>();
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
        //BattleConnectData.Instance.OnclickTestButton();
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

        // 대사 한 줄씩 출력
        clickCount++;
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
        }   
        
        
        Debug.Log("questionNum");
        Debug.Log(questionNum);
        Debug.Log("clickCount");
        Debug.Log(clickCount);
        Debug.Log("대사 출력");
        Debug.Log(nowQuestionData[questionNum][clickCount]);
    }

    void ParsingGuestionData()
    {
        for(int i = 0; i < questionData.Count; i++)
        {
            List<string> tmpList = new List<string>();
            for(int j = 0; j <questionData[i].Split('\n').Length-1; j++)
            {
                if(questionData[i].Split('\n')[j].Split('\t')[0] == "QC")
                {
                    isChoiceQuestion = true;
                }
                else
                {
                    isChoiceQuestion = false;
                }
                string tmpData = questionData[i].Split('\n')[j];
                if(tmpData.Split('\t').Length == 0 || tmpData.Split('\t') == null )
                {
                    continue;
                }
                else if(tmpData.Split('\t').Length > 0)
                {
                    tmpList.Add(tmpData.Split('\t')[1]);
                }
            }
            nowQuestionData.Add(tmpList);
        }        
    }
    /*public GameObject YelloArea, WhiteArea;
    public RectTransform ContentRect;
    public Scrollbar scrollBar;
    UIAreaScript LastArea;

    public async void Chat(bool isSend, string text, Texture picture)
    {
        if(text.Trim() == "") return;

        bool isBottom = scrollBar.value <= 0.00001f;

        UIAreaScript Area = Instantiate(isSend ? YelloArea : WhiteArea).GetComponent<UIAreaScript>();
        Area.transform.SetParent(ContentRect.transform, false);
        Area.BoxRect.sizeDelta = new Vector2(600, Area.BoxRect.sizeDelta.y);
        Area.TextRect.GetComponent<TextMeshProUGUI>().text = text;
        Fit(Area.BoxRect);
        Fit(Area.AreaRect);
        Fit(ContentRect);
        LastArea = Area;
        

        // 두 줄 이상이면 크기를 줄여가면서, 한 줄이 아래로 내려가면 바로 전 크기 대입
        float X = Area.TextRect.sizeDelta.x + 42;
        float Y = Area.TextRect.sizeDelta.y;
        if(Y > 49)
        {
            for(int i = 0; i < 200; i++) 
            {
                Area.BoxRect.sizeDelta = new Vector2(X- i * 2, Area.BoxRect.sizeDelta.y);
                Fit(Area.BoxRect);

                if(Y != Area.TextRect.sizeDelta.y) { Area.BoxRect.sizeDelta = new Vector2(X - (i * 2) + 2, Y); break;}
            }
        }

        if(!isSend && !isBottom) return;
        Invoke("ScrollDelay", 0.03f);
        
    }

    void Fit(RectTransform Rect) => LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);

    void ScrollDelay() => scrollBar.value = 0;
    */

}
