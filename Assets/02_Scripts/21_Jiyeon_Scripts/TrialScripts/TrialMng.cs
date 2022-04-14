using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EndingType {IMPOSSIBILITY, WORST, NORMAL, BEST};

public class TrialMng : MonoBehaviour
{
     CombinationGraph combinationGraph;
    int battleCnt = 0;  // 공방 수
    int endingType = 0; // 엔딩 종류

    [SerializeField]
    int suspect, weapon, motive;     // 테스트용 범인, 흉기, 동기번호

    EndingType endingTypeResult = EndingType.BEST;  // 엔딩 종류 결과

    void Start()
    {
        combinationGraph = this.GetComponent<CombinationGraph>();
    }

    /// <summary>
    /// 배심원의 선평가 결과를 출력하는 함수
    /// 배심원의 선평가 결과 보기 버튼을 클릭했을 때 실행되는 함수
    /// or 추리발표 단계에서 결론내기 버튼을 클릭했을 때 실행되는 함수로 변경할 경우, 추리발표UI클래스에서 메소드 정의
    /// </summary>
    public void ShowResultPreJudgement()
    {
        // 화면에 "배심원이 선평가 중입니다" 출력 -> 변경 가능성 있음
        SetEndingType();
        SetBattleCnt();
        // 엔딩결과에 따른 배심원의 선평가 결과 출력(스프레드 시트 연동 필요)
    }

    /// <summary>
    /// 플레이어가 선택한 조합이 준비된 조합에 있는 지 확인하는 함수
    /// </summary>
    /// <param name = "파라미터명"> 매개변수 설명 </param>
    /// <returns> 반환 값 설명 </returns>

    public void SetEndingType()
    {
        // 플레이어가 선택한 추리 조합 정보 불러오기
    
        //int row = GetSelectedCombination("범인");
        int row = suspect;
        //int col = GetSelectedCombination("흉기");
        int col = weapon;
            
        int firstWeight = combinationGraph.GetWeight(row,col);
        row = col;
        //col = GetSelectedCombination("동기");
        col = motive;
        int secondWeight = combinationGraph.GetWeight(row,col);

        if(firstWeight == secondWeight) {  // 조합에따라 정해진  엔딩타입 설정
            endingType = firstWeight;
        } else {
            endingType = 0;  // 불가능한 추리
        }

        endingTypeResult = (EndingType)endingType;

        Debug.Log("결정된 엔딩 종류");
        Debug.Log(endingTypeResult.ToString());
    }

    /// <summary>
    /// 배심원의 선평가 결과를 통해 공방 수를 바꾸는 함수
    /// </summary>
    public void SetBattleCnt()
    {
        switch(endingType){
            case 0: {   // 불가능한 추리
                // 추리 선택 과정으로 돌아가는 함수 콜; 
                battleCnt = 0;
                break;
            }
            case 1: {   // 최악의 추리 -> 공방수 = 5개
                battleCnt = 5;
                break;
            } 
            case 2: {   // 보통의 추리 -> 공방수 = 4개
                battleCnt = 4;
                break;
            }
            case 3: {   // 최선의 추리 -> 공방수 = 3개
                battleCnt = 3;
                break;
            }
        } 
        Debug.Log("변경된 공방 수");
        Debug.Log(battleCnt);
    }

    //ANCHOR 테스트를 위한 함수들

    public void OnClickedChoose()
    {
        Debug.Log("범인     흉기    동기");
        Debug.Log(suspect +"\t" + weapon + "\t" + motive);
        ShowResultPreJudgement();
    }
}
