using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupArea : MonoBehaviour
{
    int evidenceType, evidenceNum;
    [SerializeField] MysteryPresentationMng mysteryPresentationMng;
    [SerializeField] TabButtonArea tabButtonArea;
    [SerializeField] Text popupText;
    public List<string> suspectReaction = new List<string>();
    public List<string> toolReaction = new List<string>();
    public List<string> motiveReaction = new List<string>();
    [SerializeField] Button sureButton;
    [SerializeField] Button notSureButton;
    [SerializeField] GameObject investLogArea;

    void Awake() 
    {
        mysteryPresentationMng = mysteryPresentationMng.gameObject.GetComponent<MysteryPresentationMng>();
        tabButtonArea = tabButtonArea.gameObject.GetComponent<TabButtonArea>();
        sureButton.onClick.AddListener(OnClickedSureBtn);
        notSureButton.onClick.AddListener(OnClickedNotSureBtn);
        this.gameObject.SetActive(false);
    }

    public void SetEvidenceType(int type)
    {
        evidenceType = type;
    }

    public void SetEvidenceNum(int num)
    {
        evidenceNum = num;
    }

    void SetPopupText()
    {
        switch(evidenceType)
        {
            case 0: {
                popupText.text = suspectReaction[evidenceNum];
                break;
            }
            case 1: {
                popupText.text = toolReaction[evidenceNum];
                break;
            }
            case 2: {
                popupText.text = motiveReaction[evidenceNum];
                break;
            }
        }
    }

    public void ShowPopupArea()
    {
        Debug.Log("ShowPopupArea");
        SetPopupText();
        this.gameObject.SetActive(true);
        this.gameObject.SetActive(true);
    }

    public void HidePopupArea()
    {
        Debug.Log("HidePopupArea");
        this.gameObject.SetActive(false);
    }


    void OnClickedSureBtn()
    {
        switch(evidenceType)
        {
            case 0: 
            {
                mysteryPresentationMng.suspectNum = evidenceNum;
                mysteryPresentationMng.isSelectedSuspect = true;
                tabButtonArea.OnClickedToolTap();
                investLogArea.SetActive(false);
                break;
            }
            case 1: 
            {
                mysteryPresentationMng.toolNum = evidenceNum;
                mysteryPresentationMng.isSelectedTool = true;
                tabButtonArea.OnClickedMotiveTap();
                investLogArea.SetActive(false);
                break;
            }
            case 2: 
            {
                mysteryPresentationMng.motiveNum = evidenceNum;
                mysteryPresentationMng.isSelectedMotive = true;
                HidePopupArea();
                investLogArea.SetActive(false);
                break;
            }
        }
    }

    void OnClickedNotSureBtn()
    {
        switch(evidenceType)
        {
            case 0: 
            {
                mysteryPresentationMng.suspectNum = -1;
                mysteryPresentationMng.isSelectedSuspect = false;
                break;
            }
            case 1: 
            {
                mysteryPresentationMng.toolNum = -1;
                mysteryPresentationMng.isSelectedTool = false;
                break;
            }
            case 2: 
            {
                mysteryPresentationMng.motiveNum = -1;
                mysteryPresentationMng.isSelectedMotive = false;
                break;
            }
        }
        HidePopupArea();
    }   
}
