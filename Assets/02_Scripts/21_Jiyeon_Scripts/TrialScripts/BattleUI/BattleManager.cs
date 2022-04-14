using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public GameObject YelloArea, WhiteArea;
    public RectTransform ContentRect;
    public Scrollbar scrollBar;
    UIAreaScript LastArea;

    public async void Chat(bool isSend, string text, Texture picture)
    {
        if(text.Trim() == "") return;

        bool isBottom = scrollBar.value <= 0.00001f;

        UIAreaScript Area = Instantiate(isSend ? YelloArea : WhiteArea).GetComponent<UIAreaScript>();
        Area.transform.SetParent(ContentRect.transform, false);
        Area.BoxRect.sizeDelta = new Vector2(600, Area.BoxRect.sizeDelta.y);
        Area.TextRect.GetComponent<TextMeshProUGUI>().text = text;
        Fit(Area.BoxRect);
        Fit(Area.AreaRect);
        Fit(ContentRect);
        LastArea = Area;
        

        // 두 줄 이상이면 크기를 줄여가면서, 한 줄이 아래로 내려가면 바로 전 크기 대입
        float X = Area.TextRect.sizeDelta.x + 42;
        float Y = Area.TextRect.sizeDelta.y;
        if(Y > 49)
        {
            for(int i = 0; i < 200; i++) 
            {
                Area.BoxRect.sizeDelta = new Vector2(X- i * 2, Area.BoxRect.sizeDelta.y);
                Fit(Area.BoxRect);

                if(Y != Area.TextRect.sizeDelta.y) { Area.BoxRect.sizeDelta = new Vector2(X - (i * 2) + 2, Y); break;}
            }
        }

        if(!isSend && !isBottom) return;
        Invoke("ScrollDelay", 0.03f);
        
    }

    void Fit(RectTransform Rect) => LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);

    void ScrollDelay() => scrollBar.value = 0;
}
