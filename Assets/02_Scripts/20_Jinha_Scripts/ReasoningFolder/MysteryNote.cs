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
	enum clueType{Suspect=130959146,Weapon=1248845403,Motive=1307794352};
	public string clueName {get; set;}  // 단서 이름
	public string opinion {get; set;}  // 주인공 의견
	public bool[,] isDecided = new bool[3,4];   // 후보 지정 여부
    public string[] ButtonNameData = new string[12];  //선택할 수 있는 버튼 이름
    public string[] SelectedButtonExplanationData = new string[12];    //버튼 선택시 설명해주는 텍스트
    public int clickedeasoningTapId;
    public int clickedeasoningButtonId;
    public string[] data=new string[3];
    
    public void StartTrialChoice(){
        StartCoroutine(ConnectTrialDataSheet((int)clueType.Suspect,0));
        StartCoroutine(ConnectTrialDataSheet((int)clueType.Weapon,1));
        StartCoroutine(ConnectTrialDataSheet((int)clueType.Motive,2));
    }
    public IEnumerator ConnectTrialDataSheet(int sheetNumber, int num)
    {
        string URL="https://docs.google.com/spreadsheets/d/17E2KdK9H_hKl7mJo6r6ofEYaCvwiO1-QFEbrCgP9o_o/export?format=tsv&gid="+sheetNumber+"&range=B2:C5";
        UnityWebRequest www = UnityWebRequest.Get(URL);//범인,흉기,동기 통합정보 불러오기
        yield return www.SendWebRequest();
        data[num] = www.downloadHandler.text;
        SplitData(num,4*num);
    }
    public void SplitData(int num1,int num2){
        string[] line_text = data[num1].Split('\n');

        for(int i=0;i<4;i++){
            string tmp1=line_text[i].Split('\t')[0];
            string tmp2=line_text[i].Split('\t')[1];
            ButtonNameData[num2]=tmp1;
            SelectedButtonExplanationData[num2]=tmp2;
            num2++;
        }
    }
}