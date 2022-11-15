using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectJudgyCanvas : MonoBehaviour
{
    [SerializeField] GameObject dialogueCanvas;

    public void OnClickedBtn()
    {
        dialogueCanvas.GetComponent<TrialDialogueCanvas>().StartBasicConversation();
        dialogueCanvas.SetActive(true);
        this.gameObject.SetActive(false);
    }
    
}
