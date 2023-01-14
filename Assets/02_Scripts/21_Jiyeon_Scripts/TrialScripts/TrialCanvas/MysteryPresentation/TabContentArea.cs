using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QuestionData;

public class TabContentArea : MonoBehaviour
{
    public PopupArea popupArea;
    [SerializeField] Text tabTitleTxt;
    [SerializeField] List<Button> btns = new List<Button>();
    public List<Sprite> suspectSprites = new List<Sprite>();
    public List<Sprite> toolSprites = new List<Sprite>();
    public List<Sprite> motiveSprites = new List<Sprite>();
    [SerializeField] List<Text> texts = new List<Text>();
    public List<string> suspectNames = new List<string>();
    public List<string> toolNames = new List<string>();
    public List<string> motiveNames = new List<string>();
    public string nowStep = "Battle";
    public string proof;
    public BattleManager battleManager;

    void Awake() 
    {
        popupArea = popupArea.gameObject.GetComponent<PopupArea>();
    }

    public void SetTabTitleTxt(string tabName)
    {
        switch(tabName)
        {
            case "SUSPECT": 
            {
                tabTitleTxt.text = "범인은 누구일까?";
                break;
            }
            case "TOOL": 
            {
                tabTitleTxt.text = "흉기는 무엇일까?";
                break;
            }
            case "MOTIVE": 
            {
                tabTitleTxt.text = "동기는 무엇일까?";
                break;
            }
        }
    }

    public void SetBtn(string tabName)
    {
        for(int i = 0; i < 4; i++)
        {
            int tmp = i;
            switch(tabName)
            {
                case "SUSPECT": 
                {
                    btns[tmp].onClick.AddListener(() => OnClickedEvidenceBtn(tmp));
                    btns[i].transform.GetComponent<Image>().sprite = suspectSprites[i];
                    texts[i].text = suspectNames[i];
                    break;
                }
                case "TOOL": 
                {
                    btns[tmp].onClick.AddListener(() => OnClickedEvidenceBtn(tmp));
                    btns[i].transform.GetComponent<Image>().sprite = toolSprites[i];
                    texts[i].text = toolNames[i];
                    break;
                }
                case "MOTIVE": 
                {
                    btns[tmp].onClick.AddListener(() => OnClickedEvidenceBtn(tmp));
                    btns[i].transform.GetComponent<Image>().sprite = motiveSprites[i];
                    texts[i].text = motiveNames[i];
                    break;
                }
            }
        }
    }

    void OnClickedEvidenceBtn(int evidenceNum)
    {
        Debug.Log("OnClickedEvidenceBtn");
        Debug.Log("Num: " + evidenceNum);

        popupArea.SetEvidenceNum(evidenceNum);
        popupArea.ShowPopupArea();
    }

    public void SetBattleBtn(string tabName)
    {
        battleManager = battleManager.gameObject.GetComponent<BattleManager>();
        for(int i = 0; i < 4; i++)
        {
            int tmp = i;
            switch(tabName)
            {
                case "SUSPECT": 
                {
                    btns[tmp].onClick.RemoveAllListeners();
                    btns[tmp].onClick.AddListener(() => OnClickedProofBtn(tabName, tmp));
                    btns[tmp].onClick.AddListener(() => battleManager.ShowProofAction());
                    btns[i].transform.GetComponent<Image>().sprite = suspectSprites[i];
                    texts[i].text = suspectNames[i];
                    break;
                }
                case "TOOL": 
                {
                    btns[tmp].onClick.RemoveAllListeners();
                    btns[tmp].onClick.AddListener(() => OnClickedProofBtn(tabName, tmp));
                    btns[tmp].onClick.AddListener(() => battleManager.ShowProofAction());
                    btns[i].transform.GetComponent<Image>().sprite = toolSprites[i];
                    texts[i].text = toolNames[i];
                    break;
                }
                case "MOTIVE": 
                {
                    btns[tmp].onClick.RemoveAllListeners();
                    btns[tmp].onClick.AddListener(() => OnClickedProofBtn(tabName, tmp));
                    btns[tmp].onClick.AddListener(() => battleManager.ShowProofAction());
                    btns[i].transform.GetComponent<Image>().sprite = motiveSprites[i];
                    texts[i].text = motiveNames[i];
                    break;
                }
            }
        }
    }

    void OnClickedProofBtn(string tabName, int evidenceNum)
    {
        switch(tabName)
        {
            case "SUSPECT": 
            {
                proof = "0_" + evidenceNum.ToString();
                break;
            }
            case "TOOL": 
            {
                proof = "1_" + evidenceNum.ToString();
                break;
            }
            case "MOTIVE": 
            {
                proof = "2_" + evidenceNum.ToString();
                break;
            }
        }
    }
}
