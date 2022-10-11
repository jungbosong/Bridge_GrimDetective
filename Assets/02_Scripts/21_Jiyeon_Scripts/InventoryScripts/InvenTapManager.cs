using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvenTapManager : MonoBehaviour
{
    [SerializeField] GameObject characterTapObj;
    [SerializeField] GameObject toolTapObj;
    [SerializeField] GameObject motiveTapObj;
    [SerializeField] GameObject dialogueTapObj;
    TextMeshPro characterTap;
    TextMeshPro toolTap;
    TextMeshPro motiveTap;
    TextMeshPro dialogueTap;
    Color32 clickedColor = new Color32(255,255,255,155);
    Color32 unClickedColor = new Color32(255,255,255,255);

    void Awake() 
    {
        characterTap = characterTapObj.GetComponent<TextMeshPro>();
        toolTap = toolTapObj.GetComponent<TextMeshPro>();
        motiveTap = motiveTapObj.GetComponent<TextMeshPro>();
        dialogueTap = dialogueTapObj.GetComponent<TextMeshPro>();
    }

    public void OnClickedCharacterTap()
    {
        characterTap.color = clickedColor;
        toolTap.color = unClickedColor;
        motiveTap.color = unClickedColor;
        dialogueTap.color = unClickedColor;
    }

    public void OnClickedToolTap()
    {
        characterTap.color = unClickedColor;
        toolTap.color = clickedColor;
        motiveTap.color = unClickedColor;
        dialogueTap.color = unClickedColor;
    }

    public void OnClickedMotiveTap()
    {
        characterTap.color = unClickedColor;
        toolTap.color = unClickedColor;
        motiveTap.color = clickedColor;
        dialogueTap.color = unClickedColor;
    }

    public void OnClickedDialogueTap()
    {
        characterTap.color = unClickedColor;
        toolTap.color = unClickedColor;
        motiveTap.color = unClickedColor;
        dialogueTap.color = clickedColor;
    }
}
