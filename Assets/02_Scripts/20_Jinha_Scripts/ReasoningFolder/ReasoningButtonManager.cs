using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReasoningButtonManager : MonoBehaviour
{
    TrialllMnger TrialllMnger;
    public GameObject OpinionPopup;
    public Text OpinionText;
    int selectedClueType = -1, selectedClueNum = -1; // 선택된 단서 종류와 번호 (-1은 미정)
    void Awake(){
        TrialllMnger = this.GetComponent<TrialllMnger>();
    }
    public void ShowOpinionPopup()
    {
        int index=4*MysteryNote.Instance.clickedeasoningTapId+MysteryNote.Instance.clickedeasoningButtonId;
        OpinionText.GetComponent<Text>().text=MysteryNote.Instance.SelectedButtonExplanationData[index];
        Debug.Log(MysteryNote.Instance.clickedeasoningTapId+" "+MysteryNote.Instance.clickedeasoningButtonId);
    }
    public void OnClickedConvictionBtn()
    {
        int index1=MysteryNote.Instance.clickedeasoningTapId;
        int index2=MysteryNote.Instance.clickedeasoningButtonId;

        // 결정한 후보 개수 하나 증가
        if(TrialllMnger.Instance.selectedCombination[index1]==-1) TrialllMnger.Instance.UpDecidedCandidateCnt();
        // 선택한 조합 내용 선택한걸로 변경
        TrialllMnger.Instance.selectedCombination[index1]=index2;
        Debug.Log(MysteryNote.Instance.clickedeasoningTapId+" "+MysteryNote.Instance.clickedeasoningButtonId+" 확정");
        TrialllMnger.Instance.CheckCombinationCompleted();
    }
    public void OnClickedHoldBtn()
    {
        // 결정한 후보 개수 하나 감소
        TrialllMnger.Instance.DownDecidedCandidateCnt();
    }
}
