/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Networking;

public class EnterCourtChatMng : MonoBehaviour
{
    public Transform BubbleScroll;
    public int ClickCount;
    public GameObject StepZeroPanel;
    public GameObject FiveStepCanvas;

    public void EnterCourtClick(){
        Transform[] BubbleList=BubbleScroll.GetComponentsInChildren<Transform>();
        BubbleList[ClickCount].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        //BubbleList[ClickCount].SetActive(true);
        ClickCount++;
        if(ClickCount>13){
            for(int i=0; i++; i<BubbleList.Count) {
                BubbleList[ClickCount].transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            }
            BubbleList[0].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
}
*/