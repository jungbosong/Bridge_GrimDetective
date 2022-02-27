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
	enum clueType{Suspect,Weapon,Motive};
	//public string imagePath {get; set;} // 단서 이미지 경로
	public string clueName {get; set;}  // 단서 이름
	public string opinion {get; set;}  // 주인공 의견
	public bool[,] isDecided = new bool[4,3];   // 후보 지정 여부
    public List<List<string>> ButtonNameData = new List<List<string>>();  //선택할 수 있는 버튼 이름
    public List<List<string>> SelectedButtonExplanationData = new List<List<string>>();   //버튼 선택시 설명해주는 텍스트 
    public int clickedeasoningTapId;
    public void StartReasoning(){
        StartCoroutine("ConnectTrialSheet");
    }
    public IEnumerator ConnectTrialSheet()
    {
        string URL = "https://docs.google.com/spreadsheets/d/1f80PNut0CeaJHCDIuZUyTsneKhBrZGmXk9PDZ2_R71Q/edit#gid=478020856&range=E21:F32";
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();
        
        string data = www.downloadHandler.text; 
        TrialTextParsing(data);           
    }
    public void TrialTextParsing(string data)
    {
        string[] split_text = data.Split('\n');
        List<string> tmpExplanation = new List<string>();
        List<string> tmpName = new List<string>();

        for(int j=0;j<3;j++){
            for(int k=0;k<4;k++){
                string[] tmp = split_text[4*j+k].Split('\t');
                tmpExplanation.Add(tmp[0]);
                tmpName.Add(tmp[1]);
            }
            SelectedButtonExplanationData.Add(new List<string>(tmpExplanation));
            ButtonNameData.Add(new List<string>(tmpName));
            tmpExplanation.Clear();
            tmpName.Clear();
        }
    }   
}