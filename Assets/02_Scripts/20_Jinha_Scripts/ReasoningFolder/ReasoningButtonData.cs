using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReasoningButtonData : MonoBehaviour
{//선택지 버튼에 넣어서 해당 탭의 id를 받기
    public int ReasoningButtonId;
    public void ClickReasoningButton(){
        MysteryNote.Instance.clickedeasoningButtonId=ReasoningButtonId;
    }
}
