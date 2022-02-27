using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InvenDialogueSheetData : MonoBehaviour
{
    public string dialogTableAddress;
    public List<List<string>> sheetNum= new List<List<string>>();
    public List<List<string>> range = new List<List<string>>();

    void Awake() 
    {
        dialogTableAddress = "https://docs.google.com/spreadsheets/d/1jJdHJTK4TMo6zjty4K6NLCMYymjh-4nM";
        for(int i =0; i<4; i++) {
            sheetNum.Add(new List<string>());
            range.Add(new List<string>());
        }
        // 박석지 대화록 & 범위
        sheetNum[0].Add("1741567521");  range[0].Add("C18");
        sheetNum[0].Add("1242962773");  range[0].Add("C17");
        sheetNum[0].Add("1930158425");  range[0].Add("C24");
        // 구나미 대화록 & 범위
        sheetNum[1].Add("1227269045");  range[1].Add("C15");
        sheetNum[1].Add("931017955");  range[1].Add("C17");
        sheetNum[1].Add("1148838866");  range[1].Add("C21");
        // 구말종 대화록 & 범위
        sheetNum[2].Add("1158960319");  range[2].Add("C11");
        sheetNum[2].Add("18308619");  range[2].Add("C15");
        sheetNum[2].Add("1950443525");  range[2].Add("C15");
        // 강건오 대화록 & 범위
        sheetNum[3].Add("976158909");  range[3].Add("C10");
        sheetNum[3].Add("1567995307");  range[3].Add("C19");
        sheetNum[3].Add("1909111278");  range[3].Add("C18");
        
    }
}
