using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class InvenDialogueUIManager : MonoBehaviour
{
    public List<TMP_Dropdown> dropdowns = new List<TMP_Dropdown>();
    public List<GameObject> dropdownObjs = new List<GameObject>();
    //public List<TMP_Text> captionTxt = new List<TMP_Text>();
    List<bool> isOpen = new List<bool>();
    bool isChoosed = false;
    //public List<TMP_Text> captionTexts = new List<TMP_Text>();
    int chooseNum, preDropdownNum, clickedDropdownNum = 0;
    public GameObject dialogueContentPanel, invenBackButton;
    
    // 대화록 출력 관련 변수
    InvenDialogueSheetData invenDialogueSheetData;
    public TextMeshProUGUI dialoguegContentTxt;
    
    void Awake() 
    {
        invenDialogueSheetData = this.GetComponent<InvenDialogueSheetData>();
        /*
        dropdowns[0].captionText.text = "박석지";
        dropdowns[1].captionText.text  = "구나미";
        dropdowns[2].captionText.text  = "구말종";
        dropdowns[3].captionText.text  = "강건오";*/
        // 드롭다운 label변경
        dropdowns[0].captionText.SetText("박석지");
        Debug.Log("드롭다운 캡션이 안바뀜!!");
        Debug.Log(dropdowns[0].captionText.text);
        

        clickedDropdownNum = 0; 
        dialogueContentPanel.SetActive(false);
        
        for(int i = 0; i < 4; i++)   {
            int temp = i;
            isOpen.Add(false);
            dropdowns[temp].onValueChanged.AddListener(delegate{
                ShowDialogueContent(dropdowns[temp]);
        });
        }
    }

    private void Update() 
    {
        GameObject obj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if(Input.GetMouseButtonDown(0)){
            ChangeDropDwonNum(obj);
            ClickedDropDown();
        }
    }

    public void ShowDialogueContent(TMP_Dropdown clickedDropdown)
    {
        Debug.Log("선택지선택");
        Debug.Log(clickedDropdown);
        chooseNum = dropdowns[clickedDropdownNum].value;
        Debug.Log(chooseNum);
        StartCoroutine("InvenDialogNetConnect");
        dialogueContentPanel.SetActive(true);
        invenBackButton.SetActive(false);
        InitDropDownPosition();
        clickedDropdownNum = 4;
    }

    public void IsNotOpen()
    {
        for(int i = 0; i < dropdowns.Count; i++)   {
            isOpen[i] = false;
        }
    }

    void ChangeDropDwonNum(GameObject ClickedDropdown)
    {
        if(ClickedDropdown != null){ 
            switch(ClickedDropdown.name)
                {
                    case "Dropdown_Character1": clickedDropdownNum = 0; break;
                    case "Dropdown_Character2": clickedDropdownNum = 1; break;
                    case "Dropdown_Character3": clickedDropdownNum = 2; break;
                    case "Dropdown_Character4": clickedDropdownNum = 3; break;
                    //default: clickedDropdownNum = 0; break;
                }
            Debug.Log("현재 클릭한 드롭다운 번호");
            Debug.Log(clickedDropdownNum);
        } else {
            clickedDropdownNum = 4; 
        }
    }

    void ClickedDropDown()
    {
        if(clickedDropdownNum == 4) {
            Debug.Log("위치초기화");
            InitDropDownPosition();
            IsNotOpen();
            return;
        }
        if(clickedDropdownNum!=4 && !dropdowns[clickedDropdownNum].IsExpanded) {
            switch(clickedDropdownNum) {
                case 0: {
                    MoveDropDownPosition(538);
                    break;
                }
                case 1: {
                    MoveDropDownPosition(176);
                    break;
                }
                case 2: {
                    MoveDropDownPosition(-186);
                    break;
                }
                default: {
                    InitDropDownPosition(); 
                    break;
                }
            }
        } else  if(dropdowns[clickedDropdownNum].IsExpanded)
        {
            Debug.Log("열려있던거 닫기");
            InitDropDownPosition();
        }
    }
    public void InitDropDownPosition()
    {
        dropdownObjs[0].transform.localPosition = new Vector3(0,890,0);
        dropdownObjs[1].transform.localPosition = new Vector3(0,538,0);
        dropdownObjs[2].transform.localPosition = new Vector3(0,176,0);
        dropdownObjs[3].transform.localPosition = new Vector3(0,-186,0);
    }

    void MoveDropDownPosition(int condition)
    {
        if(dropdownObjs[clickedDropdownNum+1].transform.localPosition.y == condition){
            for(int i = clickedDropdownNum+1; i <dropdowns.Count; i++) {
                dropdownObjs[i].transform.localPosition -= new Vector3(0, 600, 0);
            }
        }
    }
    
    public void UpdataeDropDownOption(int characterID, int choiceNum)
    {
        Debug.Log("이벤토리의 드롭다운 옵션으를 업데이트 합니다.");
        if(CheckInventoryData.Instance.GetClueISAcquired(3, characterID*3 + choiceNum)){
            Debug.Log("대화를 획득하였으므로 옵션의 글자를 바꿉니다.");
            dropdowns[characterID].options[choiceNum].text = ChatData.Instance.requiredDatas[characterID][choiceNum].choiceTitle;
        } else {
            Debug.Log("아직 대화를 획득하지 못했습니다.");
            dropdowns[characterID].options[choiceNum].text = "?";
        }
    }

    // 대화록 출력 관련 함수
    IEnumerator InvenDialogNetConnect() 
    {
        string dialogTableAddress = invenDialogueSheetData.dialogTableAddress;
        string sheetNum = invenDialogueSheetData.sheetNum[clickedDropdownNum][chooseNum];
        string range = invenDialogueSheetData.range[clickedDropdownNum][chooseNum];

        int dropdownChoiceNum = clickedDropdownNum*3 + chooseNum;
        string newData = "";
        Debug.Log(clickedDropdownNum);
        Debug.Log(chooseNum);
        Debug.Log(dropdownChoiceNum);
        if(!CheckInventoryData.Instance.GetClueISAcquired(3, dropdownChoiceNum)) {
            newData = "아직 획득하지 못한 대화록입니다.";
            ShowDialogueData(newData);
        } else {
            ShowDialogueData("대화를 불러오는 중...");
            string URL = dialogTableAddress + "/export?format=tsv&gid=" + sheetNum + "&range=B2:" + range;
            UnityWebRequest www = UnityWebRequest.Get(URL);
            yield return www.SendWebRequest();

            string dialogueData = www.downloadHandler.text;
            ParsingDialogueData(dialogueData);
        }
    }

    public void ParsingDialogueData(string data)
    {        
        string[] split_text = data.Split('\n');
        string characterName="", dialogTxt="";
        string newData = "";
        for(int i = 0; i < split_text.Length; i++) {
            characterName = split_text[i].Split('\t')[0];
            dialogTxt = split_text[i].Split('\t')[1];
            newData += characterName + ": " + dialogTxt + "\n";
        }
        ShowDialogueData(newData);
            
    }

    public void ShowDialogueData(string data) 
    {
         dialoguegContentTxt.text = data;
    }

    
    
}
