using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{

    public void OnClickedStartButton()
    {
        SceneManager.LoadScene("MainScene");
    }
}
