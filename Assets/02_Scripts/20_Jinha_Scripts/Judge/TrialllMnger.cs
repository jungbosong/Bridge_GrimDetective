using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialllMnger : MonoBehaviour
{
    private static TrialllMnger instance = null;
    public static TrialllMnger Instance {
        get{
            if(instance == null){
                var obj = FindObjectOfType<TrialllMnger>();
                if(obj != null) {
                    instance = obj;
                } else {
                    var newObj = new GameObject().AddComponent<TrialllMnger>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }
    public GameObject NominateButton;
    public GameObject CompleteMysteryNote;
    public GameObject MysteryNoteCompletePanel;
    public GameObject MysteryNotePanel;
    int decidedCandidateCnt = 0; // 결정한 후보 개수
    public Dictionary<int, int> selectedCombination = new Dictionary<int, int>(){ 
        // 플레이어가 선택한 추리 조합
        {0,-1},
        {1,-1},
        {2,-1}
    }; 
    public void UpDecidedCandidateCnt()
    {
        if(decidedCandidateCnt >= 3) {
            return;
        } else decidedCandidateCnt++;
    }
    public void DownDecidedCandidateCnt()
    {
        if(decidedCandidateCnt <= 0) {
            return;
        } else decidedCandidateCnt--;
    }
    public void CheckCombinationCompleted(GameObject MysteryNoteCompletePanel){
        if(decidedCandidateCnt>=3) {
            Debug.Log("조합이 완성되었습니다.");
            MysteryNotePanel.SetActive(false);
            NominateButton.SetActive(false);
            CompleteMysteryNote.SetActive(true);
        }
    }
    public int GetSelectedCombination(int clueType)
    {
        int clueNum;
        if(selectedCombination.TryGetValue(clueType, out clueNum)) return clueNum;
        else return 0;
    }

    public void ShowCompleteMysteryNotePanel(){
        MysteryNoteCompletePanel.SetActive(true);
    }
}