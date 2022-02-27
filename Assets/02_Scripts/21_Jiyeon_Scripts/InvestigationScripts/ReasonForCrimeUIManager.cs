using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReasonForCrimeUIManager : MonoBehaviour
{
    public static ReasonForCrimeUIManager instance = null;
    // 범행사유 팝업창 관련 변수
    public GameObject reasonForCrimePopup;
    public TextMeshProUGUI keywordTMP;
    public TextMeshProUGUI reasonForCrimeTMP;
    public bool doneReasonForCrime = false;
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
        InformUIManager.instance.HidePopup(reasonForCrimePopup);
    }

    // 범행사유 창 추가
    public void ShowReasonForCrimePopup() 
    {
        Debug.Log("정보창 닫았어?");
        //WaitForSeconds waitForSeconds = new WaitForSeconds(4.0f);
       
            Debug.Log("내범행동기는 말이야");
        
            keywordTMP.GetComponent<TextMeshProUGUI>().text = InvestigationManager.Instance.keyword;
            reasonForCrimeTMP.GetComponent<TextMeshProUGUI>().text = InvestigationManager.Instance.reasonForCrime;
            reasonForCrimePopup.SetActive(true);   
        //yield return waitForSeconds;  
    }
}
