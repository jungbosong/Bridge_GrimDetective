using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReasoningTapData : MonoBehaviour
{//탭 버튼에 넣어서 해당 탭의 id를 받기
    public int ReasoningTapId;
    public void ClickReasoningTap(){
        MysteryNote.Instance.clickedeasoningTapId=ReasoningTapId;
    }
}
