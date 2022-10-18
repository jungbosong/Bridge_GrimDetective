using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DB연결에 필요한 데이터들
public class RequiredData
{
    public int choiceNum;      // 선택지 번호
    public string choiceTitle; // 선택지 제목(내용)
    public string sheetNum;    // 해당 대화록 시트번호
    public string range;       // 해당 대화록이 적힌 범위
}

public class ChatData : MonoBehaviour
{
    private static ChatData instance = null;
    // 대화록 링크
    public string dialogueDBLink;
    // DB연결에 필요한 데이터들
    public List<List<RequiredData>> requiredDatas = new List<List<RequiredData>>();
    
    // 싱글톤 제작 함수
    public static ChatData Instance {
        get{
            if(instance == null){
                var obj = FindObjectOfType<ChatData>();
                if(obj != null) {
                    instance = obj;
                } else {
                    var newObj = new GameObject().AddComponent<ChatData>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }

    private void Awake() 
    {
        var objs = FindObjectsOfType<ChatData>();
        if(objs.Length != 1) {
            Debug.Log("ChatData 여러개 찾음");
            Debug.Log(gameObject.name);
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Debug.Log(gameObject.name);

        InitRequiredDatas();
    }

    private void InitRequiredDatas()
    {
        for(int i =0; i<4; i++) {
            requiredDatas.Add(new List<RequiredData>());
        }
        // 박석지 대화록 주소 & 범위
        requiredDatas[0].Add(new RequiredData(){choiceNum = 0, choiceTitle = "남편", sheetNum = "1741567521", range = "B2:C17"});
        requiredDatas[0].Add(new RequiredData(){choiceNum = 1, choiceTitle = "근거없는 의심", sheetNum = "1242962773", range = "B2:C16"});
        requiredDatas[0].Add(new RequiredData(){choiceNum = 2, choiceTitle = "재떨이", sheetNum = "1930158425", range = "B2:C23"});

        // 구나미 대화록 주소 & 범위
        requiredDatas[1].Add(new RequiredData(){choiceNum = 0, choiceTitle = "딸", sheetNum = "931017955", range = "B2:C17"});
        requiredDatas[1].Add(new RequiredData(){choiceNum = 1, choiceTitle = "인간말종", sheetNum = "1227269045", range = "B2:C15"});
        requiredDatas[1].Add(new RequiredData(){choiceNum = 2, choiceTitle = "나미의 꿈", sheetNum = "1148838866", range = "B2:C21"});

        // 구말종 대화록 주소 & 범위
        requiredDatas[2].Add(new RequiredData(){choiceNum = 0, choiceTitle = "조카", sheetNum = "1158960319", range = "B2:C11"});
        requiredDatas[2].Add(new RequiredData(){choiceNum = 1, choiceTitle = "말종의 큰 그림", sheetNum = "18308619", range = "B2:C15"});
        requiredDatas[2].Add(new RequiredData(){choiceNum = 2, choiceTitle = "나 바쁘다", sheetNum = "1950443525", range = "B2:C15"});

        // 강건오 대화록 주소 & 범위
        requiredDatas[3].Add(new RequiredData(){choiceNum = 0, choiceTitle = "...애인", sheetNum = "976158909", range = "B2:C10"});
        requiredDatas[3].Add(new RequiredData(){choiceNum = 1, choiceTitle = "아몬드쿠키", sheetNum = "1567995307", range = "B2:C19"});
        requiredDatas[3].Add(new RequiredData(){choiceNum = 2, choiceTitle = "깔끔", sheetNum = "1909111278", range = "B2:C18"});
       
    }


}
