using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryPresentationMng : MonoBehaviour
{
    public int suspectNum{get; set;} 
    public int weaponNum{get; set;}
    public int motiveNum{get; set;}
    public bool isSelectedSuspect{get; set;}
    public bool isSelectedWeapon{get; set;}
    public bool isSelectedMotive{get; set;}

    void Awake() 
    {
        suspectNum = -1;
        weaponNum = -1;
        motiveNum = -1;    
        isSelectedSuspect = false;
        isSelectedWeapon = false;
        isSelectedMotive = false;
    }

    public void ShowNum()
    {
        Debug.Log("용의자번호: " + suspectNum);
        Debug.Log("흉기번호: " + weaponNum);
        Debug.Log("동기번호: " + motiveNum);
    }
}
