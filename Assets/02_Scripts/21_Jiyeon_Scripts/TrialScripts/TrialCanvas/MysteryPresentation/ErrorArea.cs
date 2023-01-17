using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ErrorArea : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI errorText;
    [SerializeField] Image lImage;
    [SerializeField] Image rImage;
    Color white, grey;

    void Awake() {
        this.gameObject.SetActive(false);
        ColorUtility.TryParseHtmlString("#FFFFFF", out white);
        ColorUtility.TryParseHtmlString("#484848", out grey);
    }

    public void SetErrorTxt(string msg)
    {
        errorText.text = msg;
    }

    public void ShowErrorTxt()
    {
        this.gameObject.SetActive(true);
        lImage.color = white;
        rImage.color = grey;
        StartCoroutine("ShowErrorTxtCorutine");
    }

    IEnumerator ShowErrorTxtCorutine()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
        yield return waitForSeconds;
        this.gameObject.SetActive(false);
        lImage.color = grey;
        rImage.color = white;
    }
}

