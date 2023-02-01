using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] GameObject settingPopup;
    [SerializeField] GameObject settingButton;
    Image settingBtnImage;
    [SerializeField] Sprite settingBtnSprite;
    [SerializeField] Sprite closeBtnSprite;
    bool isClicked = true;

    void Awake() 
    {
        settingBtnImage = settingButton.GetComponent<Image>();
        settingPopup.SetActive(false);
    }
    
    public void OnClickedSetting()
    {
        // 세팅 버튼이 클릭된 경우
        if(isClicked) 
        {
            settingPopup.SetActive(true);
            settingBtnImage.sprite = closeBtnSprite;
            isClicked = false;
        }
        // 세팅 버튼이 클릭되지 않은 경우
        else 
        {
            settingPopup.SetActive(false);
            settingBtnImage.sprite = settingBtnSprite;
            isClicked = true;
        }
    }
}
