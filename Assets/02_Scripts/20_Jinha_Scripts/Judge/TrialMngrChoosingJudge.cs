using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrialMngrChoosingJudge : MonoBehaviour
{
    CheckInventoryData CheckInventoryData;
    MysteryNote MysteryNote;

    public GameObject JudgeStartCanvas;
    public GameObject MysteryNotePanel;
    public GameObject NominateButton;
    public RectTransform CheckInvestigationText;

    //List<Judge> judgeList = new List<Judge>();

    /*public class Judge 
    {
        public string trialDate{get; set;} // 재판 날짜
        public string trialTime{get; set;} // 재판 시간 
        public enum _juryTendency {Order,Chaos}; // 배심원 성향
        public _juryTendency juryTendency {get; set;}
    }

    private void InitJudgeList()
    {
        judgeList.Add(new Judge(trialDate = "", trialTime = "", juryTendency = Order));
        judgeList.Add(new Judge(trialDate = "", trialTime = "", juryTendency = Chaos));
    }*/

    void Awake(){
        CheckInventoryData = this.GetComponent<CheckInventoryData>();
        MysteryNote = this.GetComponent<MysteryNote>();
    }

    
    /// 재판을 시작하는 함수
    /// 재판 입구의 NPC를 누르면 실행되는 함수
    /// 재판 입구의 NPC의 OnClick함수에 연결되는 함수
    public void GoToTrialScene()
    {
        //재판시작조건을 충족했는지 확인
        bool completeInvestigation = true;

        //테스트용
        /*for(int i=0;i<4;i++){
            for(int j=0;j<5;j++) {
                completeInvestigation=completeInvestigation&CheckInventoryData.GetClueISAcquired(i,j);
            }
        }*/
        
        if(!completeInvestigation) CheckInvestigationText.GetComponent<Text>().text = "조사를 완료하지 않으면 재판을 진행할 수 없어! 돌아가!";
        else JudgeStartCanvas.SetActive(true);

        //InitJudgeList();
    }


    /// 현재 재판장의 번호를 플레이어가 선택한 재판장의 번호로 설정하는 함수
    /// 재판장 선택 버튼에 OnClick함수로 들어간다.
    public void SetSelectedJudgeNum(int judgeNum)
    {
        MysteryNote.Instance.ChooseJudge=judgeNum;
        Debug.Log(judgeNum+"번쨰 분위기 재판장을 선택하였습니다.");
        NominateButton.SetActive(true);
    }
}
