using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public GameObject YellowAreaPrefab, WhiteAreaPrefab; //프리팹
    public GameObject ContentObj;       // 스크롤뷰 content
    public Scrollbar scrollBar;
    AreaScript LastArea;
    AreaScript Area;
    public GameObject newYellowArea, newWhiteArea;

    public void Chat(bool isSend, string text, string user) 
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
       
        // 채팅의 줄 길이 조정   
        float X = Area.TextRect.sizeDelta.x + 42;
        float Y = Area.TextRect.sizeDelta.y;
        if (Y > 49)
        {
            for (int i = 0; i < 200; i++)
            {
                Area.BoxRect.sizeDelta = new Vector2(X - i * 2, Area.BoxRect.sizeDelta.y);
                Fit(Area.BoxRect);

                if (Y != Area.TextRect.sizeDelta.y) { Area.BoxRect.sizeDelta = new Vector2(X - (i * 2) + 2, Y); break; }
            }
        }
        else Area.BoxRect.sizeDelta = new Vector2(X, Y);

        Area.User = user;

        Fit(Area.BoxRect);
        Fit(Area.AreaRect);
        Fit(ContentObj.GetComponent<RectTransform>());
    }
    void Fit(RectTransform Rect) => LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);
    void ScrollDelay() => scrollBar.value = 0;
}