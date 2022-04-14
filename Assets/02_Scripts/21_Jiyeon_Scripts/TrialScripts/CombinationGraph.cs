using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 추리조합에 따른 결과 데이터를 저장하고, 관리하는 클래스
/// </summary>

public class CombinationGraph : MonoBehaviour
{
     int[,] matrix = new int[12,12]
    {
        {0,	0,	0,	0,	2,	0,	0,	2,	0,	0,	0,	0},
        {0,	0,	0,	0,	0,	1,	1,	0,	0,	0,	0,	0},
        {0,	0,	0,	0,	3,	2,	0,	0,	0,	0,	0,	0},
        {0,	0,	0,	0,	0,	1,	1,	1,	0,	0,	0,	0},
        {2,	0,	3,	0,	0,	0,	0,	0,	2,	0,	3,	0},
        {0,	1,	2,	1,	0,	0,	0,	0,	0,	1,	2,	1},
        {0,	1,	0,	1,	0,	0,	0,	0,	0,	1,	0,	1},
        {2,	0,	0,	1,	0,	0,	0,	0,	2,	0,	0,	1},
        {0,	0,	0,	0,	2,	0,	0,	2,	0,	0,	0,	0},
        {0,	0,	0,	0,	0,	1,	1,	0,	0,	0,	0,	0},
        {0,	0,	0,	0,	3,	2,	0,	0,	0,	0,	0,	0},
        {0,	0,	0,	0,	0,	1,	1,	1,	0,	0,	0,	0},
    };
        
    /// <summary>
    /// matrix의 값을 반환하는 함수
    /// </summary>
    /// <param name = "row"> 행 번호 </param>
    /// <param name = "col"> 열 번호 </param>
    /// <returns> 인접행렬에 저장된 값 반환 </returns>
    public int GetWeight(int row, int col) 
    {
        return matrix[row, col];
    }
}
