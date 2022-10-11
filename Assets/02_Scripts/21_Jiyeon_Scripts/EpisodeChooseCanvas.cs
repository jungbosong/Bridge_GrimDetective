using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EpisodeChooseCanvas : MonoBehaviour
{
    int curEpisodeNum = 0;              // 현재 에피소드 번호
    [SerializeField] TextMeshProUGUI curEpisodeNumTxt;   // 현재 에피소드 번호 Text
    [SerializeField] GameObject episodes;
    [SerializeField] Scrollbar episodeSlider;
    [SerializeField] GameObject lockSpace;
    List<GameObject> episodeBtns = new List<GameObject>();
    List<bool> episodeOpened = new List<bool>();
    int openedEpisodeCnt = 1;           // 열린 에피소드 번호 개수
    
    void Awake() 
    {
        curEpisodeNumTxt = curEpisodeNumTxt.GetComponent<TextMeshProUGUI>();
        int episodeCnt = episodes.transform.childCount;
        for(int i = 0; i < episodeCnt; i++) 
        {
            episodeBtns.Add(episodes.transform.GetChild(i).gameObject);
        }
        episodeSlider = episodeSlider.GetComponent<Scrollbar>();
        episodeOpened.Add(true);
        episodeOpened.Add(false);
        episodeOpened.Add(false);
        lockSpace.SetActive(false);
    }
    
    // 다음 버튼 누를 때 실행되는 함수
    public void OnClickedNextBtn()
    {
        Debug.Log("Clicked next Btn");
        curEpisodeNum++;
        if(curEpisodeNum < episodeBtns.Count) 
        {   
            if(episodeOpened[curEpisodeNum])
            {
                lockSpace.SetActive(false);
            }
            else
            {
                lockSpace.SetActive(true);
            }
            episodeSlider.value =  curEpisodeNum * 0.3f;
            curEpisodeNumTxt.text = "에피소드" + (curEpisodeNum+1);
        }
        else
        {
            curEpisodeNum = episodeBtns.Count;
        }
    }

    // 이전 버튼 누를 때 실행되는 함수
    public void OnClickedPreBtn()
    {
        Debug.Log("Clicked Pre Btn");
        curEpisodeNum--;
        if(curEpisodeNum >= 0) 
        {
            if(episodeOpened[curEpisodeNum])
            {
                lockSpace.SetActive(false);
            }
            else
            {
                lockSpace.SetActive(true);
            }
            episodeSlider.value =  curEpisodeNum * 0.3f;
            curEpisodeNumTxt.text = "에피소드" + (curEpisodeNum+1);
        }
        else
        {
            curEpisodeNum = 0;
        }
    }

    // 에피소드 버튼 누를 때 실행되는 함수
    public void OnClickedEpisodeBtn(bool isOpened)
    {
        Debug.Log("ClickedEpisodeBtn");
        if(isOpened) 
        {
            Debug.Log("isOpened");
            SceneManager.LoadScene("Episode1Scene_Jiyeon");
        }
    }
}
