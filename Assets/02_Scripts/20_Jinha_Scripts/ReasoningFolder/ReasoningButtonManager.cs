using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReasoningButtonManager : MonoBehaviour
{
    TrialllMnger TrialllMnger;
    int selectedClueType = -1, selectedClueNum = -1; // 선택된 단서 종류와 번호 (-1은 미정)
    void Awake(){
        TrialllMnger = this.GetComponent<TrialllMnger>();
    }
    private void ShowOpinionPopup(string clueType, int clueNum)
    {

        // 주인공 의견 출력
        // 주인공 의견을 따로 저장하고 있어야 할 듯
        // -> 저장한다면 길이가 12개인 List<string>으로
    }
    public void SetTempCandidate(int clueType, int clueNum)
    {
        selectedClueType = clueType;
        selectedClueType = clueNum;
    }
    public void OnClickedConvictionBtn()
    {
        // 결정한 후보 개수 하나 증가
        TrialllMnger.Instance.UpDecidedCandidateCnt();
        // 선택한 조합 내용 선택한걸로 변경
        //TrialllMnger.Instance.SetSelectedCombination(selectedClueType, selectedClueNum);
    }
    public void OnClickedHoldBtn()
    {
        // 결정한 후보 개수 하나 감소
        TrialllMnger.Instance.DownDecidedCandidateCnt();
        // 선택한 조합 내용  미정으로 변경
        //TrialllMnger.Instance.SetSelectedCombination(selectedClueType, -1);
    }
}
