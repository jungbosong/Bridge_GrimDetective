using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DecideArea : MonoBehaviour
{
    [SerializeField] MysteryPresentationMng mysteryPresentationMng;
    [SerializeField] TabContentArea tabContentArea;
    [SerializeField] PopupArea popupArea;
    [SerializeField] GameObject mysteryPresentCanvas;
    [SerializeField] List<Image> selectedImages = new List<Image>();
    [SerializeField] List<TextMeshProUGUI> selectedNames = new List<TextMeshProUGUI>();
    //[SerializeField] List<TextMeshProUGUI> selectedReactions = new List<TextMeshProUGUI>();
    [SerializeField] Button sureBtn, notSureBtn;

    void Awake() 
    {
        mysteryPresentationMng = mysteryPresentationMng.gameObject.GetComponent<MysteryPresentationMng>();
        tabContentArea = tabContentArea.gameObject.GetComponent<TabContentArea>();
        popupArea = popupArea.gameObject.GetComponent<PopupArea>();
        sureBtn.onClick.AddListener(OnClickedSureBtn);
        notSureBtn.onClick.AddListener(OnClickedNotSureBtn);
        this.gameObject.SetActive(false);
    }

    public void ShowDecideArea()
    {
        SetSelectedImages();
        SetSelectedNames();
        //SetSelectedReactions();
        this.gameObject.SetActive(true);
    }   

    void SetSelectedImages()
    {
        selectedImages[0].sprite = tabContentArea.suspectSprites[mysteryPresentationMng.suspectNum];
        selectedImages[1].sprite = tabContentArea.toolSprites[mysteryPresentationMng.weaponNum];
        //selectedImages[2].sprite = tabContentArea.motiveSprites[mysteryPresentationMng.motiveNum];
    }

    void SetSelectedNames()
    {
        selectedNames[0].text = tabContentArea.suspectNames[mysteryPresentationMng.suspectNum];
        Debug.Log("범인명: " + tabContentArea.suspectNames[mysteryPresentationMng.suspectNum]);
        selectedNames[1].text = tabContentArea.toolNames[mysteryPresentationMng.weaponNum];
        Debug.Log("흉기명: " + tabContentArea.toolNames[mysteryPresentationMng.weaponNum]);
        /*selectedNames[2].text = tabContentArea.motiveNames[mysteryPresentationMng.motiveNum];
        Debug.Log("동기명: " + tabContentArea.motiveNames[mysteryPresentationMng.motiveNum]);*/
    }

    /*void SetSelectedReactions()
    {
        selectedReactions[0].text = popupArea.suspectReaction[mysteryPresentationMng.suspectNum];
        selectedReactions[1].text = popupArea.toolReaction[mysteryPresentationMng.weaponNum];
        selectedReactions[2].text = popupArea.motiveReaction[mysteryPresentationMng.motiveNum];
    }*/

    void OnClickedSureBtn()
    {
        this.gameObject.SetActive(false);
        mysteryPresentCanvas.SetActive(false);
    }

    void OnClickedNotSureBtn()
    {
        this.gameObject.SetActive(false);
    }
}
