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
    int decidedCandidateCnt = 0; // 결정한 후보 개수
    Dictionary<string, int> selectedCombination = new Dictionary<string, int>(){ 
        // 플레이어가 선택한 추리 조합
        /*{"범인", 0};
        {"흉기", 4};
        {"동기", 8};*/
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
    public void SetSelectedCombination(string selectedClueType, int selectedClueNum)
    {
        selectedCombination[selectedClueType] = selectedClueNum;
    }
    /*public int GetSelectedCombination(string clueType)
    {
        int clueNum;
        if(selectedCombination.TryGetValue(clueType, out clueNum))
            return clueNum;
    }*/
}
