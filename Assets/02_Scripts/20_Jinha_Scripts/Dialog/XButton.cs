using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class XButton : MonoBehaviour
{
    ChoiceButtonManager ChoiceButtonManager;
    //public GameObject ChatContent,ChoiceContent;
    public GameObject ChatPanel;
    
    void Awake() {
      ChoiceButtonManager = this.GetComponent<ChoiceButtonManager>();
    }
    /*public void XButtonChoice()
    {
        var child = ChoiceContent.GetComponentsInChildren<Transform>();
        foreach (var iter in child)
        {
            if(iter != ChoiceContent.transform)
            {
                Destroy(iter.gameObject);
            }
        }
    }*/
    public void XButtonClick()
    {
        Destroy(ChatPanel);
    }
}