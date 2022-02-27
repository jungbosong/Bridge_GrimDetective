using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReasoningTapManager : MonoBehaviour
{
    ReasoningTapData ReasoningTapData;
    public GameObject ReasoningInventoryPopup;
    public GameObject ClueText;
    public Text ButtonText1;
    public Text ButtonText2;
    public Text ButtonText3;
    public Text ButtonText4;
    void Awake(){
        ReasoningTapData = this.GetComponent<ReasoningTapData>();
    }
    private void InitSurveyRecords()
    {
	    for(int i = 0; i<3; i++) {
            for(int j=0;j<4;j++){
                MysteryNote.Instance.isDecided[i,j] = false;
            }
	    }
    }

    //지명하기 버튼을 누르면 실행
    public void OnClicedkNominateBtn()
    {
        ShowMysteryNotePanel();
    }
    public void ShowMysteryNotePanel()
    {
        ReasoningInventoryPopup.SetActive(true);
    }

    //범인/흉기/동기 탭 누르면 실행
    public void OnClickedTap()
    {
        // "범인은 누구일까?"등등 출력
        switch(MysteryNote.Instance.clickedeasoningTapId)
        {
            case 0: ClueText.GetComponent<Text>().text="범인은 누굴까?";Debug.Log("범인Tap Click");break;
            case 1: ClueText.GetComponent<Text>().text="흉기는 무엇일까?";Debug.Log("흉기Tap Click");break;
            case 2: ClueText.GetComponent<Text>().text="동기는 무엇일까?";Debug.Log("동기Tap Click");break;
            default: ClueText.GetComponent<Text>().text="범인은? 흉기는? 동기는?";break;
        }
        // 버튼 이름, 이미지 설정
        ButtonText1.GetComponent<Text>().text=MysteryNote.Instance.ButtonNameData[0][MysteryNote.Instance.clickedeasoningTapId];
        Debug.Log(MysteryNote.Instance.ButtonNameData[0][MysteryNote.Instance.clickedeasoningTapId]);
        ButtonText2.GetComponent<Text>().text=MysteryNote.Instance.ButtonNameData[1][MysteryNote.Instance.clickedeasoningTapId];
        Debug.Log(MysteryNote.Instance.ButtonNameData[1][MysteryNote.Instance.clickedeasoningTapId]);
        ButtonText3.GetComponent<Text>().text=MysteryNote.Instance.ButtonNameData[2][MysteryNote.Instance.clickedeasoningTapId];
        Debug.Log(MysteryNote.Instance.ButtonNameData[2][MysteryNote.Instance.clickedeasoningTapId]);
        ButtonText4.GetComponent<Text>().text=MysteryNote.Instance.ButtonNameData[3][MysteryNote.Instance.clickedeasoningTapId];
        Debug.Log(MysteryNote.Instance.ButtonNameData[3][MysteryNote.Instance.clickedeasoningTapId]);
    }
}
