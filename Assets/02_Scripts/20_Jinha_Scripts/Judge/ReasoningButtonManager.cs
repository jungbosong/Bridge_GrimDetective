using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReasoningButtonManager : MonoBehaviour
{
    TrialllMnger TrialllMnger;

    public GameObject OpinionPopup;
    public GameObject MysteryNoteCompletePanel;
    public Text OpinionText;

    int selectedClueType = -1, selectedClueNum = -1; // 선택된 단서 종류와 번호 (-1은 미정)

    void Awake(){
        TrialllMnger = this.GetComponent<TrialllMnger>();
    }

    public void ShowOpinionPopup(int index)
    {
        int number=4*MysteryNote.Instance.tmpTapButtonId[0]+index;
        OpinionPopup.SetActive(true);
        OpinionText.GetComponent<Text>().text=MysteryNote.Instance.SelectedButtonExplanationData[number];
        MysteryNote.Instance.tmpTapButtonId[1]=index;
        // 결정한 후보 개수 하나 증가
        if(TrialllMnger.Instance.selectedCombination[MysteryNote.Instance.tmpTapButtonId[0]]==-1) TrialllMnger.Instance.UpDecidedCandidateCnt();
        Debug.Log(index+"번째 버튼을 선택하였습니다.");
        
    }

    public void OnClickedConvictionBtn()
    {
        // 선택한 조합 내용 선택한걸로 변경
        TrialllMnger.Instance.selectedCombination[MysteryNote.Instance.tmpTapButtonId[0]]=MysteryNote.Instance.tmpTapButtonId[1];
        TrialllMnger.Instance.CheckCombinationCompleted(MysteryNoteCompletePanel);
    }
    public void OnClickedHoldBtn()
    {
        // 결정한 후보 개수 하나 감소
        TrialllMnger.Instance.DownDecidedCandidateCnt();
    }
}
