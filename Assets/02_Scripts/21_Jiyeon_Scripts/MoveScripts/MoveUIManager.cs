using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveUIManager : MonoBehaviour
{
    // 층이동 변수
    [SerializeField] private bool checkFloor = false;
    [SerializeField] public int maxFloor, minFloor;
    [SerializeField] private int nowFloorIdx;
    [SerializeField] private int canMoveRoom;

    // 방이동 변수
    [SerializeField] private bool checkRoom = false;
    [SerializeField] public int maxRoom, minRoom;
    [SerializeField] private int nowRoomIdx;
    [SerializeField] private int canMoveFloor;
    [SerializeField] private GameObject roomCanvasesParent;

     public GameObject[,] roomCanvases;

    // 버튼 UI 관리
    private GameObject btnParents;
    private GameObject[] btnChilds = new GameObject[4];
    // private Button[] buttons;

    // Panel UI관리
    [SerializeField] CanvasGroup swipeCanvasGroup;
    public GameObject positionPanel;
    private Text position;
    private int pointerID;
    

    void Awake() 
    {
        roomCanvases = new GameObject[maxFloor, maxRoom];
    }    
    // 변수초기화 
    void Start()
    {
        btnParents = GameObject.Find("MoveButtonGroup");

        for(int i = 0; i < btnParents.transform.childCount; i++)
        {
            btnChilds[i] = btnParents.transform.GetChild(i).gameObject;
            btnChilds[i].SetActive(false);
        }
        positionPanel = GameObject.Find("PositionPanel").gameObject;
        position = GameObject.Find("PositionText").GetComponent<Text>();
        positionPanel.SetActive(false);
        //InvisileRoom();
        //VisibleRoom();
    }

    void Update()
    {
        for(int i = 0; i < btnParents.transform.childCount; i++){   
            btnChilds[i].SetActive(false);
        }
        SetCheckRoomFloor();
        SetMoveUI();
        HidePosition();
    }

    public void SetMoveUI()
    {
        if(checkRoom){
            btnChilds[0].SetActive(checkRoom);
            btnChilds[1].SetActive(checkRoom);
            if(nowRoomIdx == minRoom) {btnChilds[0].SetActive(!checkRoom);}
            if(nowRoomIdx == maxRoom) {btnChilds[1].SetActive(!checkRoom);}
        }
        if(checkFloor){
            btnChilds[2].SetActive(checkFloor);
            btnChilds[3].SetActive(checkFloor);
            if(nowFloorIdx == maxFloor) { btnChilds[2].SetActive(!checkFloor);}
            if (nowFloorIdx == minFloor) { btnChilds[3].SetActive(!checkFloor);}
        }
    }

    public void SetCheckRoomFloor()
    {
        if(nowRoomIdx == canMoveFloor) checkFloor = true;
        if(nowRoomIdx != canMoveFloor) checkFloor = false;
        if(nowFloorIdx == canMoveRoom) checkRoom = true; 
        if(nowFloorIdx != canMoveRoom) checkRoom = false; 
    }
    

    public void ShowPosition()
    {
        positionPanel.SetActive(true);
        position.text = "현재 방:" + nowFloorIdx + "층" + nowRoomIdx + "번째 방";
        swipeCanvasGroup.blocksRaycasts = false;
    }

    public void HidePosition()
    {
        #if UNITY_EDITOR   // PC나 유니티 에디터일 때
            pointerID = -1;
        #elif UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID    // 휴대폰이나 이외에서 터치할 때
            pointerID = 0;
        #endif

        if((Input.GetMouseButtonDown(0))){
            //print(1);
            if(EventSystem.current.IsPointerOverGameObject(pointerID) == false){ // UI 외부를 터치했다면
            //print(2);
                positionPanel.SetActive(false);
                swipeCanvasGroup.blocksRaycasts = true;
                //print(3);
            } else return;
        }
    }

    public void VisibleRoom()
    {
        roomCanvases[nowFloorIdx-1, nowRoomIdx-1].SetActive(true);
    }

    public void InvisileRoom()
    {
        for(int i = 0; i < maxFloor; i++) {
            for(int j = 0; j < maxRoom; j++) {
                if(roomCanvases[i,j] != null)
                roomCanvases[i, j].SetActive(false);
            }
        }
    }
  
    // 버튼 클릭 했을 때 실행되는 함수들
    public void OnClickedLeftButton()
    {
        nowRoomIdx--;
        ShowPosition();
        InvisileRoom();
        VisibleRoom();
    }
    public void OnClickedRightButton()
    {
        nowRoomIdx++;
        ShowPosition();
        InvisileRoom();
        VisibleRoom();
    }
    public void OnClickedUpButton()
    {
        nowFloorIdx++;
        ShowPosition();
        InvisileRoom();
        VisibleRoom();
    }
    public void OnClickedDownButton()
    {
        nowFloorIdx--;
        ShowPosition();
        InvisileRoom();
        VisibleRoom();
    }
}
