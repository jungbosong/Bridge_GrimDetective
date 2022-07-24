using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReasoningTapManager : MonoBehaviour
{
    MysteryNote MysteryNote;

    public GameObject OpinionPopup;
    public GameObject ClueText;
    public Text ButtonText1;
    public Text ButtonText2;
    public Text ButtonText3;
    public Text ButtonText4;

    void Awake(){
        MysteryNote = this.GetComponent<MysteryNote>();

    }
    private void InitSurveyRecords()
    {
	    for(int i = 0; i<3; i++) {
            for(int j=0;j<4;j++){
                MysteryNote.Instance.isDecided[i,j] = false;
            }
	    }
    }

    //범인/흉기/동기 탭 누르면 실행
    public void OnClickedTap(int clickedTapId)
    {
        MysteryNote.Instance.tmpTapButtonId[0]=clickedTapId;
        Debug.Log(clickedTapId+"번째 탭을 선택하였습니다.");
        // "범인은 누구일까?"등등 출력
        switch(clickedTapId)
        {
            case 0: ClickSuspectTap();break;
            case 1: ClickWeaponTap();break;
            case 2: ClickMotiveTap();break;
            default: ClueText.GetComponent<Text>().text="범인은? 흉기는? 동기는?";break;
        }
        OpinionPopup.SetActive(false);
    }
    public void ClickSuspectTap(){
        ClueText.GetComponent<Text>().text="범인은 누굴까?";
        ButtonText1.GetComponent<Text>().text=MysteryNote.Instance.ButtonNameData[0];
        ButtonText2.GetComponent<Text>().text=MysteryNote.Instance.ButtonNameData[1];
        ButtonText3.GetComponent<Text>().text=MysteryNote.Instance.ButtonNameData[2];
        ButtonText4.GetComponent<Text>().text=MysteryNote.Instance.ButtonNameData[3];
    }
    public void ClickWeaponTap(){
        ClueText.GetComponent<Text>().text="흉기는 무엇일까?";
        ButtonText1.GetComponent<Text>().text=MysteryNote.Instance.ButtonNameData[4];
        ButtonText2.GetComponent<Text>().text=MysteryNote.Instance.ButtonNameData[5];
        ButtonText3.GetComponent<Text>().text=MysteryNote.Instance.ButtonNameData[6];
        ButtonText4.GetComponent<Text>().text=MysteryNote.Instance.ButtonNameData[7];
    }
    public void ClickMotiveTap(){
        ClueText.GetComponent<Text>().text="동기는 무엇일까?";
        ButtonText1.GetComponent<Text>().text=MysteryNote.Instance.ButtonNameData[8];
        ButtonText2.GetComponent<Text>().text=MysteryNote.Instance.ButtonNameData[9];
        ButtonText3.GetComponent<Text>().text=MysteryNote.Instance.ButtonNameData[10];
        ButtonText4.GetComponent<Text>().text=MysteryNote.Instance.ButtonNameData[11];
    }
}