using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreEvaluationCanvas : MonoBehaviour
{
    List<Tuple<string,string>> dialogueData = new List<Tuple<string,string>>();
    int cnt = -1;        // 현재 진행 중인 대화 번호
    int maxCnt = 0;
    bool isStarted = false;
    ChatManager chatManager;
    [SerializeField] GameObject battleCanvas;
    [SerializeField] Image leftImg;
    [SerializeField] Image rightImg;
    Color greyColor, whiteColor;
    CombinationGraph combinationGraph;
    [SerializeField] MysteryPresentationMng mysteryPresentationMng;
    public int suspect, weapon, motive;
    string result;

    void Awake() 
    {
        chatManager = this.gameObject.GetComponent<ChatManager>();   
        combinationGraph = this.gameObject.GetComponent<CombinationGraph>();
        leftImg = leftImg.transform.GetComponent<Image>();
        rightImg = rightImg.transform.GetComponent<Image>();
        ColorUtility.TryParseHtmlString("#484848", out greyColor);
        ColorUtility.TryParseHtmlString("#FFFFFF", out whiteColor);
        this.gameObject.SetActive(false);
        mysteryPresentationMng = mysteryPresentationMng.gameObject.GetComponent<MysteryPresentationMng>();
    }

    void SetData(string fileName)
    {
        dialogueData.Clear();
        dialogueData = TxtFileReader.Instance.GetData(fileName);
        Debug.Log("선평가 대사를 불러온 내용을 출력합니다.");
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

    public void StartPreJudgment()
    {
        SetData("1_PreJudgment");
        cnt = -1;
    }

    public void OnClickedDialogueBtn()
    { 
        Debug.Log("선평가 버튼이 눌러졌습니다..");
        cnt++;
        Debug.Log("현재 cnt: " + cnt + "\tmaxCnt: " + maxCnt);
        if(cnt < maxCnt-1)
        {
            if(cnt == 3) ShowPrejudgResult();
            else ShowLine(dialogueData[cnt]);
        }
        else
        {
            battleCanvas.SetActive(true);
            battleCanvas.GetComponent<BattleManager>().SetQeustionData();
            battleCanvas.GetComponent<BattleManager>().ProcessBattle();
            this.gameObject.SetActive(false);
            chatManager.DestroyAllBoxes();
        }
    }

    void ShowPrejudgResult()
    {
        // 플레이어가 선택한 추리 조합 정보 불러오기
        suspect = mysteryPresentationMng.suspectNum;
        weapon = mysteryPresentationMng.weaponNum;
        motive = mysteryPresentationMng.motiveNum;

        Debug.Log("최종 선택된 범인: " + suspect);
        Debug.Log("최종 선택된 흉기: " + weapon);
        Debug.Log("최종 선택된 동기: " + motive);

        int row = suspect;
        int col = weapon + 4;
        int firstWeight = combinationGraph.GetWeight(row,col);
        Debug.Log("firstWeight: " + firstWeight);
        row = col;
        col = motive + 8;
        int secondWeight = combinationGraph.GetWeight(row,col);
        Debug.Log("secondWeight: " + secondWeight);

        int endingType;

        if(firstWeight == secondWeight) {  // 조합에따라 정해진  엔딩타입 설정
            endingType = firstWeight;
        } else {
            endingType = 0;  // 불가능한 추리
        }
        Debug.Log("결정된 엔딩 종류");
        
        switch (endingType)
        {
            case 0: result = "불가능"; break;
            case 1: result = "최악"; break;
            case 2: result = "보통"; break;
            case 3: result = "최선"; break;
        }
        ShowLine(new Tuple<string, string>("저승사자", result));
    }

}
