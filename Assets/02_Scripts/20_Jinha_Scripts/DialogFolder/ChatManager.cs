using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public GameObject YellowAreaPrefab, WhiteAreaPrefab; //프리팹
    public GameObject ContentObj;       // 스크롤뷰 content
    AreaScript Area;
    GameObject newYellowArea, newWhiteArea;

    public void Chat(bool isSend, string text) 
    {    
        //윤가람(True)은 우측 YellowArea, 타인(False)은 좌측 WhiteArea에 텍스트
        if(isSend) { // YelloArea생성
            newYellowArea = Instantiate(YellowAreaPrefab); 
            newYellowArea.transform.SetParent(ContentObj.transform,false);
            Area = newYellowArea.GetComponent<AreaScript>();
            Area.BoxRect.sizeDelta = new Vector2(600, Area.BoxRect.sizeDelta.y);
            Area.TextRect.GetComponent<Text>().text = text;
            Fit(Area.BoxRect);
            }   
        else { // WhiteArea생성
            newWhiteArea = Instantiate(WhiteAreaPrefab);
            newWhiteArea.transform.SetParent(ContentObj.transform,false);
            Area = newWhiteArea.GetComponent<AreaScript>();
            Area.BoxRect.sizeDelta = new Vector2(600, Area.BoxRect.sizeDelta.y);
            Area.TextRect.GetComponent<Text>().text = text;
            Fit(Area.BoxRect);       
        }
    }
    void Fit(RectTransform Rect) => LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);

    public void DestroyAllBoxes()
    {
        Transform[] childList = ContentObj.GetComponentsInChildren<Transform>();

        if (childList != null) {
            for (int i = 1; i < childList.Length; i++) {
                if (childList[i] != transform)
                    Destroy(childList[i].gameObject);
            }
        }
    }
}