using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestigationManager : MonoBehaviour
{
    private static InvestigationManager instance = null;
    public ClueInfoData clueInfoData;
    //public CheckInventoryData checkInventoryData;
    public string clueName;         // 단서 이름
    public Sprite clueSprite;       // 단서 사진
    public string clueInformation;  // 단서 정보
    public bool isFirstGet;         // 첫획득 여부
    public bool hasKeyword;         // 키워드 여부
    public bool isCharacter;        // 캐릭터인가 도구인가
    public string keyword;
    public string reasonForCrime;   // 범행사유
    
    public static InvestigationManager Instance {
        get{
            if(instance == null){
                var obj = FindObjectOfType<InvestigationManager>();
                if(obj != null) {
                    instance = obj;
                } else {
                    var newObj = new GameObject().AddComponent<InvestigationManager>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }
    void Awake()
    {
        var objs = FindObjectsOfType<InvestigationManager>();
        if(objs.Length != 1) {
            Debug.Log("InvestigationManager에서 여러개 찾음");
            Debug.Log(gameObject.name);
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Debug.Log(gameObject.name);
        //checkInventoryData = gameObject.GetComponent<CheckInventoryData>();
    }
    public void OnClickTest()
    {
        Debug.Log(clueInfoData.clueName);
    }
    private void GetClueData(string clueID)
    {
        string[] tmp = clueID.Split(' ');
        string roomNum = tmp[0];
        string clueObjName = tmp[1];
        GameObject clueDataParent = GameObject.FindWithTag(roomNum);
        clueInfoData = clueDataParent.transform.Find(clueObjName).GetComponent<ClueInfoData>();
        clueName = clueInfoData.clueName;
        Debug.Log(clueName);
        clueSprite = clueInfoData.clueSprite;
        clueInformation = clueInfoData.clueInformation;
        hasKeyword = clueInfoData.hasKeyword;
        isCharacter = clueInfoData.isCharacter;
        reasonForCrime = clueInfoData.reasonForCrime;
        keyword = clueInfoData.keyword;
    
    }

// 인물 or 사물인지 확인
    public void CheckClueIsCharacter(string clueID)
    {
        GetClueData(clueID);
        // 인물일 경우
        if(clueInfoData.isCharacter == true){
            //첫조사여부 확인
            if(clueInfoData.GetIsFirstGet()){
                Debug.Log("인물이고, 첨 획득");
                Debug.Log(clueInfoData.GetIsFirstGet());
                
            // ㅇㅇ     기본 선택지만 활성화 된 선택창 (진)
            //               기본 선택지 터치 -> 대화 출력, 종료 (진) 
            //               -> ~를 획득햇다 + 인물 정보창 팝업
            //    StartCoroutine(InformUIManager.instance.ShowDiscoverPopup());
            //               -> 인벤에 새로운 대화록 추가 + 안내 팝업
            //               -> 키워드 포함?
                CheckHasKeyword();
            } else{
                Debug.Log("인물이고, 첨 획득X");
                Debug.Log(clueInfoData.GetIsFirstGet());
                 // ㄴㄴ     기본 선택지 이외의 선택지 활성화 조건 만족함? (진)
    //         ㅇㅇ 선택지 활성화 => 대화 출력, 종료 (진)
    //          -> 이전에 조사한 대화?(진)
    //                 ㅇㅇ      인물 조사 종료
    //                  ㄴㄴ     인벤에 대화록 추가 + 안내 팝업
    //                               키워드 포함?
                //CheckHasKeyword();
                InformationPopupUIManager.instance.ShowInformationPopup();
    //          ㄴㄴ 기본 선택지만 터치 가능 -> 대화출력, 종료(진)
    //          -> 인물 조사 종료
            }
   
    
        } // 사물일경우
        else{
            // 첫조사여부 확인
            if(clueInfoData.GetIsFirstGet()){
                Debug.Log("사물이고, 첨 획득");
                Debug.Log(clueInfoData.GetIsFirstGet());
                // ㅇㅇ ~획득함 팝업 +정보창 팝업 + 범행사유 팝업
                StartCoroutine(InformUIManager.instance.ShowDiscoverPopup());                 
                //-> 사물 조사 종료
            }
            else if(!clueInfoData.GetIsFirstGet()){
                Debug.Log("사물이고, 첨 획득X");
                Debug.Log(clueInfoData.GetIsFirstGet());
                  // ㄴㄴ 도구 정보창 팝업 
                InformationPopupUIManager.instance.ShowInformationPopup();
             
             //           ㄴㄴ 사물 조사 종료  
            }
          
        }
    }
    // 키워드 포함 여부 확인
    public bool CheckHasKeyword()
    {
        if(clueInfoData.hasKeyword){
            // 범행사유 정보창 팝업 -> 인물 조사 종료
            //informUIManager.ShowReasonForCrimePopup();
            return true;
        } else return false;
    }
}
