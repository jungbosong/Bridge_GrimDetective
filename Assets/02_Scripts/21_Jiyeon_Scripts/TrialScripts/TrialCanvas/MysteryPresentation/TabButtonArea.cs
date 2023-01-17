using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabButtonArea : MonoBehaviour
{
    [SerializeField] Button suspectTabBtn, toolTabBtn, motiveTabBtn;
    [SerializeField] PopupArea popupArea;
    [SerializeField] TextMeshProUGUI suspectTabTxt, toolTabTxt, motiveTabTxt;
    [SerializeField] TabContentArea tabContentArea;
    Color white, grey;
    public BattleManager battleManager;
    void Awake() 
    {
        tabContentArea = tabContentArea.gameObject.GetComponent<TabContentArea>();
        suspectTabBtn.onClick.AddListener(OnClickedSuspectTab);
        toolTabBtn.onClick.AddListener(OnClickedToolTap);
        motiveTabBtn.onClick.AddListener(OnClickedMotiveTap);
        popupArea = popupArea.gameObject.GetComponent<PopupArea>();
        ColorUtility.TryParseHtmlString("#EEEEEE", out white);
        ColorUtility.TryParseHtmlString("#767676", out grey);
        suspectTabTxt = suspectTabBtn.transform.GetComponentInChildren<TextMeshProUGUI>();
        toolTabTxt = toolTabBtn.transform.GetComponentInChildren<TextMeshProUGUI>();
        motiveTabTxt = motiveTabBtn.transform.GetComponentInChildren<TextMeshProUGUI>();
        OnClickedSuspectTab();
    }

    public void OnClickedSuspectTab()
    {
        popupArea.SetEvidenceType(0);
        popupArea.HidePopupArea();
        tabContentArea.SetTabTitleTxt("SUSPECT");
        tabContentArea.SetBtn("SUSPECT");
        suspectTabTxt.color = white;
        toolTabTxt.color = grey;
        motiveTabTxt.color = grey;
    }

    public void OnClickedToolTap()
    {
        popupArea.SetEvidenceType(1);
        popupArea.HidePopupArea();
        tabContentArea.SetTabTitleTxt("TOOL");
        tabContentArea.SetBtn("TOOL");
        suspectTabTxt.color = grey;
        toolTabTxt.color = white;
        motiveTabTxt.color = grey;
    }

    public void OnClickedMotiveTap()
    {
        popupArea.SetEvidenceType(2);
        popupArea.HidePopupArea();
        tabContentArea.SetTabTitleTxt("MOTIVE");
        tabContentArea.SetBtn("MOTIVE");
        suspectTabTxt.color = grey;
        toolTabTxt.color = grey;
        motiveTabTxt.color = white;
    }

    public void SetBattleTab()
    {
        battleManager = battleManager.gameObject.GetComponent<BattleManager>();
        suspectTabBtn.onClick.RemoveAllListeners();
        toolTabBtn.onClick.RemoveAllListeners();
        motiveTabBtn.onClick.RemoveAllListeners();

        suspectTabBtn.onClick.AddListener(battleManager.OnClickedSuspectTab);
        toolTabBtn.onClick.AddListener(battleManager.OnClickedToolTap);
        motiveTabBtn.onClick.AddListener(battleManager.OnClickedMotiveTap);
    }
}
