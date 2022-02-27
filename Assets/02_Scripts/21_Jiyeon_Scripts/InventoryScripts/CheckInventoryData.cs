using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInventoryData: MonoBehaviour
{
    private static CheckInventoryData instance;
    public static CheckInventoryData Instance {
        get{
            if(instance == null){
                var obj = FindObjectOfType<CheckInventoryData>();
                if(obj != null) {
                    instance = obj;
                } else {
                    var newObj = new GameObject().AddComponent<CheckInventoryData>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }

    private void Awake() 
    {
        var objs = FindObjectsOfType<CheckInventoryData>();
        if(objs.Length != 1) {
            Debug.Log("CheckInventoryData에서 여러개 찾음");
            Debug.Log(gameObject.name);
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
         Debug.Log(gameObject.name);
        InitClueIsAcquiredData();
    }
    public  List<List<bool>> clueIsAcquired = new List<List<bool>>();  // 인물, 도구, 범행사유, 대화록 단서 획득 여부
    
    public void InitClueIsAcquiredData()
    {
        Debug.Log("단서획득여부초기화");
        // 에피소드1 단서획득 여부 데이터 미획득으로 초기화
        List<bool> tmpList = new List<bool>();
        clueIsAcquired.Add(new List<bool>{false, false, false, false});
        clueIsAcquired.Add(new List<bool>{false, false, false, false, false});
        clueIsAcquired.Add(new List<bool>{false, false, false, false});
        clueIsAcquired.Add(new List<bool>{
            false, false, false,  
            false, false, false,  
            false, false, false,  
            false, false, false});
    }
  
    // 획득여부 데이터를 미획득 -> 획득으로 변경하는 함수
    public void ChangeClueIsAcquired(string clueTypeAndIdx)
    {
        int clueType = 0, clueIdx = 0;
        clueType = int.Parse(clueTypeAndIdx.Split(' ')[0]);
        clueIdx = int.Parse(clueTypeAndIdx.Split(' ')[1]);
        Debug.Log("다음은 변경되는 단서 종류와 번호입니다");
        Debug.Log(clueType);
        Debug.Log(clueIdx);
        clueIsAcquired[clueType][clueIdx] = true;
        Debug.Log("도구 획득여부");
        
        string tmp = "";
        for(int i = 0; i < clueIsAcquired.Count; i++) {
            for(int j = 0; j<clueIsAcquired[i].Count; j++) {
                tmp += clueIsAcquired[i][j].ToString() + "\t";
            } 
        } tmp += "\n";
//        Debug.Log(tmp);
       
    }

    // 단서의 획득여부 데이터를 확인하는 함수
    public bool GetClueISAcquired(int clueType, int clueIdx)
    {
        InventoryManager inventoryManager = this.gameObject.GetComponent<InventoryManager>();
        if(clueIsAcquired[clueType][clueIdx] == true){ 
            if(InvestigationManager.Instance.CheckHasKeyword() && !InformUIManager.instance.discoverPopup.activeSelf) { 
            return true;    
            } else return true;
        }
        else return false;    
    }
}
