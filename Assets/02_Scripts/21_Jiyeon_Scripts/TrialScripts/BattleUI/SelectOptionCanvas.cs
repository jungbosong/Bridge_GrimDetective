using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectOptionCanvas : MonoBehaviour
{
    [SerializeField]
    GameObject selectOptionCanvas;
    [SerializeField]
    GameObject optionBtnPanel;
    [SerializeField]
    Text questionText;

    public int selectedNum;
    
    public int GetSelectedNum()
    {
        return selectedNum;
    }

    // UI 보여지는 유무 조절 함수
    public void ShowSelectOptionCanvas()
    {
        Debug.Log("ShowSelectOptionCanvas");
        selectOptionCanvas.SetActive(true);
    }
    public void HideSelectOptionCanvas()
    {
        Debug.Log("HideSelectOptionCanvas");
        selectOptionCanvas.SetActive(false);
        HideSelectButton();
    }

    // 선택지 질문 설정하는 함수
    public void SetQuestionText(string question)
    {
        questionText.text = question;
    }

    // 선택지 버튼 보이는 개수 조절
    public void ShowSelectButton(int buttonNum)
    {
        Debug.Log("Show ButtonNum");
        Debug.Log(buttonNum);
        optionBtnPanel.transform.GetChild(buttonNum).gameObject.SetActive(true);
    }
    public void HideSelectButton()
    {
        for(int i = 0; i < optionBtnPanel.transform.childCount; i++)
        {
            optionBtnPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // 선택지 버튼 선택했을 때 실행되는 함수
    public void OnClickedOptionButton(int optionNum)
    {
        Debug.Log(optionNum);
        selectedNum  = optionNum;
        BattleManager.Instance.isChoiceButtonSelected = true;
        BattleManager.Instance.ShowAC(optionNum);
        HideSelectOptionCanvas();
    }

    // 선택지 내용 설정하는 함수
    public void SetSelectOptionButton(int buttonNum, string option)
    {
        GameObject btn = optionBtnPanel.transform.GetChild(buttonNum).gameObject;
        Text btnText = btn.GetComponentInChildren<Text>();
        btnText.text = option;
    }

    
}
