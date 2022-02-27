using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    int nowTapID = 0; // 현재 탭 0: 인물, 1: 도구, 2: 범행사유, 3: 대화록
    int preTapID = 0; // 이전 탭 0: 인물, 1: 도구, 2: 범행사유, 3: 대화록
    public List<GameObject> characterClues = new List<GameObject>(); // 인물 단서 오브젝트들을 저장해둘 공간
    public List<GameObject> toolClues = new List<GameObject>(); // 도구 단서 오브젝트들을 저장해둘 공간
    int motivationClueCount;    // 동기 개수
    string[] motivationNames= {"유산", "장래희망", "생활비?", "스파이?"};
    string[] motivationContents = {// 이거 받아오면 되는거 아님? 받아와서 저장해두면 안되려나.. 아닌가.. 몰겠다...
        "그러고보니 박석지 씨는 유산에 대해 구 회장과 마찰이 잦았던 모양이다.", 
        "구 회장은 구나미 씨가 기업을 물려받기를 원했던 것 같은데, 의견차이가 꽤 컸던 모양이다.", 
        "구말종 씨는 생활비를 받아내러 계속 구회장을 귀찮게 굴었던 모양이다.", 
        "어떤 산업 스파이가 있었고, 구 회장은 아무래도 그 정체를 알아낸 모양인데… 이번 사건과 관련 있을까?"};
    GameObject inventoryCanvas;
    public GameObject invenCluePanel;   // 단서(인물, 도구, 동기) 패널
    public GameObject cluePanelContent; // 스크롤뷰 컨텐트
    public GameObject invenDialoguePanel;   // 대화록 패널
    public Button clueBtn, childBtns;   // 원본 버튼 프리팹, 동적 생성될 버튼
    public GameObject clueBtnObj;       // 원본 버튼 오브젝트
    
    public GameObject investigationCanvas, notAcquiredPopup;
    string tmpClueName, tmpClueInformation;
    Sprite tmpClueSprite;
    public Sprite notAcquiredSprite;


    void Awake()
    {
        CheckInventoryData.Instance.InitClueIsAcquiredData();
        // 변수 값 초기화
        //characterClues = GameObject.FindGameObjectsWithTag("Character");
        Debug.Log("인물개수");
        Debug.Log(characterClues.Count);
        for(int i = 0; i < characterClues.Count; i++) {
            Debug.Log(characterClues[i].gameObject.name);
            if(characterClues[i].GetComponent<ClueInfoData>().hasKeyword) motivationClueCount++;
        }

        Debug.Log("도구개수");
        Debug.Log(toolClues.Count);
        for(int i = 0; i < toolClues.Count; i++) {
            Debug.Log(toolClues[i].gameObject.name);
            if(toolClues[i].GetComponent<ClueInfoData>().hasKeyword) motivationClueCount++;
        }
        notAcquiredPopup.SetActive(false);
        UpdateInvenDisplay();
        inventoryCanvas = this.gameObject;
        inventoryCanvas.SetActive(false);
    }

    // 인벤토리 탭 변경 시 화면 병경하는 함수
    public void UpdateInvenDisplay()
    {
        switch(nowTapID) {
            case 0: {   // 인물 탭 화면 그리기
                invenDialoguePanel.SetActive(false);
                invenCluePanel.SetActive(true);
                Debug.Log("인물 클릭");
                // 버튼 동적 생성
                RemoveChildBtns();
                for(int i = 0; i < characterClues.Count; i++) {
                   CreateClueButtons(i);
                   //childBtns.GetComponent<Image>().sprite = characterClues[i].GetComponent<ClueInfoData>().clueSprite;
                   ChangeClueButtonsSprites(i);
                   childBtns.transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
                break;}

            case 1:{    // 도구 탭 화면 그리기
                invenDialoguePanel.SetActive(false);
                invenCluePanel.SetActive(true);
                Debug.Log("도구 클릭");
                // 버튼 동적 생성
                RemoveChildBtns();
                for(int i = 0; i < toolClues.Count; i++) {
                   CreateClueButtons(i);
                   //childBtns.GetComponent<Image>().sprite = toolClues[i].GetComponent<ClueInfoData>().clueSprite;
                   ChangeClueButtonsSprites(i);
                   childBtns.transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
                } 
                break;}

            case 2: {   // 동기 탭 화면 그리기
                invenDialoguePanel.SetActive(false);
                invenCluePanel.SetActive(true);
                Debug.Log("동기 클릭");
                // 버튼 동적 생성
                RemoveChildBtns();
                for(int i = 0; i < motivationClueCount; i++) {
                    CreateClueButtons(i);
                    ChangeClueButtonsSprites(i);
                   
                }
                break;}

            case 3: {   // 대화록 탭 화면 그리기
                invenCluePanel.SetActive(false);
                invenDialoguePanel.SetActive(true);
                Debug.Log("대화록 클릭");
                break;}
        }
    }

    // 버튼 동적 생성 함수
    private void CreateClueButtons(int clueIdx)
    {
        clueBtnObj.SetActive(true);
        childBtns = Instantiate(clueBtn);
        childBtns.transform.SetParent(cluePanelContent.transform);

        childBtns.GetComponent<Button>().onClick.AddListener(()=>OnClickedClues(clueIdx));
        childBtns.onClick.AddListener(() => StartCoroutine(ShowNotAcquiredPopup(clueIdx)));

        // 버튼 위치, 크기 조정
        Vector3 nowChildBtnPos = childBtns.transform.position;
        //Debug.Log(nowChildBtnPos.z);
        nowChildBtnPos.z = 0f;
        //Debug.Log(nowChildBtnPos.z);
        childBtns.transform.localPosition = nowChildBtnPos;
        childBtns.transform.localScale = new Vector3(1,1,1);
        
        
    }

    private void ChangeClueButtonsSprites(int clueIdx)
    {
        CheckInventoryData checkInventoryData = this.gameObject.GetComponent<CheckInventoryData>();
        // 단서를 획득했으면 단서이미지 띄우기
        if(checkInventoryData.GetClueISAcquired(nowTapID, clueIdx)){
            switch(nowTapID) {
                case 0: { // 인물이미지 
                    childBtns.GetComponent<Image>().sprite = characterClues[clueIdx].GetComponent<ClueInfoData>().clueSprite; 
                    break;
                }
                case 1: { // 도구이미지
                    childBtns.GetComponent<Image>().sprite = toolClues[clueIdx].GetComponent<ClueInfoData>().clueSprite; 
                    break;
                }
                case 2: { // 범행사유는 이미지가 아직 정해진게 없음
                     childBtns.transform.GetComponentInChildren<TextMeshProUGUI>().text = motivationNames[clueIdx];
                     break;
                }
            }            
        } else { // 단서를 획득하지 못했으면 ?이미지 띄우기
            childBtns.GetComponent<Image>().sprite = notAcquiredSprite;
            childBtns.transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
            
        
    }

    // 동적 생성된 버튼 제거 함수
    private void RemoveChildBtns()
    {
        Transform[] childList = cluePanelContent.GetComponentsInChildren<Transform>(true);
        if(childList != null) {
            for(int i = 1; i < childList.Length; i++) {
                if(childList[i] != transform)
                Destroy(childList[i].gameObject);
            }
        }
    }


    // 인물 탭을 클릭
    public void OnClickedCharacterTap()
    {
        nowTapID = 0;
        UpdateInvenDisplay();
    }

    // 도구 탭 클릭
    public void OnClickedToolTap()
    {
        nowTapID = 1;
        UpdateInvenDisplay();
    }

    // 동기 탭 클릭
    public void OnClickedMotivationTap()
    {
        nowTapID = 2;
        UpdateInvenDisplay();
    }

    // 대화록 탭 클릭
    public void OnClickedDialogueTap()
    {
        nowTapID = 3;
        UpdateInvenDisplay();
    }

    // 단서 버튼 클릭했을 때 정보창 띄우기
    public void OnClickedClues(int clueIdx)
    {
        //CheckInventoryData checkInventoryData = this.GetComponent<CheckInventoryData>();
        if(CheckInventoryData.Instance.GetClueISAcquired(nowTapID, clueIdx)){     // 단서를 획득하면 정보창 출력
            switch(nowTapID) {
                case 0: {       // 인물탭
                    ChangeInvestManagerVariable(clueIdx, characterClues); 
                   // informUIManager.ShowInformationPopup();
                   // RestorationInvestManagerVariable();
                    break;}
                case 1:{        // 도구탭
                    ChangeInvestManagerVariable(clueIdx, toolClues);
                    InformationPopupUIManager.instance.inventoryBtnClick = true;
                    break;}
                case 2: {       // 범행사유탭
                    ChangeInvestManagerVariable(clueIdx, characterClues);
                    break;}
                case 3: {       // 대화록탭
                 // 프로토타입 나오면 하쟈
                    break;}
            }
            if(nowTapID != 2) {
                InformationPopupUIManager.instance.ShowInformationPopup();
            } else /*if (nowTapID == 2) */ReasonForCrimeUIManager.instance.ShowReasonForCrimePopup();
            //InformUIManager.instance.HidePopup(ReasonForCrimeUIManager.instance.reasonForCrimePopup);
        } else {
            StartCoroutine("ShowNotAcquiredPopup", clueIdx);
        }
        
    }

    // InvestigationManager에 있던 정보 임시 변수에 옮기고, 현재 클릭한 단서 정보로 변경
    private void ChangeInvestManagerVariable(int index, List<GameObject> clues)
    {
        tmpClueName = InvestigationManager.Instance.clueName;
        tmpClueInformation = InvestigationManager.Instance.clueInformation;
        tmpClueSprite = InvestigationManager.Instance.clueSprite;

        if(nowTapID == 0 || nowTapID == 1) {
            InvestigationManager.Instance.clueName = clues[index].GetComponent<ClueInfoData>().clueName;
            InvestigationManager.Instance.clueInformation = clues[index].GetComponent<ClueInfoData>().clueInformation;
            InvestigationManager.Instance.clueSprite = clues[index].GetComponent<ClueInfoData>().clueSprite;
        }
        else{
            InvestigationManager.Instance.clueName = motivationNames[index];
            InvestigationManager.Instance.clueInformation = motivationContents[index];
            InvestigationManager.Instance.clueSprite = clues[index].GetComponent<ClueInfoData>().clueSprite;
        }
    }

    // InvestigationManager에 있던 정보 원상 복구
    private void RestorationInvestManagerVariable()
    {
        InvestigationManager.Instance.clueName = tmpClueName;
        InvestigationManager.Instance.clueInformation = tmpClueInformation;
        InvestigationManager.Instance.clueSprite = tmpClueSprite;
    }

    // 미획득 알림창 출력
    private IEnumerator ShowNotAcquiredPopup(int clueIdx) 
    {
        //CheckInventoryData checkInventoryData = this.GetComponent<CheckInventoryData>();
        if(!CheckInventoryData.Instance.GetClueISAcquired(nowTapID, clueIdx)){ // 미획득 알림창 출력
            Debug.Log("획득 안함!");
            WaitForSeconds waitForSeconds = new WaitForSeconds(2.0f);
            notAcquiredPopup.SetActive(true);
            yield return waitForSeconds;
            notAcquiredPopup.SetActive(false);
        }

    }
    

}

