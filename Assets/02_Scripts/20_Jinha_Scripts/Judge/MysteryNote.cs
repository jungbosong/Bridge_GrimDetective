using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Networking;
public class MysteryNote : MonoBehaviour
{
    private static MysteryNote instance = null;
    public static MysteryNote Instance {
        get{
            if(instance == null){
                var obj = FindObjectOfType<MysteryNote>();
                if(obj != null) {
                    instance = obj;
                } else {
                    var newObj = new GameObject().AddComponent<MysteryNote>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }
	public string clueName {get; set;}  // 단서 이름
	public string opinion {get; set;}  // 주인공 의견
	public bool[,] isDecided = new bool[3,4];   // 후보 지정 여부
    public string[] ButtonNameData = new string[12];  //선택할 수 있는 버튼 이름
    public string[] SelectedButtonExplanationData = new string[12];    //버튼 선택시 설명해주는 텍스트
    public string[] data=new string[3];
    public int[] tmpTapButtonId = new int[2];
    public int ChooseJudge;
    
    public void StartTrialChoice(){
        StartCoroutine(ConnectTrialDataSheet());
    }

    public IEnumerator ConnectTrialDataSheet()
    {
        string URL="https://docs.google.com/spreadsheets/d/1f80PNut0CeaJHCDIuZUyTsneKhBrZGmXk9PDZ2_R71Q/export?format=tsv&gid=478020856&range=E21:F32";
        UnityWebRequest www = UnityWebRequest.Get(URL);//범인,흉기,동기 정보 불러오기
        yield return www.SendWebRequest();
        string data = www.downloadHandler.text; 
        string[] line = data.Split('\n');

        for(int i=0; i<ButtonNameData.Length; i++){
            SelectedButtonExplanationData[i]=line[i].Split('\t')[0];
            ButtonNameData[i]=line[i].Split('\t')[1];
        }
    }
}