using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject episodeChooseCanvas;
    [SerializeField] GameObject collectionCanvas;

    void Awake() {
        episodeChooseCanvas.SetActive(false);
        collectionCanvas.SetActive(false);
    }

    public void OnClickedContinue()
    {
        SceneManager.LoadScene("Episode1Scene_Jiyeon");
    }

    public void OnClickedEpisodeChoose()
    {
        episodeChooseCanvas.SetActive(true);
    }

    public void OnClickedCollection()
    {
        collectionCanvas.SetActive(true);
    }
}
