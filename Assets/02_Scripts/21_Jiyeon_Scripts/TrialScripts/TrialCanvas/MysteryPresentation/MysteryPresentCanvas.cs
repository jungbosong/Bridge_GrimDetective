using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MysteryPresentCanvas : MonoBehaviour
{
    [SerializeField] MysteryPresentationMng mysteryPresentationMng;
    [SerializeField] ErrorArea errorArea;
    [SerializeField] Button nominateBtn;
    [SerializeField] Button decideBtn;
    [SerializeField] GameObject investLogArea, popupArea, decideArea;

    void Awake() 
    {
        mysteryPresentationMng = this.GetComponent<MysteryPresentationMng>();
        errorArea = errorArea.gameObject.GetComponent<ErrorArea>();
        nominateBtn.onClick.AddListener(ShowInvestLog);
        decideBtn.onClick.AddListener(OnClickedDecideBtn);
        investLogArea.SetActive(false);
        this.gameObject.SetActive(false);
    }

    void ShowInvestLog()
    {
        investLogArea.SetActive(true);
    }
    
    void OnClickedDecideBtn()
    {
        if(!mysteryPresentationMng.isSelectedSuspect)
        {
            errorArea.SetErrorTxt("범인을 선택하세요");
            errorArea.ShowErrorTxt();
        }
        else if(!mysteryPresentationMng.isSelectedTool)
        {
            errorArea.SetErrorTxt("흉기를 선택하세요");
            errorArea.ShowErrorTxt();
        }
        else if(!mysteryPresentationMng.isSelectedMotive)
        {
            errorArea.SetErrorTxt("동기를 선택하세요");
            errorArea.ShowErrorTxt();
        }
        else
        {
            mysteryPresentationMng.ShowNum();
            decideArea.SetActive(true);
            decideArea.GetComponent<DecideArea>().ShowDecideArea();
        }
    }
}
