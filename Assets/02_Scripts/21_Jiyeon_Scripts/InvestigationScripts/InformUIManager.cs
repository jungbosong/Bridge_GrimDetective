using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InformUIManager : MonoBehaviour
{
    public static InformUIManager instance = null;
    private int pointerID;
    public CanvasGroup inventoryCanvas;
    // 발견 팝업창 관련 변수
    public GameObject discoverPopup;
    public TextMeshProUGUI discoverClueNameTMP;
    public Image discoverClueImage;
    public GameObject   informPopup;
    public GameObject chatPanel;
 
    
    
    
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
    
    public void CallShowDiscoverPopup()
    {
        if(InvestigationManager.Instance.clueInfoData.GetIsFirstGet()){
            StartCoroutine("ShowDiscoverPopup");
        } else return;
    }

    public IEnumerator ShowDiscoverPopup()
    {
        if(chatPanel.activeSelf){
            WaitForSeconds waitForSeconds = new WaitForSeconds(2.0f);
            discoverClueNameTMP.GetComponent<TextMeshProUGUI>().text = InvestigationManager.Instance.clueName;
            discoverClueImage.GetComponent<Image>().sprite = InvestigationManager.Instance.clueSprite;
            discoverPopup.SetActive(true);
            yield return waitForSeconds;
            discoverPopup.SetActive(false);
            informPopup.SetActive(true);
            InformationPopupUIManager.instance.ShowInformationPopup();
            InvestigationManager.Instance.clueInfoData.ChangeIsFirstGet();
        }
    }
   
    // 팝업창 숨기는 함수
     public void HidePopup(GameObject popup)
    {
        #if UNITY_EDITOR   // PC나 유니티 에디터일 때
            pointerID = -1;
        #elif UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID    // 휴대폰이나 이외에서 터치할 때
            pointerID = 0;
        #endif

        Vector2 mousePosition;
        float[] outsidePopupArea = {
            298f,     // 왼쪽 영역
            1133f,     // 오른쪽 영역
            2064f,     // 위
            541f      // 아래
        };
        
        if((Input.GetMouseButtonDown(0))){
            mousePosition = Input.mousePosition;
            if(mousePosition.x <= outsidePopupArea[0] || mousePosition.x >= outsidePopupArea[1] || mousePosition.y >= outsidePopupArea[2] || mousePosition.y <= outsidePopupArea[3])
            {
                popup.SetActive(false);
                inventoryCanvas.blocksRaycasts = true;
            }
            else return;
        }
    }

    
    
}
