using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InformationPopupUIManager : MonoBehaviour
{
    
    public static InformationPopupUIManager instance = null;
    public GameObject reasonForCrimePopup;
    public GameObject informPopup;
    public TextMeshProUGUI informClueNameTMP;
    public TextMeshProUGUI informClueInformTMP;
    public Image informClueImage;
    public bool inventoryBtnClick;

    private void Awake() 
    {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            if(instance != this) {
                Destroy(this.gameObject);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        InformUIManager.instance.HidePopup(informPopup);
    }

    private void OnDisable() 
    {
        // 첫획득이면
        if(InvestigationManager.Instance.clueInfoData.GetIsFirstGet()){
            InvestigationManager.Instance.clueInfoData.ChangeIsFirstGet();  
                    Debug.Log("획득했다고 바꿈");
                    Debug.Log(InvestigationManager.Instance.clueInfoData.GetIsFirstGet());
            // 범행사유 창 띄우기
            if(InvestigationManager.Instance.hasKeyword && !inventoryBtnClick){
                reasonForCrimePopup.SetActive(true);
                ReasonForCrimeUIManager.instance.ShowReasonForCrimePopup();
            }
         }

    }
    public void ShowInformationPopup()
    {
        if(! InformUIManager.instance.discoverPopup.activeSelf){
            informClueNameTMP.GetComponent<TextMeshProUGUI>().text = InvestigationManager.Instance.clueName;
            if(InvestigationManager.Instance.hasKeyword) {
                ChangeKeywordColor();
            } else {
                informClueInformTMP.GetComponent<TextMeshProUGUI>().text = InvestigationManager.Instance.clueInformation;
            }
            informClueImage.GetComponent<Image>().sprite = InvestigationManager.Instance.clueSprite;
            informPopup.SetActive(true);
             InformUIManager.instance.inventoryCanvas.blocksRaycasts = false;
        } 
    }

     // 키워드만 따로 색넣는 함수
    public void ChangeKeywordColor()
    {
        string frontInform = InvestigationManager.Instance.clueInformation.Split('[')[0];
        string tmp = InvestigationManager.Instance.clueInformation.Split('[')[1];
        string backInform = tmp.Split(']')[1];
        string keyword = tmp.Split(']')[0];
        informClueInformTMP.GetComponent<TextMeshProUGUI>().text = frontInform + "<color=red>" + keyword + "</color>" + backInform;
    }
}
