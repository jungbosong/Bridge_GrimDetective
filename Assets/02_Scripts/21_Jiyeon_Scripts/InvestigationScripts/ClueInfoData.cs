using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueInfoData: MonoBehaviour
{
    public string clueName;         // 단서 이름
    public Sprite clueSprite;       // 단서 사진
    public string clueInformation;  // 단서 정보
    private bool isFirstGet = true;         // 첫획득 여부
    public bool isCharacter;        // 캐릭터인가 도구인가
    public int characterID;         // 인물 번호
    public bool hasKeyword;         // 키워드 여부
    public string keyword;          // 키워드
    public string reasonForCrime;   // 범행사유

    public void ChangeIsFirstGet()
    {
        isFirstGet = false;
    }

    public bool GetIsFirstGet()
    {
        return isFirstGet;
    }
     
}
