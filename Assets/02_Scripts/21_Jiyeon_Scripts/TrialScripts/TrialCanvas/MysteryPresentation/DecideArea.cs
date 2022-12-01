using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecideArea : MonoBehaviour
{
    [SerializeField] MysteryPresentationMng mysteryPresentationMng;
    [SerializeField] TabContentArea tabContentArea;
    [SerializeField] PopupArea popupArea;
    [SerializeField] GameObject mysteryPresentCanvas;
    [SerializeField] List<Image> selectedImages = new List<Image>();
    [SerializeField] List<Text> selectedNames = new List<Text>();
    [SerializeField] List<Text> selectedReactions = new List<Text>();
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
        SetSelectedReactions();
        this.gameObject.SetActive(true);
    }   

    void SetSelectedImages()
    {
        selectedImages[0].sprite = tabContentArea.suspectSprites[mysteryPresentationMng.suspectNum];
        selectedImages[1].sprite = tabContentArea.toolSprites[mysteryPresentationMng.toolNum];
        selectedImages[2].sprite = tabContentArea.motiveSprites[mysteryPresentationMng.motiveNum];
    }

    void SetSelectedNames()
    {
        selectedNames[0].text = tabContentArea.suspectNames[mysteryPresentationMng.suspectNum];
        Debug.Log("범인명: " + tabContentArea.suspectNames[mysteryPresentationMng.suspectNum]);
        selectedNames[1].text = tabContentArea.toolNames[mysteryPresentationMng.toolNum];
        Debug.Log("흉기명: " + tabContentArea.toolNames[mysteryPresentationMng.toolNum]);
        selectedNames[2].text = tabContentArea.motiveNames[mysteryPresentationMng.motiveNum];
        Debug.Log("동기명: " + tabContentArea.motiveNames[mysteryPresentationMng.motiveNum]);
    }

    void SetSelectedReactions()
    {
        selectedReactions[0].text = popupArea.suspectReaction[mysteryPresentationMng.suspectNum];
        selectedReactions[1].text = popupArea.toolReaction[mysteryPresentationMng.toolNum];
        selectedReactions[2].text = popupArea.motiveReaction[mysteryPresentationMng.motiveNum];
    }

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
